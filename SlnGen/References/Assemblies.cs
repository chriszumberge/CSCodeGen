using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.References
{
    public static class Assemblies
    {
        public static AssemblyReference System = new AssemblyReference("System");
        public static AssemblyReference SystemCore = new AssemblyReference("System.Core");
        public static AssemblyReference SystemXmlLinq = new AssemblyReference("System.Xml.Linq");
        public static AssemblyReference SystemDataDataSetExtensions = new AssemblyReference("System.Data.DataSetExtensions");
        public static AssemblyReference MicrosoftCsharp = new AssemblyReference("Microsoft.CSharp");
        public static AssemblyReference SystemData = new AssemblyReference("System.Data");
        public static AssemblyReference SystemNetHttp = new AssemblyReference("System.Net.Http");
        public static AssemblyReference SystemXml = new AssemblyReference("System.Xml");
        public static AssemblyReference SystemObjectModel = new AssemblyReference("System.ObjectModel");

        public static AssemblyReference XamariniOS = new AssemblyReference("Xamarin.iOS");
    }
}
