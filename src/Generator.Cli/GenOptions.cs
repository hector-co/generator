using CommandLine;
using System.Collections.Generic;

namespace Generator.Cli
{
    public class GenOptions
    {
        [Option('f', "file", Default = ".")]
        public string File { get; set; }

        [Option('o', "output directory", Default = "")]
        public string OutputDir { get; set; }

        [Option('r', "force-regen", Default = false)]
        public bool ForceRegen { get; set; }

        [Option('g', "generate", Default = new[] { "*" }, HelpText = "ModelName:(d)omain,(c)ommand,(q)uery,(da)taaccess,(a)pi")]
        public IEnumerable<string> Generate { get; set; }

        //[Option('s', "skip", Default = new[] { "" })]
        //public IEnumerable<string> Skip { get; set; }
    }
}
