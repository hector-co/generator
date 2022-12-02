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
        public List<string> CommandUsings { get; set; } = new List<string>
        {
            "{0}.Application.Abstractions.Commands"
        };
        public List<string> QueryUsings { get; set; } = new List<string>
        {
            "{0}.Application.Abstractions.Queries"
        };
        public List<string> CommandHandlerUsings { get; set; } = new List<string>
        {
            "{0}.Domain.Abstractions",
            "{0}.Application.Abstractions.Commands"
        };
        public List<string> QueryHandlerUsings { get; set; } = new List<string>
        {
            "{0}.Application.Abstractions.Queries"
        };

        public string DomainModelFolder { get; set; } = "Domain/Model";
        public string DomainModelNamespace { get; set; } = "Domain.Model";
        public string CommandsFolder { get; set; } = "Application/Commands";
        public string CommandsNamespace { get; set; } = "Application.Commands";
        public string QueriesFolder { get; set; } = "Application/Queries";
        public string QueriesNamespace { get; set; } = "Application.Queries";
        public string DataAccessEfFolder { get; set; } = "Infrastructure/DataAccess.EF";
        public string DataAccessEfNamespace { get; set; } = "Infrastructure.DataAccess.EF";
        public string ApiControllersFolder { get; set; } = "WebApi/Controllers";
        public string ApiControllersNamespace { get; set; } = "WebApi.Controllers";
    }
}
