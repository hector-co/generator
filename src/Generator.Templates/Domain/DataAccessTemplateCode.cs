using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Domain
{
    public partial class DataAccessTemplate
    {
        private readonly ModuleDefinition _module;

        public DataAccessTemplate(ModuleDefinition module)
        {
            _module = module;
        }

        private static string ResolveEntityPropertyTypeName(PropertyDefinition propertyDefinition)
        {
            if (!propertyDefinition.IsOwnedEntity)
                return propertyDefinition.InternalTypeName.Replace($":T0:", propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.Name);

            var targetType = propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.TypeName;
            if (propertyDefinition.IsEntityType && propertyDefinition.TargetType.IsNullable)
                targetType += "?";

            return propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
        }

        public static List<PropertyInfo> GetEntityPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            var properties = modelDefinition.Properties.Values.Where(p => !p.IsGeneric && p.IsOwnedEntity)
                .Concat(modelDefinition.Properties.Values.Where(p => p.IsEntityType && !p.IsOwnedEntity));
            foreach (var property in properties)
            {
                var propInfo = new PropertyInfo
                {
                    Visibility = "internal",
                    TypeName = ResolveEntityPropertyTypeName(property),
                    Name = property.Name + (property.IsOwnedEntity ? "Id" : "")
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static List<PropertyInfo> GetSourceEntityPropertiesInfo(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            var models = moduleDefinition.Models.Values
                .Where(m => m.Properties.Values.Any(p => p.IsEntityType && p.IsCollection && p.CastTargetType<ModelTypeDefinition>().Model == modelDefinition));

            foreach (var model in models)
            {
                //var property = model.Properties.Values.FirstOrDefault(p => p.IsEntityType && p.IsCollection && p.CastTargetType<ModelTypeDefinition>().Model == modelDefinition);
                var propInfo = new PropertyInfo
                {
                    Visibility = "internal",
                    TypeName = model.IdentifierProperty.TargetType.Name,
                    Name = model.Name + "Id"
                };
                result.Add(propInfo);
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
                    TypeName = ModelTemplate.GetPropertyTypeName(modelDefinition, property),
                    Name = property.Name
                };
                result.Add(propInfo);
            }
            return result;
        }
    }
}
