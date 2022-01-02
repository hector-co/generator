using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Commands
{
    public partial class RegisterCommandTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public RegisterCommandTemplate(ModuleDefinition module, ModelDefinition model)
        {
            _module = module;
            _model = model;
        }

        public static string GetCommandName(ModelDefinition modelDefinition)
        {
            return $"Register{modelDefinition.Name}Command";
        }

        public static bool HasPropertiesForInit(ModelDefinition modelDefinition)
        {
            return modelDefinition.Properties.Values.Where(p => p.IsGeneric).Any();
        }

        public static List<PropertyInfo> GetPropertiesForInitInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            foreach (var property in modelDefinition.Properties.Values.Where(p => p.IsGeneric))
            {
                var propInfo = new PropertyInfo
                {
                    Visibility = "public",
                    TypeName = GetPropertyTypeName(modelDefinition.GetParent() ?? modelDefinition, property),
                    Name = GetPropertyName(modelDefinition, property)
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static Dictionary<string, List<PropertyInfo>> GetSubClasses(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            var entities = moduleDefinition.EntityModels.Where(e => e.GetParent() == modelDefinition);
            var valueObjects = modelDefinition.Properties.Values
                .Where(p => p.IsValueObjectType)
                .Select(p => p.CastTargetType<ModelTypeDefinition>().Model)
                .Distinct();
            return entities.Concat(valueObjects).ToDictionary(
                e => e.Name,
                e => e.Properties.Values.Select(p =>
                    new PropertyInfo
                    {
                        Visibility = "public",
                        Name = GetPropertyName(modelDefinition, p),
                        TypeName = GetPropertyTypeName(modelDefinition.GetParent() ?? modelDefinition, p)
                    }).ToList());
        }

        public static List<PropertyInfo> GetPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            foreach (var property in modelDefinition.Properties.Values)
            {
                var propInfo = new PropertyInfo
                {
                    Visibility = "public",
                    TypeName = GetPropertyTypeName(modelDefinition, property),
                    Name = GetPropertyName(modelDefinition, property)
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
            else if (propertyDefinition.IsRootType || (propertyDefinition.IsEntityType && propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.GetParent() != modelDefinition))
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return ResolvePropertyInternalType(propertyDefinition, entityType.Model.IdentifierType + (propertyDefinition.TargetType.IsNullable ? "?" : ""));
            }
            else if (propertyDefinition.IsEntityType || propertyDefinition.IsValueObjectType)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return ResolvePropertyInternalType(propertyDefinition, entityType.Model.Name);
            }
            else if (propertyDefinition.IsEnumType)
            {
                return ResolvePropertyInternalType(propertyDefinition, "int" + (propertyDefinition.TargetType.IsNullable ? "?" : ""));
            }

            return "";
        }

        private static string ResolvePropertyInternalType(PropertyDefinition propertyDefinition, string targetType)
        {
            return propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
        }

        private static string GetPropertyName(ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            return propertyDefinition.IsRootType || (propertyDefinition.IsEntityType && propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.GetParent() != modelDefinition)
                ? propertyDefinition.Name + ModelDefinition.IdPropertyName
                : propertyDefinition.Name;
        }
    }
}
