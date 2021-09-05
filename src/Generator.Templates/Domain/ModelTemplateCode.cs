using Generator.Metadata;

namespace Generator.Templates.Domain
{
    public partial class ModelTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public ModelTemplate(ModuleDefinition module, ModelDefinition model)
        {
            _module = module;
            _model = model;
        }

    }
}
