using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class NugetPackage
    {
        readonly string mId;
        public string Id => mId;

        readonly string mVersion;
        public string Version => mVersion;

        readonly string mTargetFramework;
        public string TargetFramework => mTargetFramework;

        public List<NugetAssembly> Assemblies { get; set; } = new List<NugetAssembly>();

        public NugetPackage(string id, string version, string targetFramework)
        {
            mId = id;
            mVersion = version;
            mTargetFramework = targetFramework;
        }
    }
}
