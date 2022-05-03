using Generator.Metadata;

namespace Generator.Templates.Commands
{
    public static class CommandExtensions
    {
        public static string GetCommandsNamespace(this ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
           => $"{moduleDefinition.Name}.{moduleDefinition.Settings.CommandsNamespace}.{(modelDefinition.RootEntity ?? modelDefinition).PluralName}";

        public static string GetRegisterCommandClassName(this ModelDefinition modelDefinition)
            => $"Register{modelDefinition.Name}";

        public static string GetRegisterCommandValidatorClassName(this ModelDefinition modelDefinition)
            => $"{modelDefinition.GetRegisterCommandClassName()}Validator";
    }
}
