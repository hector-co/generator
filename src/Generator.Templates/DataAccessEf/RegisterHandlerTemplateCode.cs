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

        public List<PropertyInfo> GetScalarPropertiesInfo(ModelDefinition modelDefinition, string prefix = "request")
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
                    NameOverride = $"{prefix}.{p.Name}.Adapt<{p.TypeName}>()"
                }));
            result.AddRange(modelDefinition.Properties.Values
                .Where(p => p.IsRootType).Select(p => new PropertyInfo
                {
                    Name = p.Name + (p.IsCollection
                        ? ""
                        : p.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.Name),
                    NameOverride = p.IsCollection
                        ? $"await _context.Set<{p.CastTargetType<ModelTypeDefinition>().Model.Name}>().Where(er => {prefix}.{p.Name}{p.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.Name}.Contains(er.{p.CastTargetType<ModelTypeDefinition>().Model.IdentifierProperty.Name})).ToListAsync(cancellationToken)"
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
