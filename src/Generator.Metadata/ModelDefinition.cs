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
        public bool IsAbstract { get; set; }
        public string InheritsFrom { get; set; }
        public string External { get; set; }
        [JsonIgnore]
        public ModelDefinition RootEntity { get; set; }
        [JsonIgnore]
        public PropertyDefinition IdentifierProperty { get; set; }
        public Dictionary<string, PropertyDefinition> Properties { get; set; } = new Dictionary<string, PropertyDefinition>();

        public bool IsOwnedEntity => !IsRoot && IsEntity;
        public bool IsValueObject => !IsRoot && !IsEntity;
        public bool RequiresDataAccessClass { get; private set; }
        public bool IsChildClass => !string.IsNullOrEmpty(InheritsFrom);
        public bool IsExternal => !string.IsNullOrEmpty(External);
        [JsonIgnore]
        public Dictionary<string, PropertyDefinition> EvalProperties { get; private set; } = new Dictionary<string, PropertyDefinition>();

        public void Init(ModuleDefinition moduleDefinition, string name)
        {
            Name = name;

            if (!IsEntity)
                IsRoot = false;

            if (IsEntity)
            {
                IdentifierProperty = new PropertyDefinition
                {
                    TypeName = IdentifierType,
                    Filter = new FilterDefinition
                    {
                        Apply = true
                    }
                };
                IdentifierProperty.Init(moduleDefinition, IdPropertyName);
            }
        }

        public void InitProperties(ModuleDefinition moduleDefinition)
        {
            foreach (var propertyName in Properties.Keys)
                Properties[propertyName].Init(moduleDefinition, propertyName);
        }

        public void AdjustRelationships(ModuleDefinition moduleDefinition)
        {
            if (IsOwnedEntity || IsValueObject)
            {
                foreach (var model in moduleDefinition.Model.Values)
                {
                    var modelProps = model.Properties
                        .Where(p => p.Value.IsEntityType || p.Value.IsValueObjectType)
                        .Select(p => p.Value.TargetType.Cast<ModelTypeDefinition>())
                        .Where(p => p.Model == this);
                    if (modelProps.Any())
                    {
                        RootEntity = model.RootEntity ?? model;
                        break;
                    }
                }

                if (RootEntity == null)
                    throw new ArgumentException(null, nameof(RootEntity));
            }

            RequiresDataAccessClass = IdentifierProperty != null &&
                (Properties.Any(p => p.Value.IsEntityType && !p.Value.IsGeneric) ||
                Properties.Any(p => p.Value.IsSystemType && p.Value.IsGeneric) ||
                moduleDefinition.Model.Values.Any(m => m.Properties.Values.Any(p => p.IsEntityType && p.IsCollection && p.CastTargetType<ModelTypeDefinition>().Model == this)));

            if (IsChildClass)
            {
                var parent = moduleDefinition.EntityModels.FirstOrDefault(m => m.Name == InheritsFrom);
                foreach (var property in parent.Properties)
                {
                    EvalProperties.Add(property.Key, property.Value);
                }
            }
            foreach (var property in Properties)
            {
                EvalProperties.Add(property.Key, property.Value);
            }
        }

        public bool HasMultiplePropertiesWithModelType(ModelDefinition modelDefinition, bool isGeneric)
        {
            return Properties.Values.Where(p => p.IsEntityType && p.CastTargetType<ModelTypeDefinition>().Model == modelDefinition && p.IsGeneric == isGeneric).Count() > 1;
        }

        public ModelDefinition GetRootEntity()
        {
            if (RootEntity == null) return RootEntity;
            if (!RootEntity.IsRoot) return RootEntity.GetRootEntity();
            return RootEntity;
        }
    }
}
