using Generator.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Domain
{
    public partial class DataAccessTemplateEf
    {
        private readonly ModuleDefinition _module;

        public DataAccessTemplateEf(ModuleDefinition module)
        {
            _module = module;
        }

        private static string ResolveEntityPropertyTypeName(ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            var targetType = propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.Name;
            if (propertyDefinition.IsEntityType && propertyDefinition.TargetType.IsNullable)
                targetType += "?";

            return propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
        }

        public static List<PropertyInfo> GetEntityPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            var properties = modelDefinition.Properties.Values.Where(p => !p.IsGeneric && p.IsRootType)
                .Concat(modelDefinition.Properties.Values.Where(p => p.IsCollection && p.IsRootType))
                .Concat(modelDefinition.Properties.Values.Where(p => !p.IsGeneric && p.IsOwnedEntity));
            foreach (var property in properties)
            {
                var propInfo = new PropertyInfo
                {
                    Visibility = "internal",
                    TypeName = ResolveEntityPropertyTypeName(modelDefinition, property),
                    Name = property.Name
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static List<PropertyInfo> GetSourceEntityPropertiesInfo(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            var models = moduleDefinition.Model.Values
                .Where(m => m.Properties.Values.Any(p => !p.IsRootType && p.IsEntityType && p.IsCollection && p.CastTargetType<ModelTypeDefinition>().Model == modelDefinition));

            foreach (var model in models)
            {
                var properties = model.Properties.Values.Where(p => p.IsEntityType && p.IsCollection && p.CastTargetType<ModelTypeDefinition>().Model == modelDefinition);
                foreach (var property in properties)
                {
                    var propInfo = new PropertyInfo
                    {
                        Visibility = "internal",
                        TypeName = property.WithMany
                            ? $"List<{model.Name}>"
                            : model.IdentifierProperty.TargetType.Name + (property.Required.HasValue ? (property.Required.Value ? "" : "?") : ""),
                        Name = properties.Count() == 1
                            ? property.WithMany
                                ? $"{model.Name}{property.Name}"
                                : model.Name + ModelDefinition.IdPropertyName 
                            : property.Name + (property.WithMany ? model.PluralName : (model.Name))
                    };
                    result.Add(propInfo);
                }
            }
            return result;
        }

        public static List<PropertyInfo> GetGenericSystemPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            var properties = modelDefinition.Properties.Values.Where(p => p.IsGeneric && p.IsSystemType);
            foreach (var property in properties)
            {
                var propInfo = new PropertyInfo
                {
                    Visibility = "internal",
                    TypeName = ModelTemplate.GetPropertyTypeAndName(modelDefinition, property).PropType,
                    Name = property.Name
                };
                result.Add(propInfo);
            }
            return result;
        }
    }
}
