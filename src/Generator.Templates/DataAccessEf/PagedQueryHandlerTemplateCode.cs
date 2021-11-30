using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class PagedQueryHandlerTemplate
    {
        private readonly string _namespace;
        private readonly ModelDefinition _modelDefinition;

        public PagedQueryHandlerTemplate(string @namespace, ModelDefinition modelDefinition)
        {
            _namespace = @namespace;
            _modelDefinition = modelDefinition;
        }
    }
}
