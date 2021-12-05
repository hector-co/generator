using Generator.Metadata;

namespace Generator.Templates.Queries
{
    public partial class EnumTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly EnumDefinition _enum;

        public EnumTemplate(ModuleDefinition moduleDefinition, EnumDefinition enumDefinition)
        {
            _module = moduleDefinition;
            _enum = enumDefinition;
        }
    }
}
