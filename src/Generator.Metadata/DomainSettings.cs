using System.Collections.Generic;

namespace Generator.Metadata
{
    public class DomainSettings
    {
        public bool GenerateIdProperties { get; set; } = true;
        public string AggregateRootBaseClass { get; set; }
        public string EntityBaseClass { get; set; }
        public List<string> EntityUsings { get; set; } = new List<string>();
    }
}
