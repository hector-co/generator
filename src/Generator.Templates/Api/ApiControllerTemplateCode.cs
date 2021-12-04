using Generator.Metadata;

namespace Generator.Templates.Api
{
    public partial class ApiControllerTemplate
    {
        private readonly ModuleDefinition _moduleDefinition;
        private readonly ModelDefinition _modelDefinition;

        public ApiControllerTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _moduleDefinition = moduleDefinition;
            _modelDefinition = modelDefinition;
        }
    }
}
