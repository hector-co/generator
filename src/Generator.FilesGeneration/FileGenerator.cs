using Generator.Metadata;
using Generator.Templates;
using Generator.Templates.Api;
using Generator.Templates.Commands;
using Generator.Templates.DataAccessEf;
using Generator.Templates.Domain;
using Generator.Templates.Queries;
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

        public FileGenerator(string file, Dictionary<string, TemplateGenerationOption> templateOptions, string outputDir, bool forceRegen)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException(file);

            _forceRegen = forceRegen;
            _templateOptions = templateOptions;

            _module = JsonConvert.DeserializeObject<ModuleDefinition>(File.ReadAllText(file));
            _module.Init();

            _outputDir = outputDir;
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
                    var enumFileName = $"{queryDirectory}/{@enum.Key}.cs";
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

            var registerCommandFileName = $"{commandDirectory}/{model.PluralName}/{model.GetRegisterCommandClassName()}.cs";
            SaveText(registerCommandFileName, new RegisterTemplate(_module, model).TransformText(), _forceRegen);

            var registerCommandValidatorFileName = $"{commandDirectory}/{model.PluralName}/{model.GetRegisterCommandValidatorClassName()}.cs";
            SaveText(registerCommandValidatorFileName, new RegisterValidatorTemplate(_module, model).TransformText(), _forceRegen);

            var updateCommandFileName = $"{commandDirectory}/{model.PluralName}/{model.GetUpdateCommandClassName()}.cs";
            SaveText(updateCommandFileName, new UpdateTemplate(_module, model).TransformText(), _forceRegen);

            var updateCommandValidatorFileName = $"{commandDirectory}/{model.PluralName}/{model.GetUpdateCommandValidatorClassName()}.cs";
            SaveText(updateCommandValidatorFileName, new UpdateValidatorTemplate(_module, model).TransformText(), _forceRegen);

            var deleteCommandFileName = $"{commandDirectory}/{model.PluralName}/{model.GetDeleteCommandClassName()}.cs";
            SaveText(deleteCommandFileName, new DeleteTemplate(_module, model).TransformText(), _forceRegen);
        }

        private void GenerateQueryFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Query) return;

            var model = _module.Model[modelName];

            if (model.IsAbstract) return;

            var queryDirectory = GetFolderPath(_module.Settings.QueriesFolder);

            var dtoFileName = $"{queryDirectory}/{(model.RootEntity ?? model).PluralName}/{model.Name}Dto.cs";
            SaveText(dtoFileName, new DtoTemplate(_module, model).TransformText(), _forceRegen);

            if (model.IsEntity)
            {
                if (!model.IsOwnedEntity)
                {
                    var getByIdFileName = $"{queryDirectory}/{model.PluralName}/{model.GetDtoByIdClassName()}.cs";
                    SaveText(getByIdFileName, new GetDtoByIdTemplate(_module, model).TransformText(), _forceRegen);

                    var listFileName = $"{queryDirectory}/{model.PluralName}/{model.ListDtoClassName()}.cs";
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
                var getByIdHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Queries/{model.GetDtoByIdClassName()}Handler.cs";
                SaveText(getByIdHandlerFileName, new GetDtoByIdHandlerTemplate(_module, model).TransformText(), _forceRegen);

                var listHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Queries/{model.ListDtoClassName()}Handler.cs";
                SaveText(listHandlerFileName, new ListDtoHandlerTemplate(_module, model).TransformText(), _forceRegen);

                if (option.Command)
                {
                    var registerCommandHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Commands/{model.GetRegisterCommandClassName()}Handler.cs";
                    SaveText(registerCommandHandlerFileName, new RegisterHandlerTemplate(_module, model).TransformText(), _forceRegen);

                    var updateCommandHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Commands/{model.GetUpdateCommandClassName()}Handler.cs";
                    SaveText(updateCommandHandlerFileName, new UpdateHandlerTemplate(_module, model).TransformText(), _forceRegen);

                    var deleteCommandHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Commands/{model.GetDeleteCommandClassName()}Handler.cs";
                    SaveText(deleteCommandHandlerFileName, new DeleteHandlerTemplate(_module, model).TransformText(), _forceRegen);
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

            var ctrlDirectory = GetFolderPath(_module.Settings.ApiControllersFolder);
            var ctrlFileName = $"{ctrlDirectory}/{model.PluralName}Controller.cs";
            SaveText(ctrlFileName, new ApiControllerTemplate(_module, model, option.Command).TransformText(), _forceRegen);
        }

        private string GetFolderPath(string folderName)
        {
            if (folderName.StartsWith(_module.Name))
                return $"{_outputDir}/{folderName}";
            return $"{_outputDir}/{_module.Name}.{folderName}";
        }

        private static void SaveText(string fileName, string text, bool forceRegen)
        {
            if (File.Exists(fileName) && !forceRegen) return;

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            File.WriteAllText(fileName, text);
        }
    }
}
