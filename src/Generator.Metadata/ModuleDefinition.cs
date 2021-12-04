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
        }
    }
}
