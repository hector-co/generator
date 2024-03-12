using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Generator.Metadata;
using Generator.Templates.Queries;
using Humanizer;

namespace Generator.Templates
{
    public static class Extensions
    {
        public static string GetVariablePluralName(this string name) => (name[..1].ToLower() + name[1..]).Pluralize();

        public static string GetVariableName(this string name) => name[..1].ToLower() + name[1..];

        public static string GetExtension(this string value)
        {
            return !value.Contains('.') ? value : value[(value.LastIndexOf(".", StringComparison.Ordinal) + 1)..];
        }

        public static IEnumerable<(T item, int index, int count)> WithIndex<T>(this IEnumerable<T> source)
        {
            var enumerable = source as T[] ?? source.ToArray();
            return enumerable.Select((item, index) => (item, index, enumerable.Length));
        }

        public static bool HasRelatedEntities(this ModelDefinition modelDefinition, ModuleDefinition moduleDefinition)
        {
            var anyEntityRelated = modelDefinition.EvalProperties.Values.Where(p
                    => (p.IsEntityType && p.CastTargetType<ModelTypeDefinition>().Model.RootEntity != modelDefinition
                                       && p.CastTargetType<ModelTypeDefinition>().Model != modelDefinition
                                       && !p.CastTargetType<ModelTypeDefinition>().Model.IsAbstract))
                .Distinct().Any();

            var enumTypes = modelDefinition.EvalProperties
                .Where(p => p.Value.IsEnumType)
                .Select(p => p.Value.CastTargetType<EnumTypeDefinition>().Enum).Distinct();

            if (!enumTypes.Any())
                return anyEntityRelated;

            foreach (var enumType in enumTypes)
            {
                var firstEntity = moduleDefinition.Model.Values
                    .First(e => e.Properties.Values.Any(p => p.IsEnumType && p.CastTargetType<EnumTypeDefinition>().Enum == enumType));

                if (firstEntity != modelDefinition)
                    return true;
            }

            return anyEntityRelated;
        }

        public static List<string> GetRelatedEntitiesUsings(this ModelDefinition modelDefinition, ModuleDefinition moduleDefinition)
        {
            return modelDefinition.GetRelatedEntities(moduleDefinition)
                .Where(m => !m.IsOwned)
                .Select(m => $"{moduleDefinition.GetDtoNamespace(m.Model)}")
                .Distinct()
                .ToList();
        }

        public static List<(ModelDefinition Model, bool IsOwned)> GetRelatedEntities(this ModelDefinition modelDefinition, ModuleDefinition moduleDefinition)
        {
            var result = new List<(ModelDefinition Model, bool SameLevel)>();

            foreach (var model in modelDefinition.EvalProperties.Values.Where(p
                => (p.IsEntityType && p.CastTargetType<ModelTypeDefinition>().Model.RootEntity != modelDefinition
                    && p.CastTargetType<ModelTypeDefinition>().Model != modelDefinition
                    && !p.CastTargetType<ModelTypeDefinition>().Model.IsAbstract))
                         .Select(p => p.CastTargetType<ModelTypeDefinition>().Model).Distinct())
            {
                if (model.Name == modelDefinition.Name)
                    continue;

                result.Add((model, false));
            }

            foreach (var model in modelDefinition.EvalProperties.Values.Where(p
                => (p.TargetType is ModelTypeDefinition && p.CastTargetType<ModelTypeDefinition>().Model.RootEntity == (modelDefinition.RootEntity ?? modelDefinition)
                    && p.CastTargetType<ModelTypeDefinition>().Model != modelDefinition
                    && !p.CastTargetType<ModelTypeDefinition>().Model.IsAbstract))
                         .Select(p => p.CastTargetType<ModelTypeDefinition>().Model).Distinct())
            {
                if (model.Name == modelDefinition.Name)
                    continue;

                result.Add((model, true));
            }

            var enumTypes = modelDefinition.EvalProperties
                .Where(p => p.Value.IsEnumType)
                .Select(p => p.Value.CastTargetType<EnumTypeDefinition>().Enum).Distinct();
            foreach (var enumType in enumTypes)
            {
                var firstEntity = moduleDefinition.Model.Values
                    .First(e => e.Properties.Values.Any(p => p.IsEnumType && p.CastTargetType<EnumTypeDefinition>().Enum == enumType));

                if (firstEntity != modelDefinition)
                    result.Add((firstEntity, false));
            }

            return result.Distinct().ToList();
        }
    }
}
