using Generator.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Templates.DataAccessEf
{
    public partial class RegisterHandlerTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public RegisterHandlerTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }

        public List<PropertyInfo> GetScalarPropertiesInfo(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();
            result.AddRange(modelDefinition.Properties.Values
                .Where(p => p.IsSystemType || p.IsEnumType).Select(p => new PropertyInfo
                {
                    Name = p.Name
                }));
            result.AddRange(modelDefinition.Properties.Values
                .Where(p => p.IsValueObjectType).Select(p => new PropertyInfo
                {
                    Name = p.Name,
                    NameSuffix = $".Adapt<{p.TypeName}>()"
                }));
            result.AddRange(modelDefinition.Properties.Values
                .Where(p => p.IsRootType).Select(p => new PropertyInfo
                {
                    Name = p.Name + (p.IsCollection
                        ? ""
                        : p.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.Name),
                    NameSuffix = p.IsCollection
                        ? $"{p.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.Name}.SelectMany(id => _context.Set<{p.CastTargetType<ModelTypeDefinition>().Model.Name}>().Where(r => r.Id == id)).ToList()"
                        : ""
                }));
            return result;
        }

        public Dictionary<string, ModelDefinition> GetSingleOwnedEntities(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            return modelDefinition.Properties.Values
                .Where(p => !p.IsCollection && p.IsEntityType && p.IsOwnedEntity).ToDictionary(p => p.Name, p => p.CastTargetType<ModelTypeDefinition>().Model);
        }

        public Dictionary<string, ModelDefinition> GetCollectionOwnedEntities(ModelDefinition modelDefinition)
        {
            var result = new List<PropertyInfo>();

            return modelDefinition.Properties.Values
                .Where(p => p.IsCollection && p.IsEntityType && p.IsOwnedEntity).ToDictionary(p => p.Name, p => p.CastTargetType<ModelTypeDefinition>().Model);
        }
    }
}
