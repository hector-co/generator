using Generator.Metadata;

namespace Generator.Templates.Commands
{
    public partial class DeleteTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public DeleteTemplate(ModuleDefinition module, ModelDefinition model)
        {
            _module = module;
            _model = model;
        }
    }
}
