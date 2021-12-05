using Generator.Metadata;

namespace Generator.Templates.Api
{
    public static class ApiExtensions
    {
        public static string GetApiNamespace(this ModuleDefinition moduleDefinition)
            => $"{moduleDefinition.Name}.{moduleDefinition.Settings.ApiControllersNamespace}";

        public static string GetApiRouteName(this ModelDefinition modelDefinition)
        {
            var result = "";
            foreach (var letter in modelDefinition.PluralName.GetVariableName())
            {
                if (char.IsUpper(letter))
                    result += "-";
                result += letter.ToString().ToLower();
            }
            return result;
        }
    }
}
