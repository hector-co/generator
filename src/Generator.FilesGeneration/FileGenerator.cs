﻿using Generator.Metadata;
using Generator.Templates;
using Generator.Templates.Api;
using Generator.Templates.Commands;
using Generator.Templates.DataAccessEf;
using Generator.Templates.Domain;
using Generator.Templates.Queries;
using Humanizer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Generator.FilesGeneration
{
    public class FileGenerator
    {
        private readonly string _outputDir;
        private readonly bool _forceRegen;
        private readonly ModuleDefinition _module;
        private readonly Dictionary<string, TemplateGenerationOption> _templateOptions;
        private readonly bool _useOneOf;

        public FileGenerator(string file, Dictionary<string, TemplateGenerationOption> templateOptions, string outputDir, bool forceRegen, bool useOneOf = false)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException(file);

            _forceRegen = forceRegen;
            _templateOptions = templateOptions;

            _module = JsonConvert.DeserializeObject<ModuleDefinition>(File.ReadAllText(file));
            _module.Init();

            _outputDir = outputDir;
            _useOneOf = useOneOf;
        }

        public void Generate()
        {
            GenerateGlobalFiles();

            foreach (var tplOpt in _templateOptions)
            {
                if (tplOpt.Key == "*")
                {
                    foreach (var modelName in _module.Model.Keys)
                    {
                        GenerateFiles(modelName, tplOpt.Value);
                    }
                }
                else
                {
                    GenerateFiles(tplOpt.Key, tplOpt.Value);
                }
            }
        }

        private void GenerateGlobalFiles()
        {
            var domain = _templateOptions.Values.Any(o => o.Domain);
            var query = _templateOptions.Values.Any(o => o.Query);
            var dataAccessEf = _templateOptions.Values.Any(o => o.DataAccessEf);

            if (domain)
            {
                var modelDirectory = GetFolderPath(_module.Settings.DomainModelFolder);

                var dataAccessEfFileName = $"{modelDirectory}/_DataAccessEf.cs";
                SaveText(dataAccessEfFileName, new DataAccessTemplateEf(_module).TransformText(), _forceRegen);
            }

            if (query)
            {
                var queryDirectory = GetFolderPath(_module.Settings.QueriesFolder);

                foreach (var @enum in _module.Enums)
                {
                    var enumFileName = $"{queryDirectory}/{@enum.Value.GetEnumParent(_module).PluralName}/Queries/{@enum.Key}.cs";
                    SaveText(enumFileName, new EnumTemplate(_module, @enum.Value).TransformText(), _forceRegen);
                }
            }

            if (dataAccessEf)
            {
                var dataAccessEfDirectory = GetFolderPath(_module.Settings.DataAccessEfFolder);

                var dataAccessEfContextFileName = $"{dataAccessEfDirectory}/{_module.Name.GetExtension()}Context.cs";
                SaveText(dataAccessEfContextFileName, new ContextTemplate(_module).TransformText(), _forceRegen);

                var dataAccessEfContextFactoryFileName = $"{dataAccessEfDirectory}/{_module.Name.GetExtension()}ContextFactory.cs";
                if (File.Exists(dataAccessEfContextFactoryFileName)) return;
                SaveText(dataAccessEfContextFactoryFileName, new ContextFactoryTemplate(_module).TransformText(), _forceRegen);
            }
        }

        private void GenerateFiles(string modelName, TemplateGenerationOption option)
        {
            GenerateDomainFiles(modelName, option);
            GenerateCommandFiles(modelName, option);
            GenerateQueryFiles(modelName, option);
            GenerateDataAccessEfFiles(modelName, option);
            GenerateApiFiles(modelName, option);
            GenerateWebUI(modelName, option);
        }

        private void GenerateDomainFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Domain) return;

            var model = _module.Model[modelName];

            var modelDirectory = GetFolderPath(_module.Settings.DomainModelFolder);
            var modelFileName = $"{modelDirectory}/{model.Name}.cs";
            SaveText(modelFileName, new ModelTemplate(_module, model).TransformText(), _forceRegen);
        }

        private void GenerateCommandFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Command) return;

            var model = _module.Model[modelName];

            if (!model.IsRoot || model.IsAbstract) return;

            var commandDirectory = GetFolderPath(_module.Settings.CommandsFolder);

            var registerCommandFileName = $"{commandDirectory}/{model.PluralName}/Commands/{model.GetRegisterCommandClassName()}.cs";
            SaveText(registerCommandFileName, new RegisterTemplate(_module, model).TransformText(), _forceRegen);

            var registerCommandValidatorFileName = $"{commandDirectory}/{model.PluralName}/Commands/{model.GetRegisterCommandValidatorClassName()}.cs";
            SaveText(registerCommandValidatorFileName, new RegisterValidatorTemplate(_module, model).TransformText(), _forceRegen);

            var updateCommandFileName = $"{commandDirectory}/{model.PluralName}/Commands/{model.GetUpdateCommandClassName()}.cs";
            SaveText(updateCommandFileName, new UpdateTemplate(_module, model).TransformText(), _forceRegen);

            var updateCommandValidatorFileName = $"{commandDirectory}/{model.PluralName}/Commands/{model.GetUpdateCommandValidatorClassName()}.cs";
            SaveText(updateCommandValidatorFileName, new UpdateValidatorTemplate(_module, model).TransformText(), _forceRegen);

            var deleteCommandFileName = $"{commandDirectory}/{model.PluralName}/Commands/{model.GetDeleteCommandClassName()}.cs";
            SaveText(deleteCommandFileName, new DeleteTemplate(_module, model).TransformText(), _forceRegen);
        }

        private void GenerateQueryFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Query) return;

            var model = _module.Model[modelName];

            if (model.IsAbstract) return;

            var queryDirectory = GetFolderPath(_module.Settings.QueriesFolder);

            var dtoFileName = $"{queryDirectory}/{(model.RootEntity ?? model).PluralName}/Queries/{model.Name}Dto.cs";
            SaveText(dtoFileName, new DtoTemplate(_module, model).TransformText(), _forceRegen);

            if (model.IsEntity)
            {
                if (!model.IsOwnedEntity)
                {
                    var getByIdFileName = $"{queryDirectory}/{model.PluralName}/Queries/{model.GetDtoByIdClassName()}.cs";
                    SaveText(getByIdFileName, new GetDtoByIdTemplate(_module, model).TransformText(), _forceRegen);

                    var listFileName = $"{queryDirectory}/{model.PluralName}/Queries/{model.ListDtoClassName()}.cs";
                    SaveText(listFileName, new ListDtoTemplate(_module, model).TransformText(), _forceRegen);
                }
            }
        }

        private void GenerateDataAccessEfFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.DataAccessEf) return;

            var model = _module.Model[modelName];

            if (!model.IsEntity || model.IsAbstract) return;

            var dataAccessEfDirectory = GetFolderPath(_module.Settings.DataAccessEfFolder);

            if (!model.IsOwnedEntity)
            {
                if (option.Query)
                {
                    var getByIdHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Queries/{model.GetDtoByIdClassName()}Handler.cs";
                    SaveText(getByIdHandlerFileName, new GetDtoByIdHandlerTemplate(_module, model).TransformText(), _forceRegen);

                    var listHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Queries/{model.ListDtoClassName()}Handler.cs";
                    SaveText(listHandlerFileName, new ListDtoHandlerTemplate(_module, model).TransformText(), _forceRegen);
                }

                if (option.Command)
                {
                    var registerCommandHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Commands/{model.GetRegisterCommandClassName()}Handler.cs";
                    var updateCommandHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Commands/{model.GetUpdateCommandClassName()}Handler.cs";
                    var deleteCommandHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Commands/{model.GetDeleteCommandClassName()}Handler.cs";
                    if (_useOneOf)
                    {
                        SaveText(registerCommandHandlerFileName, new RegisterHandlerTemplateOneOf(_module, model).TransformText(), _forceRegen);
                        SaveText(updateCommandHandlerFileName, new UpdateHandlerTemplateOneOf(_module, model).TransformText(), _forceRegen);
                        SaveText(deleteCommandHandlerFileName, new DeleteHandlerTemplateOneOf(_module, model).TransformText(), _forceRegen);
                    }
                    else
                    {
                        SaveText(registerCommandHandlerFileName, new RegisterHandlerTemplate(_module, model).TransformText(), _forceRegen);
                        SaveText(updateCommandHandlerFileName, new UpdateHandlerTemplate(_module, model).TransformText(), _forceRegen);
                        SaveText(deleteCommandHandlerFileName, new DeleteHandlerTemplate(_module, model).TransformText(), _forceRegen);
                    }
                }

                if (QueryableExtensionsTemplate.RequiresQueryableExtensions(model))
                {
                    var extensionsFileName = $"{dataAccessEfDirectory}/{model.PluralName}/{model.Name}QueryableExtensions.cs";
                    SaveText(extensionsFileName, new QueryableExtensionsTemplate(_module, model).TransformText(), _forceRegen);
                }
            }
            var configFileName = $"{dataAccessEfDirectory}/{(model.RootEntity ?? model).PluralName}/{model.Name}Configuration.cs";
            SaveText(configFileName, new ModelConfigurationTemplate(_module, model).TransformText(), _forceRegen);
        }

        private void GenerateApiFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Api) return;

            var model = _module.Model[modelName];

            if (!model.IsRoot || model.IsAbstract) return;

            var modDirectory = GetFolderPath(_module.Settings.ApiModulesFolder);
            var modFileName = $"{modDirectory}/{model.PluralName}Module.cs";
            if (_useOneOf)
            {
                SaveText(modFileName, new MinimalApiTemplateOneOf(_module, model, option.Command).TransformText(), _forceRegen);
            }
            else
            {
                SaveText(modFileName, new MinimalApiTemplate(_module, model, option.Command).TransformText(), _forceRegen);
            }
        }

        public void GenerateWebUI(string modelName, TemplateGenerationOption option)
        {
            if (!option.WebUI) return;

            var model = _module.Model[modelName];

            if (model.IsAbstract) return;

            var webDir = $"{_outputDir}/../../webapp/src/modules/{_module.Name.Camelize()}";

            var modelFileName = $"{webDir}/model/{model.Name.Camelize()}.ts";
            SaveText(modelFileName, new Templates.WebUI.ModelTemplate(_module, model).Generate(), _forceRegen);

            if (model.IsRoot)
            {
                var serviceFileName = $"{webDir}/services/{model.Name.Camelize()}Service.ts";
                SaveText(serviceFileName, new Templates.WebUI.ServiceTemplate(_module, model).Generate(), _forceRegen);

                var indexPageName = $"{webDir}/pages/{model.PluralName.Camelize()}/IndexPage.vue";
                SaveText(indexPageName, new Templates.WebUI.IndexPageTemplate(_module, model).Generate(), _forceRegen);

                var registerPageName = $"{webDir}/pages/{model.PluralName.Camelize()}/RegisterPage.vue";
                SaveText(registerPageName, new Templates.WebUI.RegisterPageTemplate(_module, model).Generate(), _forceRegen);

                var updatePageName = $"{webDir}/pages/{model.PluralName.Camelize()}/UpdatePage.vue";
                SaveText(updatePageName, new Templates.WebUI.UpdatePageTemplate(_module, model).Generate(), _forceRegen);
            }
        }

        private string GetFolderPath(string folderName)
        {
            if (!string.IsNullOrEmpty(_module.Settings.ModuleFolder) && folderName.StartsWith(_module.Settings.ModuleFolder))
                return $"{_outputDir}/{folderName}";

            return $"{_outputDir}/{(!string.IsNullOrEmpty(_module.Settings.ModuleFolder) ? $"{_module.Settings.ModuleFolder}." : "/")}{folderName}";
        }

        private static void SaveText(string fileName, string text, bool forceRegen)
        {
            if (File.Exists(fileName) && !forceRegen) return;

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            File.WriteAllText(fileName, text);
        }
    }
}
