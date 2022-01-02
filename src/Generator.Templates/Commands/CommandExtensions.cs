using Generator.Metadata;

namespace Generator.Templates.Commands
{
    public static class CommandExtensions
    {
        public static string GetCommandsNamespace(this ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
           => $"{moduleDefinition.Name}.{moduleDefinition.Settings.CommandsNamespace}.{(modelDefinition.Parent ?? modelDefinition).PluralName}";
    }
}
