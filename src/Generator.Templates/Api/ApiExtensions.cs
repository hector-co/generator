using Generator.Metadata;

namespace Generator.Templates.Api
{
    public static class ApiExtensions
    {
        public static string GetApiNamespace(this ModuleDefinition moduleDefinition)
            => moduleDefinition.Name.GetApiNamespace();

        public static string GetApiNamespace(this string @namespace)
            => $"{@namespace}.Api.Controllers";

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
