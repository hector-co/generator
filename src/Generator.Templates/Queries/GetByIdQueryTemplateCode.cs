using Generator.Metadata;

namespace Generator.Templates.Queries
{
    public partial class GetByIdQueryTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public GetByIdQueryTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }
    }
}
