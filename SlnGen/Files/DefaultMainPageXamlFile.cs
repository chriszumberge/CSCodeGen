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
    public sealed class DefaultMainPageXamlFile : XamlProjectFile
    {
        public DefaultMainPageXamlFile(string assemblyName) : base("MainPage")
        {
            XNamespace @namespace = "http://xamarin.com/schemas/2014/forms";
            XNamespace xNamespace = "http://schemas.microsoft.com/winfx/2009/xaml";
            XNamespace localNamespace = $"clr-namespace:{assemblyName}";

            XElement rootNode = new XElement(@namespace + "ContentPage",
                    //new XAttribute(@namespace+"xmlns", "http://xamarin.com/schemas/2014/forms"),
                    //new XAttribute(xNamespace+"xmlns", "http://schemas.microsoft.com/winfx/2009/xaml"),
                    //new XAttribute(localNamespace+"xmlns", $"clr-namespace:{assemblyName}"),
                    new XAttribute(xNamespace+"Class", $"{assemblyName}.MainPage"),
                    new XElement(@namespace + "Label",
                        new XAttribute("Text", "Welcome to Xamarin Forms!"),
                        new XAttribute("VerticalOptions", "Center"),
                        new XAttribute("HorizontalOptions", "Center")
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

            CGFile file = new CGFile("MainPage.xaml.cs")
            {
                UsingStatements =
                {
                    new CGUsingStatement("System"),
                    new CGUsingStatement("System.Collections.Generic"),
                    new CGUsingStatement("System.Linq"),
                    new CGUsingStatement("System.Text"),
                    new CGUsingStatement("System.Threading.Tasks"),
                    new CGUsingStatement("Xamarin.Forms")
                },
                Namespaces =
                {
                    new CGNamespace(assemblyName)
                    {
                        Classes =
                        {
                            new CGClass("MainPage", "ContentPage", false, false, true)
                            {
                                ClassConstructors =
                                {
                                    new CGClassConstructor("MainPage")
                                    {
                                        ConstructorText =
                                        {
                                            "InitializeComponent();"
                                        }
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
