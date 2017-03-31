using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen
{
    public sealed class ConsoleApplicationCsProj : CsProj
    {

        public ConsoleApplicationCsProj(string assemblyName, string targetFrameworkVersion) : base(assemblyName, "Exe", targetFrameworkVersion)
        {
        }

        protected override XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace)
        {
            return new XElement[] { new XElement(xNamespace+"AutoGenerateBindingRedirects", new XText("true")) };
        }
    }
}
