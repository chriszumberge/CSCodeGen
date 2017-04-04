using CSCodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class AssemblyInfoFile : ProjectFile
    {
        readonly string mAssembltyTitle;
        readonly Guid mAssemblyGuid;
        readonly Version mAssemblyVersion;
        readonly Version mAssemblyFileVersion;

        public AssemblyInfoFile(string assemblyTitle, Guid assemblyGuid, Version assemblyVersion, Version assemblyFileVersion) : base("AssemblyInfo.cs", true, false)
        {
            mAssembltyTitle = assemblyTitle;
            mAssemblyGuid = assemblyGuid;
            mAssemblyVersion = assemblyVersion;
            mAssemblyFileVersion = assemblyFileVersion;

            FileContents = AssemblyInfoTemplate.CompileTemplateWithReplacers(
                new List<TemplateFieldReplacer>
                {
                    new TemplateFieldReplacer("AssemblyTitle", mAssembltyTitle),
                    new TemplateFieldReplacer("AssemblyGuid", mAssemblyGuid.ToString()),
                    new TemplateFieldReplacer("AssemblyVersion", mAssemblyVersion.ToString()),
                    new TemplateFieldReplacer("AssemblyFileVersion", mAssemblyFileVersion.ToString())
                });
        }

        private FileTemplate AssemblyInfoTemplate = new FileTemplate(
            new ProjectFile(
                new CGFile("AssemblyInfo.cs")
                {
                    UsingStatements =
                    {
                        new CGUsingStatement("System.Reflection"),
                        new CGUsingStatement("System.Runtime.CompilerServices"),
                        new CGUsingStatement("System.Runtime.InteropServices")
                    },
                    AdditionalLines =
                    {
                        "// General Information about an assembly is controlled through the following ",
                        "// set of attributes. Change these attribute values to modify the information",
                        "// associated with an assembly.",
                        "[assembly: AssemblyTitle(\"{{Field::AssemblyTitle}}\")]",
                        "[assembly: AssemblyDescription(\"\")]",
                        "[assembly: AssemblyConfiguration(\"\")]",
                        "[assembly: AssemblyCompany(\"Microsoft\")]",
                        "[assembly: AssemblyProduct(\"{{Field::AssemblyTitle}}\")]",
                        "[assembly: AssemblyCopyright(\"Copyright © Microsoft 2014\")]",
                        "[assembly: AssemblyTrademark(\"\")]",
                        "[assembly: AssemblyCulture(\"\")]",
                        "",
                        "// Setting ComVisible to false makes the types in this assembly not visible",
                        "// to COM components.  If you need to access a type in this assembly from ",
                        "// COM, set the ComVisible attribute to true on that type.",
                        "[assembly: ComVisible(false)]",
                        "",
                        "// The following GUID is for the ID of the typelib if this project is exposed to COM",
                        "[assembly: Guid(\"{{Field::AssemblyGuid\")]",
                        "",
                        "// Version information for an assembly consists of the following four values:",
                        "//",
                        "//      Major Version",
                        "//      Minor Version",
                        "//      Build Number",
                        "//      Revision",
                        "//",
                        "// You can specify all the values or you can default the Revision and Build Numbers ",
                        "// by using the '*' as shown below:",
                        "[assembly: AssemblyVersion(\"{{Field::AssemblyVersion}}\")]",
                        "[assembly: AssemblyFileVersion(\"{{Field::AssemblyFileVersion}}\")]"
                    }
                }
            )
        );
    }
}
