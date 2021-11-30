using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Queries
{
    public partial class PagedQueryTemplate
    {
        private readonly string _namespace;
        private readonly ModelDefinition _modelDefinition;

        public PagedQueryTemplate(string @namespace, ModelDefinition modelDefinition)
        {
            _namespace = @namespace;
            _modelDefinition = modelDefinition;
        }

        public static List<PropertyInfo> GetFilterPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            foreach (var property in modelDefinition.Properties.Values.Where(p => p.Filter?.Apply ?? false))
            {
                var propInfo = new PropertyInfo
                {
                    Visibility = "public",
                    TypeName = GetPropertyTypeName(property),
                    Name = property.IsEntityType ? property.Name + ModelDefinition.IdPropertyName : property.Name
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static string GetPropertyTypeName(PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.IsSystemType || propertyDefinition.IsEnumType)
            {
                return ResolvePropertyInternalType(propertyDefinition, propertyDefinition.TargetType.Name);
            }
            else if (propertyDefinition.IsEntityType)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return ResolvePropertyInternalType(propertyDefinition, entityType.Model.IdentifierType);
            }

            return "";
        }

        private static string ResolvePropertyInternalType(PropertyDefinition propertyDefinition, string targetType)
        {
            var tType = propertyDefinition.IsGeneric ? targetType : propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
            return $"{propertyDefinition.Filter.FilterTypeName}<{tType}>";
        }
    }
}
