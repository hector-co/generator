using CommandLine;
using System.Collections.Generic;

namespace Generator.Cli
{
    public class GenOptions
    {
        [Option('f', "file")]
        public string File { get; set; }

        [Option('d', "output directory")]
        public string OutputDir { get; set; }

        [Option('r', "force-regen")]
        public bool ForceRegen { get; set; }

        [Option('g', "generate", Default = new[] { "*" }, HelpText = "ModelName:(d)omain,(q)uery,(da)taaccess,(a)pi")]
        public IEnumerable<string> Generate { get; set; }

        //[Option('s', "skip", Default = new[] { "" })]
        //public IEnumerable<string> Skip { get; set; }
    }
}
