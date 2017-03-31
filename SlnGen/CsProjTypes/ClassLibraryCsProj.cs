using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen
{
    public sealed class ClassLibraryCsProj : CsProj
    {
        public ClassLibraryCsProj(string assemblyName, string targetFrameworkVersion) : base(assemblyName, "Library", targetFrameworkVersion)
        {

        }

        protected override XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace)
        {
            return new XElement[] { };
        }
    }
}
