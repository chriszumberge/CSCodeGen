using CSCodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SlnGen
{
    /// <summary>
    /// 
    /// </summary>
    public class FileTemplate
    {
        /// <summary>
        /// The template field namespace
        /// </summary>
        public const string TemplateFieldNamespace = "Field::";
        private static string TemplateFieldRegex = $"{{{{{TemplateFieldNamespace}[A-Za-z0-9]*}}}}";

        /// <summary>
        /// Gets or sets the template text.
        /// </summary>
        /// <value>
        /// The template text.
        /// </value>
        public string TemplateText { get; set; } = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTemplate"/> class.
        /// </summary>
        /// <param name="templateText">The template text.</param>
        public FileTemplate(string templateText)
        {
            TemplateText = templateText;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTemplate"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public FileTemplate(ProjectFile file)
        {
            TemplateText = file.FileContents;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTemplate"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public FileTemplate(CGFile file)
        {
            TemplateText = file.ToString();
        }

        /// <summary>
        /// Gets the template field names.
        /// </summary>
        /// <returns>List of template field names.</returns>
        public List<string> GetTemplateFieldNames()
        {
            Regex regex = new Regex(TemplateFieldRegex);
            MatchCollection matches = regex.Matches(TemplateText);
            List<string> fields = new List<string>();
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    string fieldName = match.Value.Substring(TemplateFieldNamespace.Length + 2, match.Value.Length - TemplateFieldNamespace.Length - 4);
                    if (!String.IsNullOrEmpty(fieldName))
                    {
                        fields.Add(fieldName);
                    }
                }
            }
            return fields;
        }

        /// <summary>
        /// Compiles the template with replacers.
        /// </summary>
        /// <param name="replacers">The replacers.</param>
        /// <returns></returns>
        public string CompileTemplateWithReplacers(List<TemplateFieldReplacer> replacers)
        {
            List<string> templateFieldNames = this.GetTemplateFieldNames();
            string compiledText = TemplateText;
            foreach (string templateFieldName in templateFieldNames)
            {
                string templateField = $"{{{{{TemplateFieldNamespace}{templateFieldName}}}}}";
                TemplateFieldReplacer replacer = replacers.FirstOrDefault(r => r.FieldName.Equals(templateFieldName));
                // Default to empty string so if replacer wasn't defined it just removes the template
                string replacementValue = String.Empty;
                if (replacer != null)
                {
                    replacementValue = replacer.ReplacerValue;
                }
                compiledText = compiledText.Replace(templateField, replacementValue);
            }
            return compiledText;
        }
    }


    /// <summary>
    /// Immutable as of now
    /// </summary>
    public class TemplateFieldReplacer
    {
        private KeyValuePair<string, string> mFieldReplacer;

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string FieldName => mFieldReplacer.Key;
        /// <summary>
        /// Gets the replacer value.
        /// </summary>
        /// <value>
        /// The replacer value.
        /// </value>
        public string ReplacerValue => mFieldReplacer.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateFieldReplacer"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="replacerValue">The replacer value.</param>
        public TemplateFieldReplacer(string fieldName, string replacerValue)
        {
            mFieldReplacer = new KeyValuePair<string, string>(fieldName, replacerValue);
        }
    }
}
