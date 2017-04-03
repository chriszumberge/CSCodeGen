using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Files
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
        }
    }
}
