using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

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

        private static List<string> GetIgnoredPropertyNames(ModelDefinition modelDefinition)
        {
            return modelDefinition.Properties.Values.Where(p => p.IsSystemType && p.IsGeneric)
                .Concat(modelDefinition.Properties.Values.Where(p => p.IsGeneric && !p.IsCollection))
                .Select(p => p.Name).Distinct().ToList();
        }

        private static List<string> GetSerializedPropertyNames(ModelDefinition modelDefinition)
        {
            return modelDefinition.Properties.Values.Where(p => p.IsSystemType && p.IsGeneric)
                .Concat(modelDefinition.Properties.Values.Where(p => p.IsGeneric && !p.IsCollection))
                .Select(p => p.Name).Distinct().ToList();
        }

        private static List<string> GetNonGenericEntitiesPropertyNames(ModelDefinition modelDefinition)
        {
            return modelDefinition.Properties.Values.Where(p => p.IsEntityType && !p.IsGeneric)
                .Select(p => p.Name).Distinct().ToList();
        }

        private static List<PropertyDefinition> GetManyToOneProperties(ModelDefinition modelDefinition)
        {
            return modelDefinition.Properties.Values.Where(p => p.IsEntityType && p.IsCollection && !p.WithMany)
                .Distinct().ToList();
        }
    }
}
