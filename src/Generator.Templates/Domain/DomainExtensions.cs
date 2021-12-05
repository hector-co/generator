using Generator.Metadata;
using System;

namespace Generator.Templates.Domain
{
    public static class DomainExtensions
    {
        public static string GetDomainModelNamespace(this ModuleDefinition moduleDefinition)
            => $"{moduleDefinition.Name}.{moduleDefinition.Settings.DomainModelNamespace}";
    }
}
