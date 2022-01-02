using Humanizer;
using System.Collections.Generic;

namespace Generator.Metadata
{
    public class Settings
    {
        private string _rootBaseClass = "";
        private string _apiPrefix;

        internal string ModuleName { get; set; }
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
        public string ApiPrefix
        {
            set => _apiPrefix = value;
            get => !string.IsNullOrEmpty(_apiPrefix)
                      ? _apiPrefix
                      : ModuleName.Camelize();
        }

        public string EntityBaseClass { get; set; }
        public List<string> EntityUsings { get; set; } = new List<string>();

        public string DomainModelFolder { get; set; } = "Domain/Model";
        public string DomainModelNamespace { get; set; } = "Domain.Model";
        public string CommandsFolder { get; set; } = "Commands";
        public string CommandsNamespace { get; set; } = "Commands";
        public string QueriesFolder { get; set; } = "Queries";
        public string QueriesNamespace { get; set; } = "Queries";
        public string DataAccessEfFolder { get; set; } = "DataAccess.Ef";
        public string DataAccessEfNamespace { get; set; } = "DataAccess.Ef";
        public string ApiControllersFolder { get; set; } = "Api/Controllers";
        public string ApiControllersNamespace { get; set; } = "Api.Controllers";
    }
}
