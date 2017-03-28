using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCodeGen.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            CGMethodSignature ifc_methodA = new CGMethodSignature(AccessibilityLevel.Public, "MethodA", null, true);
            CGMethodSignature ifc_methodB = new CGMethodSignature(AccessibilityLevel.Public, "MethodB", typeof(int), false,
                new List<CGMethodArgument>
                {
                    new CGMethodArgument(typeof(string), "strParam"),
                    new CGMethodArgument(typeof(HttpStyleUriParser), "uriParserParam"),
                    new CGMethodArgument(typeof(Object), "options", null)
                });
            CGInterface ifc = new CGInterface(AccessibilityLevel.Public, "ITestInterface", new List<string>
            {
               "IBaseInterface", "IEnumerable<T>"
            }, new List<CGMethodSignature>
            {
                ifc_methodA,
                ifc_methodB
            });

            CGNamespace testNamespace = new CGNamespace("Test.Interfaces");
            testNamespace.AddInterface(ifc);

            CGFile file = new CGFile("TestFile");
            file.AddUsingStatment(new CGUsingStatement("System"));
            file.AddUsingStatment(new CGUsingStatement("System.Text"));
            file.AddNamespace(testNamespace);

            string fileOutput = file.ToString();

            Console.ReadLine();
        }
    }
}
