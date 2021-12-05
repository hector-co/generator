using Generator.Metadata;

namespace Generator.Templates.Api
{
    public partial class ApiControllerTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public ApiControllerTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }
    }
}
