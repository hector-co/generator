using Generator.Metadata;
using System.Linq;

namespace Generator.Templates.Queries
{
    public partial class EnumTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly EnumDefinition _enum;

        public EnumTemplate(ModuleDefinition moduleDefinition, EnumDefinition enumDefinition)
        {
            _module = moduleDefinition;
            _enum = enumDefinition;
        }

        public ModelDefinition GetParentDto()
        {
            foreach (var model in _module.Model.Values)
            {
                if (model.Properties.Any(p => p.Value.IsEnumType && p.Value.CastTargetType<EnumTypeDefinition>().Enum == _enum))
                    return model;
            }
            return null;
        }
    }
}
