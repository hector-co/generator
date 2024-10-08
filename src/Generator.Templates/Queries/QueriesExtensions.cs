﻿using Generator.Metadata;
using System.Linq;

namespace Generator.Templates.Queries
{
    public static class QueriesExtensions
    {
        public static string GetDtoNamespace(this ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
           => !modelDefinition.IsExternal
            ? $"{moduleDefinition.Namespace}.{moduleDefinition.Settings.QueriesNamespace}.{(modelDefinition.RootEntity ?? modelDefinition).PluralName}.Queries"
            : $"{modelDefinition.External}.Application.{modelDefinition.PluralName}.Queries";

        public static string GetDtoName(this ModelDefinition modelDefinition)
            => $"{modelDefinition.Name}Dto";

        public static string GetDtoByIdClassName(this ModelDefinition modelDefinition)
            => $"Get{modelDefinition.Name}ById";

        public static string ListDtoClassName(this ModelDefinition modelDefinition)
            => $"List{modelDefinition.PluralName}";

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
