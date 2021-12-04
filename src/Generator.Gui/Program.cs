using System;
using System.IO;
using System.Windows.Forms;

namespace Generator.Gui
{
    internal static class Program
    {
        const string FileName = "metadata.json";
        const int MaxDepth = 3;


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            TryGetMetadataFilePath(Environment.CurrentDirectory, out var file);

            Application.Run(new Form1(file));
        }

        private static bool TryGetMetadataFilePath(string basePath, out string metadataFilePath, int level = 1)
        {
            metadataFilePath = "";

            var filePath = $"{basePath}\\{FileName}";
            if (File.Exists(filePath))
            {
                metadataFilePath = filePath;
                return true;
            }

            if (level == MaxDepth)
                return false;

            basePath = Path.GetFullPath(Path.Combine(basePath, ".."));
            return TryGetMetadataFilePath(basePath, out metadataFilePath, level + 1);
        }
    }
}