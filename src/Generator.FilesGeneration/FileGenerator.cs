using Generator.Metadata;
using Generator.Templates.Api;
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
        private readonly ModuleDefinition _moduleDefinition;
        private readonly Dictionary<string, TemplateGenerationOption> _templateOptions;

        public FileGenerator(string file, Dictionary<string, TemplateGenerationOption> templateOptions, string outputDir, bool forceRegen)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException(file);

            _forceRegen = forceRegen;
            _templateOptions = templateOptions;

            _moduleDefinition = JsonConvert.DeserializeObject<ModuleDefinition>(File.ReadAllText(file));
            _moduleDefinition.Init();

            _outputDir = outputDir + "/" + _moduleDefinition.Name;
        }

        public void Generate()
        {
            GenerateGlobalFiles();

            foreach (var tplOpt in _templateOptions)
            {
                if (tplOpt.Key == "*")
                {
                    foreach (var modelName in _moduleDefinition.Model.Keys)
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
                var modelDirectory = $"{_outputDir}.Domain/Model";

                var dataAccessEfFileName = $"{modelDirectory}/_DataAccessEf.cs";
                SaveText(dataAccessEfFileName, new DataAccessTemplateEf(_moduleDefinition).TransformText(), _forceRegen);
            }

            if (query)
            {
                var queryDirectory = $"{_outputDir}.Queries";

                foreach (var @enum in _moduleDefinition.Enums)
                {
                    var enumFileName = $"{queryDirectory}/{@enum.Key}.cs";
                    SaveText(enumFileName, new EnumTemplate(_moduleDefinition.Name, @enum.Value).TransformText(), _forceRegen);
                }
            }

            if (dataAccessEf)
            {
                var dataAccessEfDirectory = $"{_outputDir}.DataAccess.Ef";

                var dataAccessEfContextFileName = $"{dataAccessEfDirectory}/Context.cs";
                SaveText(dataAccessEfContextFileName, new ContextTemplate(_moduleDefinition).TransformText(), _forceRegen);

                var dataAccessEfContextFactoryFileName = $"{dataAccessEfDirectory}/ContextFactory.cs";
                SaveText(dataAccessEfContextFactoryFileName, new ContextFactoryTemplate(_moduleDefinition).TransformText(), _forceRegen);
            }
        }

        private void GenerateFiles(string modelName, TemplateGenerationOption option)
        {
            GenerateDomainFiles(modelName, option);
            GenerateQueryFiles(modelName, option);
            GenerateDataAccessEfFiles(modelName, option);
            GenerateApiFiles(modelName, option);
        }

        private void GenerateDomainFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Domain) return;

            var model = _moduleDefinition.Model[modelName];

            var modelDirectory = $"{_outputDir}.Domain/Model";
            var modelFileName = $"{modelDirectory}/{model.Name}.cs";
            SaveText(modelFileName, new ModelTemplate(_moduleDefinition, model).TransformText(), _forceRegen);
        }

        private void GenerateQueryFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Query) return;

            var model = _moduleDefinition.Model[modelName];

            var queryDirectory = $"{_outputDir}.Queries";

            var dtoFileName = $"{queryDirectory}/{(model.Parent ?? model).PluralName}/{model.Name}Dto.cs";
            SaveText(dtoFileName, new DtoTemplate(_moduleDefinition.Name, model).TransformText(), _forceRegen);

            if (model.IsEntity)
            {
                if (!model.IsOwnedEntity)
                {
                    var getByIdFileName = $"{queryDirectory}/{model.PluralName}/{model.Name}DtoGetById.cs";
                    SaveText(getByIdFileName, new GetByIdQueryTemplate(_moduleDefinition.Name, model).TransformText(), _forceRegen);

                    var pagedFileName = $"{queryDirectory}/{model.PluralName}/{model.Name}DtoPagedQuery.cs";
                    SaveText(pagedFileName, new PagedQueryTemplate(_moduleDefinition.Name, model).TransformText(), _forceRegen);
                }
            }
        }

        private void GenerateDataAccessEfFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Query) return;

            var model = _moduleDefinition.Model[modelName];

            if (!model.IsEntity) return;

            var dataAccessEfDirectory = $"{_outputDir}.DataAccess.Ef";

            if (!model.IsOwnedEntity)
            {
                var getByIdHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Queries/{model.Name}DtoGetByIdQueryHandler.cs";
                SaveText(getByIdHandlerFileName, new GetByIdQueryHandlerTemplate(_moduleDefinition.Name, model).TransformText(), _forceRegen);

                var pagedHandlerFileName = $"{dataAccessEfDirectory}/{model.PluralName}/Queries/{model.Name}DtoPagedQueryHandler.cs";
                SaveText(pagedHandlerFileName, new PagedQueryHandlerTemplate(_moduleDefinition.Name, model).TransformText(), _forceRegen);

                if (QueryableExtensionsTemplate.RequiresQueryableExtensions(model))
                {
                    var extensionsFileName = $"{dataAccessEfDirectory}/{model.PluralName}/{model.Name}QueryableExtensions.cs";
                    SaveText(extensionsFileName, new QueryableExtensionsTemplate(_moduleDefinition.Name, model).TransformText(), _forceRegen);
                }
            }
            var configFileName = $"{dataAccessEfDirectory}/{(model.Parent ?? model).PluralName}/{model.Name}Configuration.cs";
            SaveText(configFileName, new ModelConfigurationTemplate(_moduleDefinition.Name, model).TransformText(), _forceRegen);
        }

        private void GenerateApiFiles(string modelName, TemplateGenerationOption option)
        {
            if (!option.Domain) return;

            var model = _moduleDefinition.Model[modelName];

            if (!model.IsRoot) return;

            var ctrlDirectory = $"{_outputDir}.Api/Controllers";
            var ctrlFileName = $"{ctrlDirectory}/{model.PluralName}Controller.cs";
            SaveText(ctrlFileName, new ApiControllerTemplate(_moduleDefinition, model).TransformText(), _forceRegen);
        }

        private static void SaveText(string fileName, string text, bool forceRegen)
        {
            if (File.Exists(fileName) && !forceRegen) return;

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            File.WriteAllText(fileName, text);
        }
    }
}
