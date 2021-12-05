using System.Collections.Generic;

namespace Generator.Metadata
{
    public class Settings
    {
        private string _rootBaseClass = "";

        public string DatabaseSchema { get; set; } = "public";

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

        public string DomainModelFolder { get; set; } = "Domain/Model";
        public string DomainModelNamespace { get; set; } = "Domain.Model";
        public string QueriesFolder { get; set; } = "Queries";
        public string QueriesNamespace { get; set; } = "Queries";
        public string DataAccessEfFolder { get; set; } = "DataAccess.Ef";
        public string DataAccessEfNamespace { get; set; } = "DataAccess.Ef";
        public string ApiControllersFolder { get; set; } = "Api/Controllers";
        public string ApiControllersNamespace { get; set; } = "Api.Controllers";
    }
}
