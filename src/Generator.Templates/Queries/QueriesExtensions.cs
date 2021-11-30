using Generator.Metadata;

namespace Generator.Templates.Queries
{
    public static class QueriesExtensions
    {
        public static string GetQueriesNamespace(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.Name.GetQueriesNamespace();
        }

        public static string GetQueriesNamespace(this string @namespace)
        {
            return $"{@namespace}.Queries";
        }

        public static string GetDtoNamespace(this ModelDefinition modelDefinition, string @namespace)
           => $"{@namespace.GetQueriesNamespace()}.{(modelDefinition.Parent ?? modelDefinition).PluralName}";

        public static string GetDtoName(this ModelDefinition modelDefinition)
            => $"{modelDefinition.Name}Dto";
    }
}
