using Generator.Metadata;

namespace Generator.Templates.Queries
{
    public partial class EnumTemplate
    {
        private readonly string _namespace;
        private readonly EnumDefinition _enumDefinition;

        public EnumTemplate(string @namespace, EnumDefinition enumDefinition)
        {
            _namespace = @namespace;
            _enumDefinition = enumDefinition;
        }
    }
}
