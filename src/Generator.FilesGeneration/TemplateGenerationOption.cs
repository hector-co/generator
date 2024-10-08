﻿namespace Generator.FilesGeneration
{
    public class TemplateGenerationOption
    {
        public bool Domain { get; set; }
        public bool Command { get; set; }
        public bool Query { get; set; }
        public bool DataAccessEf { get; set; }
        public bool Api { get; set; }
        public bool WebUI { get; set; }

        public bool AnyChecked()
        {
            return Domain || Command || Query || DataAccessEf || Api || WebUI;
        }
    }
}