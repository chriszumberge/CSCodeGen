using CSCodeGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen
{
    public sealed class DefaultAppXamlFile : XamlProjectFile
    {
        public DefaultAppXamlFile(string assemblyName) : base("App")
        {
            XNamespace @namespace = "http://xamarin.com/schemas/2014/forms";
            XNamespace xNamespace = "http://schemas.microsoft.com/winfx/2009/xaml";
            
            XElement rootNode = new XElement(@namespace + "Application",
                    //new XAttribute("xmlns", "http://xamarin.com/schemas/2014/forms"),
                    //new XAttribute("xmlns", "http://schemas.microsoft.com/winfx/2009/xaml"),
                    new XAttribute(xNamespace+"Class", $"{assemblyName}.App"),
                    new XElement(@namespace + "Application.Resources"
                    )
                );

            using (var memoryStream = new MemoryStream())
            {
                rootNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    string contents = streamReader.ReadToEnd();
                    XamlFileContents = contents;
                }
            }

            CGFile file = new CGFile("App.xaml.cs")
            {
                UsingStatements =
                {
                    new CGUsingStatement("System"),
                    new CGUsingStatement("System.Collections.Generic"),
                    new CGUsingStatement("System.Linq"),
                    new CGUsingStatement("System.Text"),
                    new CGUsingStatement("Xamarin.Forms")
                },
                Namespaces =
                {
                    new CGNamespace(assemblyName)
                    {
                        Classes =
                        {
                            new CGClass("App", "Application", false, false, true)
                            {
                                ClassConstructors =
                                {
                                    new CGClassConstructor("App")
                                    {
                                        ConstructorText =
                                        {
                                            "InitializeComponent();",
                                            Environment.NewLine,
                                            $"MainPage = new {assemblyName}.MainPage();"
                                        }
                                    }
                                },
                                ClassMethods =
                                {
                                    new CGMethod(new CGMethodSignature(AccessibilityLevel.Protected, "OnStart", "void", false, true))
                                    {
                                        MethodText = "// Handle when your app starts"
                                    },
                                    new CGMethod(new CGMethodSignature(AccessibilityLevel.Protected, "OnSleep", "void", false, true))
                                    {
                                        MethodText = "// Handle when your app sleeps"
                                    },
                                    new CGMethod(new CGMethodSignature(AccessibilityLevel.Protected, "OnResume", "void", false, true))
                                    {
                                        MethodText = "// Handle when your app resumes"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            XamlCsFileContents = file.ToString();
        }
    }
}
