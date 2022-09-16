using Generator.Metadata;
using System.Linq;
using System.Reflection;

namespace Generator.Templates.Queries
{
    public static class QueriesExtensions
    {
        public static string GetQueriesNamespace(this ModuleDefinition moduleDefinition)
        {
            return $"{moduleDefinition.Name}.{moduleDefinition.Settings.QueriesNamespace}";
        }

        public static string GetDtoNamespace(this ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
           => $"{moduleDefinition.GetQueriesNamespace()}.{(modelDefinition.RootEntity ?? modelDefinition).PluralName}";

        public static string GetDtoName(this ModelDefinition modelDefinition)
            => $"{modelDefinition.Name}Dto";

        public static string GetDtoByIdClassName(this ModelDefinition modelDefinition)
            => $"Get{modelDefinition.GetDtoName()}ById";

        public static string ListDtoClassName(this ModelDefinition modelDefinition)
            => $"List{modelDefinition.GetDtoName()}";

        public static ModelDefinition GetEnumParent(this EnumDefinition enumDefinition, ModuleDefinition moduleDefinition)
        {
            foreach (var model in moduleDefinition.Model.Values)
            {
                if (model.Properties.Any(p => p.Value.IsEnumType && p.Value.CastTargetType<EnumTypeDefinition>().Enum == enumDefinition))
                    return model.RootEntity ?? model;
            }
            return null;
        }
    }
}
