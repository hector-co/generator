using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Commands
{
    public partial class UpdateTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public UpdateTemplate(ModuleDefinition module, ModelDefinition model)
        {
            _module = module;
            _model = model;
        }

        public static bool HasSubClasses(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            return moduleDefinition.GetSubModels(modelDefinition).Any();
        }

        public static Dictionary<string, List<PropertyInfo>> GetSubClasses(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            var entities = moduleDefinition.GetSubModels(modelDefinition);
            return entities.Distinct().ToDictionary(
                e => "Update" + e.Name,
                e => GetPropertiesInfo(e));
        }

        public static List<PropertyInfo> GetPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            if (modelDefinition.IsEntity && !modelDefinition.IsRoot)
                result.Add(new PropertyInfo
                {
                    Visibility = "public",
                    TypeName = modelDefinition.IdentifierType,
                    Name = modelDefinition.IdentifierProperty.Name
                });

            foreach (var property in modelDefinition.EvalProperties.Values)
            {
                if (property.IsEntityType && property.CastTargetType<ModelTypeDefinition>().Model.IsAbstract)
                    continue;

                var propInfo = new PropertyInfo
                {
                    Visibility = "public",
                    Name = GetPropertyName(modelDefinition.GetRootEntity() ?? modelDefinition, property),
                    TypeName = GetPropertyTypeName(modelDefinition.GetRootEntity() ?? modelDefinition, property)
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static string GetPropertyTypeName(ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.IsSystemType)
            {
                return ResolvePropertyInternalType(propertyDefinition, propertyDefinition.TargetType.Name);
            }
            else if (propertyDefinition.IsRootType || (propertyDefinition.IsEntityType && propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.GetRootEntity() != modelDefinition))
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return ResolvePropertyInternalType(propertyDefinition, entityType.Model.IdentifierType + (propertyDefinition.TargetType.IsNullable ? "?" : ""));
            }
            else if (propertyDefinition.IsEntityType || propertyDefinition.IsValueObjectType)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return ResolvePropertyInternalType(propertyDefinition, "Update" + entityType.Model.Name, "Update" + modelDefinition.Name + ".");
            }
            else if (propertyDefinition.IsEnumType)
            {
                return ResolvePropertyInternalType(propertyDefinition, "int" + (propertyDefinition.TargetType.IsNullable ? "?" : ""));
            }

            return "";
        }

        private static string ResolvePropertyInternalType(PropertyDefinition propertyDefinition, string targetType, string prefix = "")
        {
            return propertyDefinition.InternalTypeName.Replace($":T0:", prefix + targetType);
        }

        private static string GetPropertyName(ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            return propertyDefinition.IsRootType || (propertyDefinition.IsEntityType && propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.GetRootEntity() != modelDefinition)
                ? propertyDefinition.Name + ModelDefinition.IdPropertyName
                : propertyDefinition.Name;
        }
    }
}
