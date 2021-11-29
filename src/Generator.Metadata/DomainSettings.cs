using System.Collections.Generic;

namespace Generator.Metadata
{
    public class DomainSettings
    {
        private string _rootBaseClass = "";

        public bool GenerateIdProperties { get; set; } = true;
        public string RootBaseClass
        {
            get
            {
                return string.IsNullOrEmpty(_rootBaseClass) ? EntityBaseClass : _rootBaseClass;
            }
            set
            {
                _rootBaseClass = value;
            }
        }
        public string EntityBaseClass { get; set; }
        public List<string> EntityUsings { get; set; } = new List<string>();
    }
}
