using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class ContextFactoryTemplate
    {
        private readonly ModuleDefinition _module;

        public ContextFactoryTemplate(ModuleDefinition moduleDefinition)
        {
            _module = moduleDefinition;
        }
    }
}
