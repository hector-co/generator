using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.DataAccessEf
{
    public partial class QueryableExtensionsTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public QueryableExtensionsTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
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
