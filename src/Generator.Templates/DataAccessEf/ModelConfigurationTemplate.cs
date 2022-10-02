﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Generator.Templates.DataAccessEf
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Generator.Metadata;
    using Generator.Templates.Domain;
    using Humanizer;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class ModelConfigurationTemplate : ModelConfigurationTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using Microsoft.EntityFrameworkCore;\r\nusing Microsoft.EntityFrameworkCore.Metadat" +
                    "a.Builders;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing ");
            
            #line 13 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetDomainModelNamespace()));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 15 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetDataAccessModelNamespace(_model)));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\npublic class ");
            
            #line 17 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("Configuration : IEntityTypeConfiguration<");
            
            #line 17 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write(">\r\n{\r\n    private readonly string _dbSchema;\r\n\r\n    public ");
            
            #line 21 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("Configuration(string dbSchema)\r\n    {\r\n        _dbSchema = dbSchema;\r\n    }\r\n\r\n  " +
                    "  public void Configure(EntityTypeBuilder<");
            
            #line 26 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("> builder)\r\n    {\r\n        builder.ToTable(\"");
            
            #line 28 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("\", _dbSchema);\r\n");
            
            #line 29 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    foreach(var propertyName in GetIgnoredPropertyNames(_model))
{ 
            
            #line default
            #line hidden
            this.Write("        builder.Ignore(m => m.");
            
            #line 31 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 32 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    } 
            
            #line default
            #line hidden
            
            #line 33 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    foreach(var propertyName in GetSerializedPropertyNames(_model))
{ 
            
            #line default
            #line hidden
            this.Write("        builder.Property(m => m.");
            
            #line 35 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write("_Serialized);\r\n");
            
            #line 36 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    } 
            
            #line default
            #line hidden
            
            #line 37 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    foreach(var property in _model.Properties.Values.Where(p => !p.IsCollection && (p.Size.HasValue || (p.Required.HasValue && p.Required.Value)) || !string.IsNullOrEmpty(p.DbType)))
{ 
            
            #line default
            #line hidden
            this.Write("        builder.Property(m => m.");
            
            #line 39 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 40 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        if(property.Required.HasValue && property.Required.Value) 
    { 
            
            #line default
            #line hidden
            this.Write("            .IsRequired()");
            
            #line 42 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Size.HasValue? "" : ";"));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 43 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        } 
            
            #line default
            #line hidden
            
            #line 44 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        if(!string.IsNullOrEmpty(property.DbType)) 
    { 
            
            #line default
            #line hidden
            this.Write("            .HasColumnType(\"");
            
            #line 46 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.DbType));
            
            #line default
            #line hidden
            this.Write("\")");
            
            #line 46 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Size.HasValue? "" : ";"));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 47 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        } 
            
            #line default
            #line hidden
            
            #line 48 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        if(property.Size.HasValue) 
    { 
            
            #line default
            #line hidden
            this.Write("            .HasMaxLength(");
            
            #line 50 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Size.Value));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 51 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        } 
            
            #line default
            #line hidden
            
            #line 52 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    } 
            
            #line default
            #line hidden
            
            #line 53 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    foreach(var propertyName in GetNonGenericEntitiesPropertyNames(_model))
{ 
            
            #line default
            #line hidden
            this.Write("        builder.HasOne(m => m.");
            
            #line 55 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write(")\r\n            .WithMany()\r\n            .HasForeignKey(r => r.");
            
            #line 57 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propertyName));
            
            #line default
            #line hidden
            this.Write("Id);\r\n");
            
            #line 58 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    } 
            
            #line default
            #line hidden
            
            #line 59 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    foreach(var property in GetManyToOneProperties(_model))
{ 
            
            #line default
            #line hidden
            this.Write("        builder.HasMany(m => m.");
            
            #line 61 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n            .WithOne()\r\n");
            
            #line 63 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        if (_model.HasMultiplePropertiesWithModelType(property.CastTargetType<ModelTypeDefinition>().Model, property.IsGeneric))
    { 
            
            #line default
            #line hidden
            this.Write("            .HasForeignKey(r => r.");
            
            #line 65 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            
            #line 65 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("Id);\r\n");
            
            #line 66 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        } 
            
            #line default
            #line hidden
            
            #line 67 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        else
    { 
            
            #line default
            #line hidden
            this.Write("            .HasForeignKey(r => r.");
            
            #line 69 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("Id);\r\n");
            
            #line 70 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        } 
            
            #line default
            #line hidden
            
            #line 71 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    } 
            
            #line default
            #line hidden
            
            #line 72 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    foreach(var property in _model.Properties.Values.Where(p => p.WithMany))
{ 
            
            #line default
            #line hidden
            this.Write("        builder.HasMany(m => m.");
            
            #line 74 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n            .WithMany(r => r.");
            
            #line 75 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture($"{_model.Name}{property.Name}"));
            
            #line default
            #line hidden
            this.Write(")\r\n            .UsingEntity<Dictionary<string, object>>(\r\n                \"");
            
            #line 77 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture($"{_model.Name}{property.Name.Singularize()}"));
            
            #line default
            #line hidden
            this.Write("\",\r\n                j => j\r\n                    .HasOne<");
            
            #line 79 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.CastTargetType<ModelTypeDefinition>().Model.Name));
            
            #line default
            #line hidden
            this.Write(">()\r\n                    .WithMany()\r\n                    .HasForeignKey(\"");
            
            #line 81 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name.Singularize()));
            
            #line default
            #line hidden
            this.Write("Id\"),\r\n                j => j\r\n                    .HasOne<");
            
            #line 83 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write(">()\r\n                    .WithMany()\r\n                    .HasForeignKey(\"");
            
            #line 85 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("Id\"));\r\n");
            
            #line 86 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    } 
            
            #line default
            #line hidden
            
            #line 87 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    foreach(var property in _model.Properties.Values.Where(p => p.IsValueObjectType))
{ 
            
            #line default
            #line hidden
            
            #line 89 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        if (property.IsCollection)
        { 
            
            #line default
            #line hidden
            this.Write("        builder.OwnsMany(m => m.");
            
            #line 91 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n            .ToTable(\"");
            
            #line 92 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            
            #line 92 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("\", _dbSchema)\r\n            .WithOwner().HasForeignKey(\"");
            
            #line 93 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("Id\");\r\n");
            
            #line 94 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        } 
            
            #line default
            #line hidden
            
            #line 95 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        else if (!property.IsGeneric)
        { 
            
            #line default
            #line hidden
            this.Write("        builder.OwnsOne(m => m.");
            
            #line 97 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n            .ToTable(\"");
            
            #line 98 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            
            #line 98 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("\", _dbSchema)\r\n            .WithOwner().HasForeignKey(\"");
            
            #line 99 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("Id\");\r\n");
            
            #line 100 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
        } 
            
            #line default
            #line hidden
            
            #line 101 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\ModelConfigurationTemplate.tt"
    } 
            
            #line default
            #line hidden
            this.Write("    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class ModelConfigurationTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
