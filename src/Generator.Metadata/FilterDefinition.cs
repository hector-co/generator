using Newtonsoft.Json;

namespace Generator.Metadata
{
    public enum FilterType
    {
        Default,
        Equals,
        Range,
        Multiple,
        Contains
    }

    [JsonConverter(typeof(FilterDefinitionConverter))]
    public class FilterDefinition
    {
        public bool Apply { get; set; }
        public FilterType Type { get; set; }

        public string FilterTypeName
        {
            get
            {
                switch (Type)
                {
                    case FilterType.Equals:
                        return "EqualsFilterProperty";
                    case FilterType.Range:
                        return "RangeFilterProperty";
                    case FilterType.Multiple:
                        return "InFilterProperty";
                    case FilterType.Contains:
                        return "ContainsFilterProperty";
                    default:
                        return "FilterProperty";
                }
            }
        }
    }
}
