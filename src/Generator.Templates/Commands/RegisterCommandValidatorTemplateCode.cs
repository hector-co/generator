using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.Commands
{
    public partial class RegisterCommandValidatorTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public RegisterCommandValidatorTemplate(ModuleDefinition module, ModelDefinition model)
        {
            _module = module;
            _model = model;
        }

        public static string GetCommandValidatorName(ModelDefinition modelDefinition)
        {
            return $"{RegisterCommandTemplate.GetCommandName(modelDefinition)}Validator";
        }

        private static Dictionary<string, List<string>> GetPropertiesValidations(ModelDefinition modelDefinition)
        {
            var result = new Dictionary<string, List<string>>();

            foreach (var property in modelDefinition.Properties.Values)
            {
                var validations = new List<string>();
                if (!property.IsGeneric && property.IsRootType && !property.CastTargetType<ModelTypeDefinition>().IsNullable)
                    validations.Add("NotEmpty()");
                if (!property.IsGeneric && property.IsOwnedEntity && !property.CastTargetType<ModelTypeDefinition>().IsNullable)
                    validations.Add("NotEmpty()");
                if (!property.IsGeneric && property.IsValueObjectType)
                    validations.Add($"SetValidator(new {property.CastTargetType<ModelTypeDefinition>().Model.Name}Validator())");
                if (property.Required ?? false)
                    validations.Add("NotEmpty()");
                if ((property.Size ?? 0) > 0)
                    validations.Add($"MaximumLength({property.Size})");

                if (validations.Any())
                {
                    validations[validations.Count - 1] = validations[validations.Count - 1] + ";";
                    result.Add(GetPropertyName(modelDefinition.GetParent() ?? modelDefinition, property), validations);
                }
            }
            return result;
        }

        private static Dictionary<string, string> GetCollectionPropertiesValidations(ModelDefinition modelDefinition)
        {
            var result = new Dictionary<string, string>();

            foreach (var property in modelDefinition.Properties.Values)
            {
                if (property.IsCollection && ((!property.IsRootType && property.IsEntityType && property.CastTargetType<ModelTypeDefinition>().Model != modelDefinition) || property.IsValueObjectType))
                    result.Add(GetPropertyName(modelDefinition.GetParent() ?? modelDefinition, property), property.CastTargetType<ModelTypeDefinition>().Model.Name);
            }
            return result;
        }

        private static List<ModelDefinition> GetModelsForValidations(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            var entities = moduleDefinition.EntityModels.Where(e => e.GetParent() == modelDefinition);
            var valueObjects = modelDefinition.Properties.Values
                .Where(p => p.IsValueObjectType)
                .Select(p => p.CastTargetType<ModelTypeDefinition>().Model)
                .Distinct();

            return entities.Concat(valueObjects).Distinct().ToList();
        }

        private static string GetPropertyName(ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            return propertyDefinition.IsRootType || (propertyDefinition.IsEntityType && propertyDefinition.CastTargetType<ModelTypeDefinition>().Model.GetParent() != modelDefinition)
                ? propertyDefinition.Name + ModelDefinition.IdPropertyName
                : propertyDefinition.Name;
        }
    }
}
