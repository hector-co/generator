using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class GetDtoByIdHandlerTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public GetDtoByIdHandlerTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }
    }
}
