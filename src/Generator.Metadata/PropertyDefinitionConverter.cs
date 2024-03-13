using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Generator.Metadata
{
    public class PropertyDefinitionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            PropertyDefinition property;
            if (reader.Value != null)
            {
                if (TryGetProperty(reader.Value.ToString(), out property))
                {
                    return property;
                }
            }
            var jObject = JObject.Load(reader);
            return TryGetProperty(jObject, out property) ? property : null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static bool TryGetProperty(string typeName, out PropertyDefinition propertyDefinition)
        {
            propertyDefinition = new PropertyDefinition
            {
                TypeName = typeName
            };
            return true;
        }

        private bool TryGetProperty(JObject jObject, out PropertyDefinition propertyDefinition)
        {
            var typeName = jObject["typeName"];
            var filter = jObject["filter"]?.ToObject<FilterDefinition>();
            var pluralName = jObject["pluralName"];
            var withMany = jObject["withMany"];
            var required = jObject["required"];
            var size = jObject["size"];
            var dbType = jObject["dbType"];
            var ui = jObject["ui"]?.ToObject<UiDefinition>();
            if (typeName == null)
            {
                propertyDefinition = null;
                return false;
            }
            if (!TryGetProperty(((dynamic)typeName).Value.ToString(), out propertyDefinition))
            {
                propertyDefinition = null;
                return false;
            }
            if (required != null && required.Type == JTokenType.Boolean)
            {
                propertyDefinition.Required = required.Value<bool>();
            }
            if (size != null && size.Type == JTokenType.Integer)
            {
                propertyDefinition.Size = size.Value<int>();
            }
            if (dbType != null && dbType.Type == JTokenType.String)
            {
                propertyDefinition.DbType = dbType.Value<string>();
            }
            if (filter != null) propertyDefinition.Filter = filter;
            if (ui != null) propertyDefinition.UI = ui;
            if (pluralName != null) propertyDefinition.PluralName = pluralName.Value<string>();
            if (withMany != null) propertyDefinition.WithMany = withMany.Value<bool>();
            return true;
        }
    }
}
