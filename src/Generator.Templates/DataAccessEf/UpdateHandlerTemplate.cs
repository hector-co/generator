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
    using Generator.Templates.Domain;
    using Generator.Templates.Commands;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class UpdateHandlerTemplate : UpdateHandlerTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using Mapster;\r\nusing ");
            
            #line 4 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.Name));
            
            #line default
            #line hidden
            this.Write(".Commands;\r\nusing Microsoft.EntityFrameworkCore;\r\nusing ");
            
            #line 6 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetDomainModelNamespace()));
            
            #line default
            #line hidden
            this.Write(";\r\nusing ");
            
            #line 7 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetCommandsNamespace(_model)));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 9 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetDataAccessModelNamespace(_model)));
            
            #line default
            #line hidden
            this.Write(".Commands;\r\n\r\npublic class ");
            
            #line 11 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetUpdateCommandClassName()));
            
            #line default
            #line hidden
            this.Write("Handler : ICommandHandler<");
            
            #line 11 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetUpdateCommandClassName()));
            
            #line default
            #line hidden
            this.Write(">\r\n{\r\n    private readonly ");
            
            #line 13 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetContextName()));
            
            #line default
            #line hidden
            this.Write(" _context;\r\n\r\n    public ");
            
            #line 15 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetUpdateCommandClassName()));
            
            #line default
            #line hidden
            this.Write("Handler(");
            
            #line 15 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetContextName()));
            
            #line default
            #line hidden
            this.Write(" context)\r\n    {\r\n        _context = context;\r\n    }\r\n\r\n    public async Task<Res" +
                    "ponse> Handle(");
            
            #line 20 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetUpdateCommandClassName()));
            
            #line default
            #line hidden
            this.Write(" request, CancellationToken cancellationToken)\r\n    {\r\n        var ");
            
            #line 22 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name.GetVariableName()));
            
            #line default
            #line hidden
            this.Write(" = await _context.Set<");
            
            #line 22 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write(">()\r\n");
            
            #line 23 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
    if (QueryableExtensionsTemplate.RequiresIncludes(_model))
{ 
            
            #line default
            #line hidden
            this.Write("            .AddIncludes()\r\n");
            
            #line 26 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
    } 
            
            #line default
            #line hidden
            this.Write("            .FirstOrDefaultAsync(m => m.");
            
            #line 27 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.IdentifierProperty.Name));
            
            #line default
            #line hidden
            this.Write(" == request.");
            
            #line 27 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.IdentifierProperty.Name));
            
            #line default
            #line hidden
            this.Write(", cancellationToken);\r\n\r\n        if (");
            
            #line 29 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name.GetVariableName()));
            
            #line default
            #line hidden
            this.Write(" == null)\r\n            return Response.Failure(new Error(\"");
            
            #line 30 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write(".Update.NotFound\", \"Entity not found.\"));\r\n\r\n");
            
            #line 32 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
  foreach(var propInfo in GetScalarPropertiesInfo(_model))
{ 
            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 34 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name.GetVariableName()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 34 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propInfo.Name));
            
            #line default
            #line hidden
            this.Write(" = ");
            
            #line 34 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.IsNullOrEmpty(propInfo.NameOverride)? "request." + propInfo.Name : propInfo.NameOverride));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 35 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
  } 
            
            #line default
            #line hidden
            
            #line 36 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
  foreach(var entity in GetSingleOwnedEntities(_model))
{ 
            
            #line default
            #line hidden
            
            #line 38 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
      foreach(var propInfo in GetScalarPropertiesInfo(entity.Value))
    { 
            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 40 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name.GetVariableName()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 40 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Key));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 40 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propInfo.Name));
            
            #line default
            #line hidden
            this.Write(" = ");
            
            #line 40 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.IsNullOrEmpty(propInfo.NameOverride)? "request." + entity.Key + "." + propInfo.Name : propInfo.NameOverride));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 41 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
      } 
            
            #line default
            #line hidden
            
            #line 42 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
  } 
            
            #line default
            #line hidden
            
            #line 43 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
  foreach(var entity in GetCollectionOwnedEntities(_model))
{ 
            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 45 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name.GetVariableName()));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 45 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Key));
            
            #line default
            #line hidden
            this.Write(" = request.");
            
            #line 45 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Key));
            
            #line default
            #line hidden
            this.Write(".Select(r => new ");
            
            #line 45 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Value.Name));
            
            #line default
            #line hidden
            this.Write("\r\n        {\r\n            ");
            
            #line 47 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Value.IdentifierProperty.Name));
            
            #line default
            #line hidden
            this.Write(" = r.");
            
            #line 47 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Value.IdentifierProperty.Name));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 48 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
      foreach(var propInfo in GetScalarPropertiesInfo(entity.Value, "r"))
    { 
            
            #line default
            #line hidden
            this.Write("            ");
            
            #line 50 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(propInfo.Name));
            
            #line default
            #line hidden
            this.Write(" = ");
            
            #line 50 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.IsNullOrEmpty(propInfo.NameOverride)? "r." + propInfo.Name : propInfo.NameOverride));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 51 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
      } 
            
            #line default
            #line hidden
            this.Write("        }).ToList();\r\n");
            
            #line 53 "D:\Users\Hector\projects\generatorv2\src\Generator.Templates\DataAccessEf\UpdateHandlerTemplate.tt"
  } 
            
            #line default
            #line hidden
            this.Write("\r\n        await _context.SaveChangesAsync(cancellationToken);\r\n        return Res" +
                    "ponse.Success();\r\n    }\r\n}");
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
    public class UpdateHandlerTemplateBase
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
