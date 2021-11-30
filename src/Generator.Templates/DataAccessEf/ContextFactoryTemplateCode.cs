using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class ContextFactoryTemplate
    {
        private readonly ModuleDefinition _moduleDefinition;

        public ContextFactoryTemplate(ModuleDefinition moduleDefinition)
        {
            _moduleDefinition = moduleDefinition;
        }
    }
}
