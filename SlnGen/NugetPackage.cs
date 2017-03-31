using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public class NugetPackage : AssemblyReference
    {
        readonly string mInclude;
        public string Include => mInclude;

        public NugetPackage(string name) : base(name)
        {
        }

        public NugetPackage(string name, string include, string hintPath, bool isPrivate) : base(name, hintPath, isPrivate)
        {
            mInclude = include;
        }
    }
}
