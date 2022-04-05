using Generator.Metadata;

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
    }
}
