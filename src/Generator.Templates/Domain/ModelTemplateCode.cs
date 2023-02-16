using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Domain
{
    public partial class ModelTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public ModelTemplate(ModuleDefinition module, ModelDefinition model)
        {
            _module = module;
            _model = model;
        }

        public static string GetBaseClassName(ModelDefinition modelDefinition, ModuleDefinition moduleDefinition)
        {
            if (!string.IsNullOrEmpty(modelDefinition.InheritsFrom))
                return $": {modelDefinition.InheritsFrom}";

            if (modelDefinition.IsRoot)
                return string.IsNullOrEmpty(moduleDefinition.Settings.RootBaseClass)
                    ? ""
                    : $": {moduleDefinition.Settings.RootBaseClass.Replace(":T0:", modelDefinition.IdentifierType)}";

            if (modelDefinition.IsEntity)
                return string.IsNullOrEmpty(moduleDefinition.Settings.EntityBaseClass)
                        ? ""
                        : $": {moduleDefinition.Settings.EntityBaseClass.Replace(":T0:", modelDefinition.IdentifierType)}";

            return "";
        }

        public static bool HasPropertiesForInit(ModelDefinition modelDefinition)
        {
            return modelDefinition.Properties.Values.Where(p =>
                    (p.IsGeneric && !p.IsRootType) ||
                    (p.IsGeneric && p.IsOwnedEntity && p.CastTargetType<ModelTypeDefinition>().Model.RootEntity == modelDefinition))
                .Any();
        }

        public static List<PropertyInfo> GetPropertiesForInitInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            foreach (var property in modelDefinition.Properties.Values.Where(p => p.IsGeneric))
            {
                if ((property.IsGeneric && property.IsRootType) ||
                    (property.IsGeneric && property.IsOwnedEntity && property.CastTargetType<ModelTypeDefinition>().Model.RootEntity != modelDefinition))
                    continue;

                var propInfo = new PropertyInfo
                {
                    Visibility = "public",
                    TypeName = GetPropertyTypeAndName(modelDefinition, property).PropType,
                    Name = property.Name
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static List<PropertyInfo> GetPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            foreach (var property in modelDefinition.Properties.Values)
            {
                if ((property.IsGeneric && property.IsRootType) ||
                    (property.IsGeneric && property.IsOwnedEntity && property.CastTargetType<ModelTypeDefinition>().Model.RootEntity != modelDefinition))
                    continue;

                var (PropName, PropType) = GetPropertyTypeAndName(modelDefinition, property);
                var propInfo = new PropertyInfo
                {
                    Visibility = "public",
                    TypeName = PropType,
                    Name = PropName
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static (string PropName, string PropType) GetPropertyTypeAndName(ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.IsSystemType || propertyDefinition.IsValueObjectType)
            {
                return (propertyDefinition.Name, ResolvePropertyInternalType(propertyDefinition, propertyDefinition.TargetType.Name));
            }
            else if (propertyDefinition.IsEntityType && propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.RootEntity == modelDefinition)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return (propertyDefinition.Name, ResolvePropertyInternalType(propertyDefinition, entityType.Name));
            }
            else if ((propertyDefinition.IsEntityType && propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.RootEntity != modelDefinition)
                || propertyDefinition.IsRootType)
            {
                return (propertyDefinition.Name + ModelDefinition.IdPropertyName,
                    ResolvePropertyInternalType(propertyDefinition, propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.TypeName) + (propertyDefinition.TargetType.IsNullable ? "?" : ""));
            }
            else if (propertyDefinition.IsEnumType)
            {
                return (propertyDefinition.Name, ResolvePropertyInternalType(propertyDefinition, "int" + (propertyDefinition.TargetType.IsNullable ? "?" : "")));
            }

            return ("", "");
        }

        private static string ResolvePropertyInternalType(PropertyDefinition propertyDefinition, string targetType)
        {
            return propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
        }

    }
}
