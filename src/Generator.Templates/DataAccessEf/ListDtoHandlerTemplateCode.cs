using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class ListDtoHandlerTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public ListDtoHandlerTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }
    }
}
