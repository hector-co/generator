using Generator.Metadata;

namespace Generator.Templates.Queries
{
    public partial class GetByIdQueryTemplate
    {
        private readonly string _namespace;
        private readonly ModelDefinition _modelDefinition;

        public GetByIdQueryTemplate(string @namespace, ModelDefinition modelDefinition)
        {
            _namespace = @namespace;
            _modelDefinition = modelDefinition;
        }
    }
}
