using CommandLine;
using CommandLine.Text;
using Generator.FilesGeneration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Generator.Cli
{
    class Program
    {
        private static void GenerateFiles(GenOptions options)
        {
            var file = string.IsNullOrEmpty(options.File)
                ? Environment.CurrentDirectory
                : options.File;
            if (!file.EndsWith("json", StringComparison.InvariantCultureIgnoreCase))
                file += "/metadata.json";

            var tplOpts = new Dictionary<string, TemplateGenerationOption>();

            foreach (var modelGenOpts in options.Generate)
            {
                if (modelGenOpts == "*" || modelGenOpts == "*:*")
                {
                    tplOpts.Add("*", new TemplateGenerationOption
                    {
                        Domain = true,
                        Command = true,
                        Query = true,
                        DataAccessEf = true,
                        Api = true
                    });
                }
                else
                {
                    var modelOptions = modelGenOpts.Split(':');
                    if (modelOptions.Length != 2)
                        throw new FormatException(modelGenOpts);

                    tplOpts.Add(modelOptions[0], FromString(modelOptions[1]));
                }
            }

            var outputDir = string.IsNullOrEmpty(options.OutputDir)
                ? Path.GetDirectoryName(file)
                : options.OutputDir;
            new FileGenerator(file, tplOpts, outputDir, options.ForceRegen).Generate();
        }

        public static TemplateGenerationOption FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new TemplateGenerationOption();

            var result = new TemplateGenerationOption
            {
                DataAccessEf = VerifyAndRemove(ref value, "da"),
                Domain = VerifyAndRemove(ref value, "d"),
                Command = VerifyAndRemove(ref value, "c"),
                Query = VerifyAndRemove(ref value, "q"),
                Api = VerifyAndRemove(ref value, "a")
            };
            return result;

            static bool VerifyAndRemove(ref string value, string option)
            {
                if (value == "*")
                    return true;

                if (value.Contains(option))
                {
                    value.Replace(option, "");
                    return true;
                }
                return false;
            }
        }

        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<GenOptions>(args);

            result.WithParsed(o =>
                {
                    try
                    {
                        GenerateFiles(o);
                        Console.WriteLine("Generation completed");
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("File not found");
                        Console.WriteLine(HelpText.AutoBuild(result, x => x, x => x));
                    }
                    catch
                    {
                        Console.WriteLine(HelpText.AutoBuild(result, x => x, x => x));
                    }
                });
        }
    }
}
