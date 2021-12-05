using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class ContextTemplate
    {
        private readonly ModuleDefinition _module;

        public ContextTemplate(ModuleDefinition moduleDefinition)
        {
            _module = moduleDefinition;
        }
    }
}
