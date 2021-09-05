using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Metadata
{
    public class ModelDefinition
    {
        public string Name { get; set; }
        public string IdentifierType { get; set; } = "int";
        public string PluralName { get; set; }
        public bool IsRoot { get; set; } = true;
        public bool IsEntity { get; set; } = true;
        [JsonIgnore]
        public ModelDefinition Parent { get; set; }
        [JsonIgnore]
        public PropertyDefinition IdentifierProperty { get; set; }
        public Dictionary<string, PropertyDefinition> Properties { get; set; }

        public bool IsOwnedEntity => !IsRoot && IsEntity;
        public bool IsValueObject => !IsRoot && !IsEntity;
        public bool RequiresDataAccessClass { get; private set; }

        public void AdjustProperties(ModuleDefinition moduleDefinition, string name)
        {
            Name = name;

            if (!IsEntity)
                IsRoot = false;

            if (IsEntity)
            {
                IdentifierProperty = new PropertyDefinition
                {
                    TypeName = IdentifierType
                };
                IdentifierProperty.Init(moduleDefinition, "Id");
            }

            foreach (var propertyName in Properties.Keys)
                Properties[propertyName].Init(moduleDefinition, propertyName);
        }

        public void Init(ModuleDefinition moduleDefinition, string name)
        {
            if (IsOwnedEntity)
            {
                foreach (var model in moduleDefinition.Models.Values)
                {
                    var modelProps = model.Properties
                        .Where(p => p.Value.IsEntityType)
                        .Select(p => p.Value.TargetType.Cast<ModelTypeDefinition>())
                        .Where(p => p.Model == this);
                    if (modelProps.Any())
                    {
                        Parent = model;
                        break;
                    }
                }

                if (Parent == null)
                    throw new ArgumentException(null, nameof(Parent));
            }

            RequiresDataAccessClass = IdentifierProperty != null &&
                (Properties.Any(p => p.Value.IsGeneric) ||
                Properties.Any(p => p.Value.IsEntityType) ||
                moduleDefinition.Models.Values.Any(m => m.Properties.Values.Any(p => p.IsEntityType && p.IsCollection && p.CastTargetType<ModelTypeDefinition>().Model == this)));
        }
    }
}
