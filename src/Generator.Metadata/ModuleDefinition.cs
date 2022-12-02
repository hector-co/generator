using System.Collections.Generic;
using System.Linq;

namespace Generator.Metadata
{
    public class ModuleDefinition
    {
        public ModuleDefinition()
        {
            Settings = new Settings();
        }

        public string Name { get; set; }

        public Settings Settings { get; set; } = new Settings();
        public Dictionary<string, ModelDefinition> Model { get; set; } = new Dictionary<string, ModelDefinition>();
        public Dictionary<string, EnumDefinition> Enums { get; set; } = new Dictionary<string, EnumDefinition>();

        public IEnumerable<ModelDefinition> RootModels => Model.Values.Where(m => m.IsRoot);
        public IEnumerable<ModelDefinition> EntityModels => Model.Values.Where(m => m.IsEntity);
        public IEnumerable<ModelDefinition> OwnedEntityModels => Model.Values.Where(m => m.IsOwnedEntity);
        public IEnumerable<ModelDefinition> ValueObjectModels => Model.Values.Where(m => m.IsValueObject);

        public void Init()
        {
            foreach (var modelName in Model.Keys)
                Model[modelName].Init(this, modelName);

            foreach (var modelName in Model.Keys)
                Model[modelName].InitProperties(this);

            foreach (var modelName in Model.Keys)
                Model[modelName].AdjustRelationships(this);

            foreach (var enumName in Enums.Keys)
                Enums[enumName].Init(enumName);

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
    }
}
