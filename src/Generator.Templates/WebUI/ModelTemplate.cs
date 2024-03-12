using Generator.Metadata;
using Humanizer;
using System;
using System.Linq;

namespace Generator.Templates.WebUI;

public class ModelTemplate
{
    private readonly ModuleDefinition _module;
    private readonly ModelDefinition _model;

    public ModelTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
    {
        _module = moduleDefinition;
        _model = modelDefinition;
    }

    public string Generate()
    {
        return $$"""
            import { merge } from 'lodash';
            {{GetImports()}}
            export class {{_model.Name}} {
              constructor(data?: any) {
                if (data) {
                  merge(this, data, {{GetMergeMapping()}});
                }
              }

            {{GetProperties()}}
            }
            """;
    }

    private string GetProperties()
    {
        var props = string.Empty;
        if (_model.IsEntity)
        {
            props += $"  id = 0;{Environment.NewLine}";
        }
        foreach (var property in _model.Properties)
        {
            props += $"  {property.Key.Camelize()}{GetDefaultValue(property.Value)};{Environment.NewLine}";
        }
        return props.TrimEnd();
    }

    private static string GetDefaultValue(PropertyDefinition property)
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

    private string GetImports()
    {
        var imports = string.Empty;
        foreach (var model in _model.GetRelatedEntities(_module).Select(m => m.Model).Distinct())
        {
            if (model.IsExternal)
            {
                imports += $"import {{ {model.Name} }} from '../../{model.External.Camelize()}/model/{model.Name.Camelize()}';{Environment.NewLine}";
            }
            else
            {
                imports += $"import {{ {model.Name} }} from './{model.Name.Camelize()}';{Environment.NewLine}";
            }
        }
        return imports;
    }

    private string GetMergeMapping()
    {
        if (!_model.EvalProperties.Any(p => p.Value.IsEntityType || p.Value.IsOwnedEntity || p.Value.IsValueObjectType))
        {
            return "{}";
        }

        var mapping = string.Empty;
        foreach (var property in _model.Properties.Values.Where(p => p.IsEntityType || p.IsOwnedEntity || p.IsValueObjectType))
        {
            if (property.IsCollection)
                mapping += $"        {property.Name.Camelize()}: (data.{property.Name.Camelize()}?.map((x: any) => new {property.TargetType.Name}(x))) ?? [],{Environment.NewLine}";    
            else
                mapping += $"        {property.Name.Camelize()}: new {property.TargetType.Name}(data.{property.TargetType.Name.Camelize()}),{Environment.NewLine}";
        }

        return $"{{{Environment.NewLine}{mapping}      }}";
    }
}