using Humanizer;

namespace Generator.Templates
{
    public static class Extensions
    {
        public static string GetVariablePluralName(this string name) => (name.Substring(0, 1).ToLower() + name.Substring(1)).Pluralize();

        public static string GetVariableName(this string name) => name.Substring(0, 1).ToLower() + name.Substring(1);

        public static string GetExtension(this string value)
        {
            if (!value.Contains('.'))
                return value;

            return value.Substring(value.LastIndexOf(".") + 1);
        }
    }
}
