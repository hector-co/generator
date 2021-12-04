using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Metadata
{
    public class ModelDefinition
    {
        public const string IdPropertyName = "Id";

        private string _pluralName;

        public string Name { get; set; }
        public string IdentifierType { get; set; } = "int";
        public string PluralName
        {
            get
            {
                return string.IsNullOrEmpty(_pluralName) ? Name.Pluralize() : _pluralName;
            }
            set
            {
                _pluralName = value;
            }
        }
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

        public void Init(ModuleDefinition moduleDefinition, string name)
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
                IdentifierProperty.Init(moduleDefinition, ModelDefinition.IdPropertyName);
            }
        }

        public void InitProperties(ModuleDefinition moduleDefinition)
        {
            foreach (var propertyName in Properties.Keys)
                Properties[propertyName].Init(moduleDefinition, propertyName);
        }

        public void AdjustRelationships(ModuleDefinition moduleDefinition)
        {
            foreach (var propertyName in Properties.Keys)
                Properties[propertyName].Init(moduleDefinition, propertyName);

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
                (Properties.Any(p => p.Value.IsEntityType && !p.Value.IsGeneric) ||
                Properties.Any(p => p.Value.IsSystemType && p.Value.IsGeneric) ||
                moduleDefinition.Models.Values.Any(m => m.Properties.Values.Any(p => p.IsEntityType && p.IsCollection && p.CastTargetType<ModelTypeDefinition>().Model == this)));
        }

        public bool HasMultiplePropertiesWithModelType(ModelDefinition modelDefinition, bool isGeneric)
        {
            return Properties.Values.Where(p => p.IsEntityType && p.CastTargetType<ModelTypeDefinition>().Model == modelDefinition && p.IsGeneric == isGeneric).Count() > 1;
        }
    }
}
