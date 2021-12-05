using System.Collections.Generic;

namespace Generator.Metadata
{
    public class CommandDefinition
    {
        public string ModelName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<CommandPropertiesDefinition> Properties { get; set; }
        public List<string> Ignore { get; set; }
    }

    public class CommandPropertiesDefinition
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public List<CommandPropertyValidationDefinition> Validations { get; set; }
        public List<CommandPropertiesDefinition> Properties { get; set; }
    }

    public class CommandPropertyValidationDefinition
    {
        public string Type { get; set; }
        public List<string> Values { get; set; }
    }
}
