using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class ModelConfigurationTemplate
    {
        private readonly string _namespace;
        private readonly ModelDefinition _modelDefinition;

        public ModelConfigurationTemplate(string @namespace, ModelDefinition modelDefinition)
        {
            _namespace = @namespace;
            _modelDefinition = modelDefinition;
        }
    }
}
