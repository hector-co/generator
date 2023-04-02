using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public static class DataAccessEfExtensions
    {
        public static string GetDataAccessNamespace(this ModuleDefinition moduleDefinition)
            => $"{moduleDefinition.Namespace}.{moduleDefinition.Settings.DataAccessEfNamespace}";

        public static string GetContextName(this ModuleDefinition moduleDefinition)
            => $"{moduleDefinition.Name}Context";

        public static string GetDataAccessModelNamespace(this ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
            => $"{moduleDefinition.GetDataAccessNamespace()}.{(modelDefinition.RootEntity ?? modelDefinition).PluralName}";
    }
}
