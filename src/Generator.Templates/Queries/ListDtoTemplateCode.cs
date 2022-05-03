using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Queries
{
    public partial class ListDtoTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public ListDtoTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }

        public static List<PropertyInfo> GetFilterPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            foreach (var property in modelDefinition.EvalProperties.Values.Where(p => p.Filter?.Apply ?? false))
            {
                if (property.IsEntityType && property.CastTargetType<ModelTypeDefinition>().Model.IsAbstract)
                    continue;

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
            if (propertyDefinition.IsSystemType)
            {
                return ResolvePropertyInternalType(propertyDefinition, propertyDefinition.TargetType.Name);
            }
            else if (propertyDefinition.IsEntityType)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return ResolvePropertyInternalType(propertyDefinition, entityType.Model.IdentifierType + (propertyDefinition.TargetType.IsNullable ? "?" : ""));
            }
            else if (propertyDefinition.IsEnumType)
            {
                return ResolvePropertyInternalType(propertyDefinition, "int" + (propertyDefinition.TargetType.IsNullable ? "?" : ""));
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
