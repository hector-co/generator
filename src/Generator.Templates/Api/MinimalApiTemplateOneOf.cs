﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Generator.Templates.Api
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Generator.Templates.Queries;
    using Generator.Templates.Commands;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class MinimalApiTemplateOneOf : MinimalApiTemplateOneOfBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Shared.Application.Queries;

using ");
            
            #line 17 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetDtoNamespace(_model)));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 18 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
  if (_generateCommands) 
    { 
            
            #line default
            #line hidden
            this.Write("using ");
            
            #line 20 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetCommandsNamespace(_model)));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 21 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
  } 
            
            #line default
            #line hidden
            this.Write("\r\nnamespace ");
            
            #line 23 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.GetModuleNamespace()));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\npublic class ");
            
            #line 25 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.PluralName));
            
            #line default
            #line hidden
            this.Write("Module : ICarterModule\r\n{\r\n    public void AddRoutes(IEndpointRouteBuilder app)\r\n" +
                    "    {\r\n        var group = app.MapGroup(\"");
            
            #line 29 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_module.Settings.ApiPrefix));
            
            #line default
            #line hidden
            this.Write("/");
            
            #line 29 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetModuleRouteName()));
            
            #line default
            #line hidden
            this.Write("\")\r\n            .WithTags(\"");
            
            #line 30 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.PluralName));
            
            #line default
            #line hidden
            this.Write("\");\r\n        group.MapGet(\"/{id}\", GetById).WithName(\"Get");
            
            #line 31 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("ById\");\r\n        group.MapGet(\"/\", List);\r\n");
            
            #line 33 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
  if (_generateCommands) 
{ 
            
            #line default
            #line hidden
            this.Write("        group.MapPost(\"/\", Register);\r\n        group.MapPut(\"/{id}\", Update);\r\n  " +
                    "      group.MapDelete(\"/{id}\", Delete);\r\n");
            
            #line 38 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
 }
            
            #line default
            #line hidden
            this.Write("    }\r\n\r\n    public static async Task<Results<Ok<Result<");
            
            #line 41 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetDtoName()));
            
            #line default
            #line hidden
            this.Write(">>, NotFound<string>>> GetById(");
            
            #line 41 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.IdentifierProperty.TypeName));
            
            #line default
            #line hidden
            this.Write(" id, ISender sender, CancellationToken cancellationToken)\r\n    {\r\n        var get" +
                    "ByIdQuery = new ");
            
            #line 43 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetDtoByIdClassName()));
            
            #line default
            #line hidden
            this.Write("(id);\r\n        var result = await sender.Send(getByIdQuery, cancellationToken);\r\n" +
                    "        if (result.Data == null) return TypedResults.NotFound(\"");
            
            #line 45 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write(" not found\");\r\n        return TypedResults.Ok(result);\r\n    }\r\n\r\n    public stati" +
                    "c async Task<Ok<Result<");
            
            #line 49 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetDtoName()));
            
            #line default
            #line hidden
            this.Write("[]>>> List([AsParameters] ");
            
            #line 49 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.ListDtoClassName()));
            
            #line default
            #line hidden
            this.Write(" query, ISender sender, CancellationToken cancellationToken)\r\n    {\r\n        var " +
                    "result = await sender.Send(query, cancellationToken);\r\n        return TypedResul" +
                    "ts.Ok(result);\r\n    }\r\n");
            
            #line 54 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
  if (_generateCommands) 
{ 
            
            #line default
            #line hidden
            this.Write("\r\n    public static async Task<CreatedAtRoute<Result<");
            
            #line 57 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetDtoName()));
            
            #line default
            #line hidden
            this.Write(">>> Register([FromBody] ");
            
            #line 57 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetRegisterCommandClassName()));
            
            #line default
            #line hidden
            this.Write(" command, ISender sender, CancellationToken cancellationToken)\r\n    {\r\n        va" +
                    "r response = await sender.Send(command, cancellationToken);\r\n        response.Ve" +
                    "rify();\r\n        var model = await sender.Send(new ");
            
            #line 61 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetDtoByIdClassName()));
            
            #line default
            #line hidden
            this.Write("(response.AsT0.Value), cancellationToken);\r\n        return TypedResults.CreatedAt" +
                    "Route(model, \"Get");
            
            #line 62 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.Name));
            
            #line default
            #line hidden
            this.Write("ById\", new { id = response.Value });\r\n    }\r\n\r\n    public static async Task<NoCon" +
                    "tent> Update(");
            
            #line 65 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.IdentifierProperty.TypeName));
            
            #line default
            #line hidden
            this.Write(" id, [FromBody] ");
            
            #line 65 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetUpdateCommandClassName()));
            
            #line default
            #line hidden
            this.Write(@" command, ISender sender, CancellationToken cancellationToken)
    {
        command.Id = id;
        var response = await sender.Send(command, cancellationToken);
        response.Verify();
        return TypedResults.NoContent();
    }

    public static async Task<NoContent> Delete(");
            
            #line 73 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.IdentifierProperty.TypeName));
            
            #line default
            #line hidden
            this.Write(" id, ISender sender, CancellationToken cancellationToken)\r\n    {\r\n        var com" +
                    "mand = new ");
            
            #line 75 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_model.GetDeleteCommandClassName()));
            
            #line default
            #line hidden
            this.Write("(id);\r\n        var response = await sender.Send(command, cancellationToken);\r\n   " +
                    "     response.Verify();\r\n        return TypedResults.NoContent();\r\n    }\r\n");
            
            #line 80 "D:\Users\Hector\source\generator\src\Generator.Templates\Api\MinimalApiTemplateOneOf.tt"
 }
            
            #line default
            #line hidden
            this.Write("}");
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
    public class MinimalApiTemplateOneOfBase
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
        public System.Text.StringBuilder GenerationEnvironment
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
