using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public class NugetAssembly : AssemblyReference
    {
        readonly string mInclude;
        public string Include => mInclude;

        public NugetAssembly(string name, string include, string hintPath, bool isPrivate) : base(name, hintPath, isPrivate)
        {
            mInclude = include;
        }
    }
}
