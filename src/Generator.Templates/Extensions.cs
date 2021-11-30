using Humanizer;

namespace Generator.Templates
{
    public static class Extensions
    {
        public static string GetVariablePluralName(this string name) => (name.Substring(0, 1).ToLower() + name.Substring(1)).Pluralize();

        public static string GetVariableName(this string name) => name.Substring(0, 1).ToLower() + name.Substring(1);
    }
}
