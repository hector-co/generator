using Generator.Metadata;

namespace Generator.Templates.Queries
{
    public partial class GetDtoByIdTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public GetDtoByIdTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }
    }
}
