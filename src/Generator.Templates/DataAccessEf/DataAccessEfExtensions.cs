using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public static class DataAccessEfExtensions
    {
        public static string GetDataAccessNamespace(this ModuleDefinition moduleDefinition)
            => $"{moduleDefinition.Name}.{moduleDefinition.Settings.DataAccessEfNamespace}";

        public static string GetContextName(this ModuleDefinition moduleDefinition)
            => $"{moduleDefinition.Name.GetExtension()}Context";

        public static string GetDataAccessModelNamespace(this ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
            => $"{moduleDefinition.GetDataAccessNamespace()}.{(modelDefinition.Parent ?? modelDefinition).PluralName}";
    }
}
