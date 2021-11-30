using Generator.Metadata;

namespace Generator.Templates.Domain
{
    public static class DomainExtensions
    {
        public static string GetDomainModelNamespace(this ModuleDefinition moduleDefinition)
            => moduleDefinition.Name.GetDomainModelNamespace();

        public static string GetDomainModelNamespace(this string @namespace)
            => $"{@namespace}.Domain.Model";
    }
}
