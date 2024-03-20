using Generator.Metadata;

namespace Generator.Templates.WebUI;

public static class WebUiExtensions
{
    public static string GetTsType(this PropertyDefinition property)
    {
        return property.TargetType switch
        {
            SystemTypeDefinition systemType => systemType.Name switch
            {
                var x when
                    x == "char" ||
                    x == "string" ||
                    x == "DateTime" ||
                    x == "DateTimeOffset" ||
                    x == "TimeSpan" ||
                    x == "Guid" => "string",
                "bool" => "boolean",
                _ => "number"
            },
            ModelTypeDefinition => "",
            _ => "number"
        };
    }

    public static string GetDefaultValue(this PropertyDefinition property)
    {
        var type = property.TargetType switch
        {
            SystemTypeDefinition systemType => systemType.Name switch
            {
                var x when
                    x == "char" ||
                    x == "string" ||
                    x == "DateTime" ||
                    x == "DateTimeOffset" ||
                    x == "TimeSpan" ||
                    x == "Guid" => ("string", "''"),
                "bool" => ("boolean", "false"),
                _ => ("number", "0")
            },
            ModelTypeDefinition modelType => (modelType.Model.Name, $"new {modelType.Model.Name}()"),
            _ => ("number", "0")
        };

        return property.IsCollection
            ? $": {type.Item1}[] = []"
            : $" = {type.Item2}";
    }
}
