using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class ContextTemplate
    {
        private readonly ModuleDefinition _moduleDefinition;

        public ContextTemplate(ModuleDefinition moduleDefinition)
        {
            _moduleDefinition = moduleDefinition;
        }
    }
}
