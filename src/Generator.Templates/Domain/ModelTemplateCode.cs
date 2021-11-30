using Generator.Metadata;
using System.Collections.Generic;

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
            if (modelDefinition.IsRoot)
                return string.IsNullOrEmpty(moduleDefinition.DomainSettings.RootBaseClass)
                    ? ""
                    : $": {moduleDefinition.DomainSettings.RootBaseClass.Replace(":T0:", modelDefinition.IdentifierType)}";

            if (modelDefinition.IsEntity)
                return string.IsNullOrEmpty(moduleDefinition.DomainSettings.EntityBaseClass)
                        ? ""
                        : $": {moduleDefinition.DomainSettings.EntityBaseClass.Replace(":T0:", modelDefinition.IdentifierType)}";

            return "";
        }

        public static List<PropertyInfo> GetPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            foreach (var property in modelDefinition.Properties.Values)
            {
                var propInfo = new PropertyInfo
                {
                    Visibility = "public",
                    TypeName = GetPropertyTypeName(property),
                    Name = property.Name
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static string GetPropertyTypeName(PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.IsSystemType || propertyDefinition.IsValueObjectType || propertyDefinition.IsEnumType)
            {
                return ResolvePropertyInternalType(propertyDefinition, propertyDefinition.TargetType.Name);
            }
            else if (propertyDefinition.IsEntityType)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return ResolvePropertyInternalType(propertyDefinition, entityType.Name);
            }

            return "";
        }

        private static string ResolvePropertyInternalType(PropertyDefinition propertyDefinition, string targetType)
        {
            return propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
        }

    }
}
