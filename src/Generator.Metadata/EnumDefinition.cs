using System.Collections.Generic;

namespace Generator.Metadata
{
    public class EnumDefinition
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }

        public void Init(string name)
        {
            Name = name;
        }
    }
}
