using Humanizer;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Metadata
{
    public class ModuleDefinition
    {
        public string Namespace { get; set; }
        public string Name { get; set; }

        public Settings Settings { get; set; } = new Settings();
        public Dictionary<string, ModelDefinition> Model { get; set; } = new Dictionary<string, ModelDefinition>();
        public Dictionary<string, EnumDefinition> Enums { get; set; } = new Dictionary<string, EnumDefinition>();
        public CustomTypeDefinition[] CustomTypes { get; set; }

        public IEnumerable<ModelDefinition> RootModels => Model.Values.Where(m => m.IsRoot);
        public IEnumerable<ModelDefinition> EntityModels => Model.Values.Where(m => m.IsEntity);
        public IEnumerable<ModelDefinition> OwnedEntityModels => Model.Values.Where(m => m.IsOwnedEntity);
        public IEnumerable<ModelDefinition> ValueObjectModels => Model.Values.Where(m => m.IsValueObject);

        public void Init()
        {
            if (Settings.ModuleFolder == null)
                Settings.ModuleFolder = Namespace;

            foreach (var modelName in Model.Keys)
                Model[modelName].Init(this, modelName);

            foreach (var modelName in Model.Keys)
                Model[modelName].InitProperties(this);

            foreach (var modelName in Model.Keys)
                Model[modelName].AdjustRelationships(this);

            foreach (var enumName in Enums.Keys)
                Enums[enumName].Init(enumName);

            AdjustUI();

            if (Settings == null) Settings = new Settings();
            Settings.ModuleName = Name;
        }

        public IEnumerable<ModelDefinition> GetSubModels(ModelDefinition modelDefinition)
        {
            var entities = EntityModels.Where(e => e.GetRootEntity() == modelDefinition);
            var valueObjects = modelDefinition.EvalProperties.Values
                .Where(p => p.IsValueObjectType)
                .Select(p => p.CastTargetType<ModelTypeDefinition>().Model)
                .Distinct();

            entities = entities.Concat(valueObjects);

            var result = new List<ModelDefinition>(entities);

            foreach (var entity in entities)
                result.AddRange(GetSubModels(entity));

            return result.Distinct();
        }

        public void AdjustUI()
        {
            foreach (var model in Model.Values)
            {
                foreach (var property in model.Properties.Values)
                {
                    property.UI ??= new UiDefinition();

                    if (string.IsNullOrEmpty(property.UI.Label))
                        property.UI.Label = property.Name.Humanize();

                    if (property.UI.Order == 0)
                        property.UI.Order = model.Properties.Values.ToList().IndexOf(property) + 100;

                    if (string.IsNullOrEmpty(property.UI.Field))
                    {
                        property.UI.FieldEval = $"'{property.Name.Camelize()}'";
                        property.UI.NameEval = $"'{property.Name.Camelize()}'";
                    }
                    else
                    {
                        if (property.UI.Field.Contains('.'))
                        {
                            property.UI.FieldEval = $"(m: {model.Name}) => m.{property.UI.Field.Split('.').Select(s => s.Camelize()).Aggregate((a, b) => $"{a}.{b}")}";
                            property.UI.NameEval = $"'{property.UI.Field.Split('.').Select(s => s.Camelize()).Aggregate((a, b) => $"{a}.{b}")}'";
                        }
                        else
                        {
                            property.UI.FieldEval = $"'{property.UI.Field.Camelize()}'";
                            property.UI.NameEval = $"'{property.UI.Field.Camelize()}'";
                        }
                    }
                }
            }
        }
    }
}
