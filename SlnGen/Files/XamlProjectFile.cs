using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public class XamlProjectFile
    {
        EmbeddedResourceProjectFile mXamlFile { get; set; }
        /// <summary>
        /// Gets the xaml file.
        /// </summary>
        /// <value>
        /// The xaml file.
        /// </value>
        public EmbeddedResourceProjectFile XamlFile => mXamlFile;

        ProjectFile mXamlCsFile { get; set; }
        /// <summary>
        /// Gets the xaml cs file.
        /// </summary>
        /// <value>
        /// The xaml cs file.
        /// </value>
        public ProjectFile XamlCsFile => mXamlCsFile;

        /// <summary>
        /// Gets or sets the xaml file contents.
        /// </summary>
        /// <value>
        /// The xaml file contents.
        /// </value>
        public string XamlFileContents
        {
            get { return mXamlFile.FileContents; }
            set { mXamlFile.FileContents = value; }
        }

        /// <summary>
        /// Gets or sets the xaml cs file contents.
        /// </summary>
        /// <value>
        /// The xaml cs file contents.
        /// </value>
        public string XamlCsFileContents
        {
            get { return mXamlCsFile.FileContents; }
            set { mXamlCsFile.FileContents = value; }
        }

        public XamlProjectFile(string fileNameWithoutExtension)
        {
            mXamlFile = new EmbeddedResourceProjectFile(String.Concat(fileNameWithoutExtension, ".xaml"), "Designer", "MSBuild:UpdateDesignTimeXaml");

            mXamlCsFile = new ProjectFile(String.Concat(fileNameWithoutExtension, ".xaml.cs"), true)
            {
                DependentUpon = { String.Concat(fileNameWithoutExtension, ".xaml") }
            };
        }
    }
}
