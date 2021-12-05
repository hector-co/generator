using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class GetByIdQueryHandlerTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public GetByIdQueryHandlerTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }
    }
}
