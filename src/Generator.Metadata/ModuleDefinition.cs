using Humanizer;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Metadata
{
    public class ModuleDefinition
    {
        private string _apiPrefix;
        public string Name { get; set; }
        public string ApiPrefix
        {
            set => _apiPrefix = value;
            get => !string.IsNullOrEmpty(_apiPrefix)
                      ? _apiPrefix
                      : Name.Camelize();
        }

        public string DatabaseSchema { get; set; }
        public DomainSettings DomainSettings { get; set; } = new DomainSettings();
        public Dictionary<string, ModelDefinition> Models { get; set; } = new Dictionary<string, ModelDefinition>();
        public Dictionary<string, EnumDefinition> Enums { get; set; } = new Dictionary<string, EnumDefinition>();

        public IEnumerable<ModelDefinition> RootModels => Models.Values.Where(m => m.IsRoot);
        public IEnumerable<ModelDefinition> EntityModels => Models.Values.Where(m => m.IsEntity);
        public IEnumerable<ModelDefinition> OwnedEntityModels => Models.Values.Where(m => m.IsOwnedEntity);
        public IEnumerable<ModelDefinition> ValueObjectModels => Models.Values.Where(m => m.IsValueObject);

        public void Init()
        {
            foreach (var modelName in Models.Keys)
                Models[modelName].Init(this, modelName);

            foreach (var modelName in Models.Keys)
                Models[modelName].InitProperties(this);

            foreach (var modelName in Models.Keys)
                Models[modelName].AdjustRelationships(this);

            foreach (var enumName in Enums.Keys)
                Enums[enumName].Init(enumName);
        }
    }
}
