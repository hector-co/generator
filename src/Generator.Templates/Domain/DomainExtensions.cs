using Generator.Metadata;
using System;

namespace Generator.Templates.Domain
{
    public static class DomainExtensions
    {
        public static string GetDomainModelNamespace(this ModuleDefinition moduleDefinition)
            => moduleDefinition.Name.GetDomainModelNamespace();

        public static string GetDomainModelNamespace(this string @namespace)
            => $"{@namespace}.Domain.Model";

        public static string GetJoinModelTypeName(this ModelDefinition modelDefinition, PropertyDefinition propertyDefinition)
        {
            if (!propertyDefinition.RelationRequiresJoinModel()) throw new Exception("Relation does not require Join Table");
            return $"{modelDefinition.Name}{propertyDefinition.Name}DataAccess";
        }

        public static string GetJoinModelTypePropertyName(this PropertyDefinition propertyDefinition)
        {
            if (!propertyDefinition.RelationRequiresJoinModel()) throw new Exception("Relation does not require Join Table");
            return $"{propertyDefinition.Name}DataAccess";
        }
    }
}
