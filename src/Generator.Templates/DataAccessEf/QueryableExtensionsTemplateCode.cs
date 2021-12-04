using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.DataAccessEf
{
    public partial class QueryableExtensionsTemplate
    {
        private readonly string _namespace;
        private readonly ModelDefinition _modelDefinition;

        public QueryableExtensionsTemplate(string @namespace, ModelDefinition modelDefinition)
        {
            _namespace = @namespace;
            _modelDefinition = modelDefinition;
        }

        public static bool RequiresQueryableExtensions(ModelDefinition modelDefinition)
        {
            return RequiresIncludes(modelDefinition);
        }

        public static bool RequiresIncludes(ModelDefinition modelDefinition)
        {
            return modelDefinition.Properties.Values.Any(p => p.IsEntityType);
        }

        public static List<string> GetRelatedEntitiesPropertyNames(ModelDefinition modelDefinition)
        {
            return modelDefinition.Properties.Values
                .Where(p => p.IsEntityType)
                .Select(p => p.Name)
                .ToList();
        }
    }
}
