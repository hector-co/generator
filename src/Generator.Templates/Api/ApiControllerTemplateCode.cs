using Generator.Metadata;

namespace Generator.Templates.Api
{
    public partial class ApiControllerTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;
        private readonly bool _generateCommands;

        public ApiControllerTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition, bool generateCommands)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
            _generateCommands = generateCommands;
        }
    }
}
