using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public static class DataAccessEfExtensions
    {
        public static string GetDataAccessNamespace(this ModuleDefinition moduleDefinition)
            => moduleDefinition.Name.GetDataAccessNamespace();

        public static string GetDataAccessNamespace(this string @namespace)
            => $"{@namespace}.DataAccess.Ef";

        public static string GetContextName(this ModuleDefinition moduleDefinition)
            => moduleDefinition.Name.GetContextName();

        public static string GetContextName(this string @namespace)
            => $"{@namespace}Context";

        public static string GetDataAccessModelNamespace(this ModelDefinition modelDefinition, string @namespace)
            => $"{@namespace.GetDataAccessNamespace()}.{(modelDefinition.Parent ?? modelDefinition).PluralName}";
    }
}
