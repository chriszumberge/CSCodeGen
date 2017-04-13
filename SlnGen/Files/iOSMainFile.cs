using CSCodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class iOSMainFile : ProjectFile
    {
        public iOSMainFile(string assemblyName) : base("Main.cs", true, false)
        {
            CGFile file = new CGFile("Main.cs")
            {
                UsingStatements =
                {
                    new CGUsingStatement("System"),
                    new CGUsingStatement("System.Collections.Generic"),
                    new CGUsingStatement("System.Linq"),
                    new CGUsingStatement("Foundation"),
                    new CGUsingStatement("UIKit")
                },
                Namespaces =
                {
                    new CGNamespace(assemblyName)
                    {
                        Classes =
                        {
                            new CGClass("Application")
                            {
                                ClassMethods =
                                {
                                    new CGMethod(new CGMethodSignature("Main", "void", true, false)
                                    {
                                        Arguments =
                                        {
                                             new CGMethodArgument("string[]", "args")
                                        }
                                    })
                                    {
                                        MethodComments =
                                        {
                                            "This is the main entry point of the application."
                                        },
                                        MethodText = String.Concat(
                                            "// if you want to use a different Application Delegate class from \"AppDelegate\"", Environment.NewLine,
                                            "// you can specify it here.", Environment.NewLine,
                                            "UIApplication.Main(args, null, \"AppDelegate\");"
                                        )
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
