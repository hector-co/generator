using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class ModelConfigurationTemplate
    {
        public static string GetDataAccessNameSpace(ModelDefinition modelDefinition, string @namespace)
        {
            return $"{@namespace}.DataAccess.Ef.{modelDefinition.PluralName}";
        }
    }
}
