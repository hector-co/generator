using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Queries
{
    public partial class DtoTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public DtoTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }

        public static List<string> GetRelatedEntitiesUsings(ModelDefinition modelDefinition, ModuleDefinition moduleDefinition)
        {
            var result = new List<string>();

            foreach (var model in modelDefinition.EvalProperties.Values.Where(p
                => (p.IsEntityType && p.CastTargetType<ModelTypeDefinition>().Model.RootEntity != modelDefinition
                    && p.CastTargetType<ModelTypeDefinition>().Model != modelDefinition
                    && !p.CastTargetType<ModelTypeDefinition>().Model.IsAbstract)
                    || p.IsValueObjectType).Select(p => p.CastTargetType<ModelTypeDefinition>().Model).Distinct())
            {
                result.Add($"{moduleDefinition.GetDtoNamespace(model)}");
            }

            return result;
        }

        public static bool HasPropertiesForInit(ModelDefinition modelDefinition)
        {
            return modelDefinition.EvalProperties.Values.Where(p => p.IsGeneric).Any();
        }

        public static List<PropertyInfo> GetPropertiesForInitInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            foreach (var property in modelDefinition.EvalProperties.Values.Where(p => p.IsGeneric))
            {
                if (property.IsEntityType && property.CastTargetType<ModelTypeDefinition>().Model.IsAbstract)
                    continue;

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

        public static List<PropertyInfo> GetPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            if (modelDefinition.IsEntity)
                result.Add(new PropertyInfo
                {
                    Visibility = "public",
                    TypeName = modelDefinition.IdentifierType,
                    Name = ModelDefinition.IdPropertyName
                });

            foreach (var property in modelDefinition.EvalProperties.Values)
            {
                if (property.IsEntityType && property.CastTargetType<ModelTypeDefinition>().Model.IsAbstract)
                    continue;

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
            if (propertyDefinition.IsSystemType)
            {
                return ResolvePropertyInternalType(propertyDefinition, propertyDefinition.TargetType.Name);
            }
            else if (propertyDefinition.IsValueObjectType || propertyDefinition.IsEntityType)
            {
                var entityType = propertyDefinition.CastTargetType<ModelTypeDefinition>();
                return ResolvePropertyInternalType(propertyDefinition, entityType.Model.GetDtoName());
            }
            else if (propertyDefinition.IsEnumType)
            {
                return ResolvePropertyInternalType(propertyDefinition, propertyDefinition.TargetType.Name + (propertyDefinition.TargetType.IsNullable ? "?" : ""));
            }

            return "";
        }

        private static string ResolvePropertyInternalType(PropertyDefinition propertyDefinition, string targetType)
        {
            return propertyDefinition.InternalTypeName.Replace($":T0:", targetType);
        }
    }
}
