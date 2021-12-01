using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class JoinModelConfigurationTemplate
    {
        private readonly string _namespace;
        private readonly ModelDefinition _modelDefinition;
        private readonly PropertyDefinition _propertyDefinition;

        public JoinModelConfigurationTemplate(string @namespace, ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            _namespace = @namespace;
            _modelDefinition = modelDefinition;
            _propertyDefinition = propertyDefinition;
        }
    }
}
