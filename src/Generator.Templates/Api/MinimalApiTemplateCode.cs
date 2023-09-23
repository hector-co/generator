using Generator.Metadata;

namespace Generator.Templates.Api
{
    public partial class MinimalApiTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;
        private readonly bool _generateCommands;

        public MinimalApiTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition, bool generateCommands)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
            _generateCommands = generateCommands;
        }
    }
}
