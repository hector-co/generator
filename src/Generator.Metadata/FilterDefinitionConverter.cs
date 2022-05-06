using Newtonsoft.Json;
using System;

namespace Generator.Metadata
{
    public class FilterDefinitionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var filter = new FilterDefinition();
            if (reader.Value != null)
            {
                bool apply;
                if (bool.TryParse(reader.Value.ToString(), out apply))
                {
                    filter.Apply = apply;
                    return filter;
                }
                FilterType filterType;
                if (Enum.TryParse(reader.Value.ToString(), true, out filterType))
                {
                    filter.Apply = true;
                    filter.Type = filterType;
                    return filter;
                }
            }

            existingValue = existingValue ?? serializer.ContractResolver.ResolveContract(objectType).DefaultCreator();
            serializer.Populate(reader, existingValue);
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
