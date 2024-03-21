using System.Text.Json.Serialization;

namespace Generator.Metadata;

public class UiDefinition
{
    public string Label { get; set; }
    public bool HideInList { get; set; }
    public int Order { get; set; }
    public bool Sortable { get; set; } = true;
    public string Align { get; set; } = "left";
    public string Width { get; set; }
    public string Field { get; set; }

    public string HeaderStyle =>
        string.IsNullOrEmpty(Width)
            ? ""
            : $"width: {Width};";

    [JsonIgnore]
    public string NameEval { get; set; }

    [JsonIgnore]
    public string FieldEval { get; set; }
}
