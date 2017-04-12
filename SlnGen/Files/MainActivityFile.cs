using CSCodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class MainActivityFile : ProjectFile
    {
        public MainActivityFile(string appName, string assemblyName) : base("MainActivity.cs")
        {
            CGFile file = new CGFile("MainActivity.cs")
            {
                UsingStatements =
                {
                    new CGUsingStatement("System"),
                    new CGUsingStatement("Android.App"),
                    new CGUsingStatement("Android.Content.PM"),
                    new CGUsingStatement("Android.Runtime"),
                    new CGUsingStatement("Android.Views"),
                    new CGUsingStatement("Android.Widget"),
                    new CGUsingStatement("Android.OS")
                },
                Namespaces =
                {
                    new CGNamespace(assemblyName)
                    {
                        Classes =
                        {
                            new CGClass("MainActivity", "global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity")
                            {
                                ClassAttributes =
                                {
                                    $"[Activity(Label = \"{appName}\", Icon = \"@drawable/icon\", Theme = \"@style/MainTheme\", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]"
                                },
                                ClassMethods =
                                {
                                    new CGMethod(new CGMethodSignature(AccessibilityLevel.Protected, "OnCreate", "void", false, true)
                                    {
                                        Arguments =
                                        {
                                            new CGMethodArgument("Bundle", "bundle")
                                        }
                                    })
                                    {
                                        MethodText = String.Concat(
                                            "TabLayoutResource = Resource.Layout.Tabbar;", Environment.NewLine,
                                            "ToolbarResource = Resource.Layout.Toolbar;", Environment.NewLine,
                                            Environment.NewLine,
                                            "base.OnCreate(bundle);", Environment.NewLine,
                                            Environment.NewLine,
                                            "global::Xamarin.Forms.Forms.Init(this, bundle);", Environment.NewLine,
                                            "LoadApplication(new App());"
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
