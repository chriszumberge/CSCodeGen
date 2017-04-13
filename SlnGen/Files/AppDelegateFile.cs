using CSCodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class AppDelegateFile : ProjectFile
    {
        public AppDelegateFile(string assemblyName) : base("AppDelegate.cs", true, false)
        {
            CGFile file = new CGFile("AppDelegate.cs")
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
                            new CGClass("AppDelegate", "global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate", false, false, true)
                            {
                                ClassComments =
                                {
                                    "The UIApplicationDelegate for the application. This class is responsible for launching the",
                                    "User Interface of the application, as well as listening (and optionally responding) to",
                                    "application events from iOS."
                                },
                                ClassAttributes =
                                {
                                    "[Register(\"AppDelegate\")]"
                                },
                                ClassMethods =
                                {
                                    new CGMethod(new CGMethodSignature("FinishedLaunching", "bool", false, true)
                                    {
                                        Arguments =
                                        {
                                            new CGMethodArgument("UIApplication", "app"),
                                            new CGMethodArgument("NSDictionary", "options")
                                        }
                                    })
                                    {
                                        MethodComments =
                                        {
                                            "",
                                            "This method is invoked when the application has loaded and is ready to run. In this",
                                            "method you should instantiate the window, load the UI into it and then make the window",
                                            "visible.",
                                            "",
                                            "You have 17 seconds to return from this method, or iOS will terminate your application.",
                                            ""
                                        },
                                        MethodText = String.Concat(
                                            "global::Xamarin.Forms.Forms.Init();", Environment.NewLine,
                                            "LoadApplication(new App());", Environment.NewLine,
                                            Environment.NewLine,
                                            "return base.FinishedLaunching(app, options);"
                                        )
                                    }
                                }
                            }
                        }
                    }
                }
            };

            this.FileContents = file.ToString();
        }
    }
}
