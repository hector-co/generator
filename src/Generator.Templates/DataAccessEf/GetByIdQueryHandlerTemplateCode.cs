using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class GetByIdQueryHandlerTemplate
    {
        private readonly string _namespace;
        private readonly ModelDefinition _modelDefinition;

        public GetByIdQueryHandlerTemplate(string @namespace, ModelDefinition modelDefinition)
        {
            _namespace = @namespace;
            _modelDefinition = modelDefinition;
        }
    }
}
