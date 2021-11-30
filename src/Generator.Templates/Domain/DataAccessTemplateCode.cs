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

        private static string ResolveEntityPropertyTypeName(ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.WithMany)
                return $"List<{modelDefinition.Name}{propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.Name}DataAccess>";

            var targetType = propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.TypeName;
            if (propertyDefinition.IsEntityType && propertyDefinition.TargetType.IsNullable)
                targetType += "?";

            return propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
        }

        public static List<PropertyInfo> GetEntityPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            var properties = modelDefinition.Properties.Values.Where(p => !p.IsGeneric && p.IsOwnedEntity)
                .Concat(modelDefinition.Properties.Values.Where(p => !p.IsGeneric && p.IsEntityType && !p.IsOwnedEntity));
            foreach (var property in properties)
            {
                var propInfo = new PropertyInfo
                {
                    Visibility = "internal",
                    TypeName = ResolveEntityPropertyTypeName(modelDefinition, property),
                    Name = property.WithMany
                    ? property.Name + "DataAccess"
                    : property.Name + ModelDefinition.IdPropertyName
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
                var propInfo = new PropertyInfo
                {
                    Visibility = "internal",
                    TypeName = model.IdentifierProperty.TargetType.Name,
                    Name = model.Name + ModelDefinition.IdPropertyName
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
                    TypeName = ModelTemplate.GetPropertyTypeName(property),
                    Name = property.Name
                };
                result.Add(propInfo);
            }
            return result;
        }

        public static List<PropertyInfo> GetJoinPropertiesInfo(ModelDefinition modelDefinition)
        {
            if (!modelDefinition.Properties.Values.Any(p => p.IsCollection && p.IsEntityType && p.WithMany))
                return new List<PropertyInfo>();

            var result = new List<PropertyInfo>();

            var properties = modelDefinition.Properties.Values.Where(p => p.IsCollection && p.IsEntityType && p.WithMany);
            foreach (var property in properties)
            {
                result.Add(new PropertyInfo
                {
                    Visibility = "internal",
                    TypeName = $"List<{modelDefinition.Name}{property.CastTargetType<ModelTypeDefinition>().Model.Name}DataAccess>",
                    Name = $"{property.CastTargetType<ModelTypeDefinition>().Model.Name}DataAccess"
                });
            }
            return result;
        }

        public static Dictionary<string, List<PropertyInfo>> GetJoinClassesInfo(ModuleDefinition moduleDefinition)
        {
            var result = new Dictionary<string, List<PropertyInfo>>();

            var models = moduleDefinition.Models.Values.Where(m => m.Properties.Values.Any(p => p.IsCollection && p.IsEntityType && p.WithMany));

            foreach (var model in models)
            {
                var properties = model.Properties.Values.Where(p => p.IsCollection && p.IsEntityType && p.WithMany);
                foreach (var property in properties)
                {
                    result.Add($"{model.Name}{property.CastTargetType<ModelTypeDefinition>().Model.Name}DataAccess", new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            Visibility = "internal",
                            TypeName = model.IdentifierProperty.TargetType.Name,
                            Name = model.Name + ModelDefinition.IdPropertyName
                        },
                        new PropertyInfo
                        {
                            Visibility = "internal",
                            TypeName = model.Name,
                            Name = model.Name
                        },
                        new PropertyInfo
                        {
                            Visibility = "internal",
                            TypeName = property.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.TargetType.Name,
                            Name = property.CastTargetType<ModelTypeDefinition>().Model.Name + ModelDefinition.IdPropertyName
                        },
                        new PropertyInfo
                        {
                            Visibility = "internal",
                            TypeName = property.CastTargetType<ModelTypeDefinition>().Model.Name,
                            Name = property.CastTargetType<ModelTypeDefinition>().Model.Name
                        }
                    });

                }
            }
            return result;
        }
    }
}
