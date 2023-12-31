using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class DeleteHandlerTemplateOneOf
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public DeleteHandlerTemplateOneOf(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }
    }
}
