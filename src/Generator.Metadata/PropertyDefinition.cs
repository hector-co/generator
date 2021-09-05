using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Generator.Metadata
{
    public class PropertyDefinition
    {
        public const string GenericTypeExp = @"\w+<(.*)?>";
        public static readonly IEnumerable<string> ValidSystemTypes = new[] { "byte", "bool", "int", "long", "float", "double", "char", "string", "DateTime", "DateTimeOffset", "TimeSpan", "object", "DayOfWeek" };

        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool WithMany { get; set; }

        [JsonIgnore]
        public bool IsGeneric { get; private set; }
        [JsonIgnore]
        public bool IsCollection { get; private set; }
        public string TypeName { get; set; }
        [JsonIgnore]
        public string InternalTypeName { get; set; }
        [JsonIgnore]
        public List<TypeDefinition> TargetTypes { get; set; }
        public TypeDefinition TargetType => TargetTypes.First();


        public bool IsSystemType => TargetType is SystemTypeDefinition;
        public bool IsRootType => TargetType is ModelTypeDefinition t0 && t0.Model.IsRoot;
        public bool IsEntityType => TargetType is ModelTypeDefinition t0 && t0.Model.IsEntity;
        public bool IsValueObjectType => TargetType is ModelTypeDefinition t0 && t0.Model.IsValueObject;
        public bool IsEnumType => TargetType is EnumTypeDefinition;

        public T CastTargetType<T>()
           where T : TypeDefinition
        {
            return TargetType.Cast<T>();
        }

        public void Init(ModuleDefinition moduleDefinition, string name)
        {
            Name = name;

            if (!TypeName.Contains("<"))
            {
                TargetTypes = new List<TypeDefinition>
                {
                    GetTypeDefinition (moduleDefinition, TypeName)
                };

                InternalTypeName = TargetType is SystemTypeDefinition
                    ? TypeName : ":T0:";

                return;
            }

            var match = Regex.Match(TypeName, GenericTypeExp);
            if (!match.Success)
                throw new ArgumentException(nameof(TypeName));

            IsGeneric = true;
            IsCollection = TypeName.StartsWith("List<");

            var stype = match.Groups[1].Value;
            if (!stype.Contains(","))
            {
                InternalTypeName = TypeName.Replace(stype, ":T0:");
                TargetTypes = new List<TypeDefinition>
                {
                    GetTypeDefinition (moduleDefinition, stype)
                };
                return;
            }

            TargetTypes = new List<TypeDefinition>();
            var stypes = stype.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var st in stypes)
            {
                TargetTypes.Add(GetTypeDefinition(moduleDefinition, st));
            }
            InternalTypeName = TypeName.Replace(stype, string.Join(", ", Enumerable.Range(0, stypes.Count()).Select(n => $":T{n}:")));
        }

        private static TypeDefinition GetTypeDefinition(ModuleDefinition moduleDefinition, string typeName)
        {
            var nullable = typeName.EndsWith('?');
            typeName = typeName.TrimEnd('?');
            if (ValidSystemTypes.Contains(typeName))
                return new SystemTypeDefinition
                {
                    Name = typeName,
                    IsNullable = nullable
                };
            else if (moduleDefinition.Models.Keys.Contains(typeName))
                return new ModelTypeDefinition
                {
                    Name = typeName,
                    IsNullable = nullable,
                    Model = moduleDefinition.Models[typeName]
                };
            else if (moduleDefinition.Enums.Keys.Contains(typeName))
                return new EnumTypeDefinition
                {
                    Name = typeName,
                    IsNullable = nullable,
                    Enum = moduleDefinition.Enums[typeName]
                };
            else
                throw new ArgumentException(null, nameof(typeName));
        }
    }
}
