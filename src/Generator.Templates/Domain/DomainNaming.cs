using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Domain
{
    public static class DomainNaming
    {
        public static string GetDomainModelNameSpace(this ModuleDefinition moduleDefinition)
        {
            return $"{moduleDefinition.Name}.Domain.Model";
        }

        public static string GetBaseClassName(this ModelDefinition modelDefinition, ModuleDefinition moduleDefinition)
        {
            if (modelDefinition.IsRoot)
                return string.IsNullOrEmpty(moduleDefinition.DomainSettings.AggregateRootBaseClass)
                    ? ""
                    : $": {moduleDefinition.DomainSettings.AggregateRootBaseClass.Replace(":T0:", modelDefinition.IdentifierType)}";

            if (modelDefinition.IsEntity)
                return string.IsNullOrEmpty(moduleDefinition.DomainSettings.EntityBaseClass)
                        ? ""
                        : $": {moduleDefinition.DomainSettings.EntityBaseClass.Replace(":T0:", modelDefinition.IdentifierType)}";

            return "";
        }

        public static string GetPropertyTypeName(this ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.IsSystemType || propertyDefinition.IsValueObjectType || propertyDefinition.IsEnumType)
                return ResolvePropertyInternalType(propertyDefinition, propertyDefinition.TargetType.Name);

            if (propertyDefinition.IsEntityType)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                if (entityType.Model.Parent == modelDefinition)
                    return ResolvePropertyInternalType(propertyDefinition, entityType.Name, false);
                else
                    return ResolvePropertyInternalType(propertyDefinition, entityType.Model.IdentifierType);
            }

            return "";
        }

        private static string ResolvePropertyInternalType(PropertyDefinition propertyDefinition, string targetType, bool applyNullable = true)
        {
            if (propertyDefinition.IsEntityType && propertyDefinition.TargetType.IsNullable && applyNullable)
                targetType += "?";

            return propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
        }

        public static string GetPropertyName(this ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.IsSystemType || propertyDefinition.IsValueObjectType || propertyDefinition.IsEnumType)
                return propertyDefinition.Name;

            if (propertyDefinition.IsEntityType)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                if (entityType.Model.Parent == modelDefinition)
                    return propertyDefinition.Name;
                else
                    return propertyDefinition.Name + "Id" + (propertyDefinition.IsCollection ? "s" : "");
            }

            return "";
        }
    }
}
