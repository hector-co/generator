﻿using Generator.Metadata;

namespace Generator.Templates.DataAccessEf
{
    public partial class DeleteHandlerTemplate
    {
        private readonly ModuleDefinition _module;
        private readonly ModelDefinition _model;

        public DeleteHandlerTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
        {
            _module = moduleDefinition;
            _model = modelDefinition;
        }
    }
}
