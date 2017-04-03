using CSCodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string slnPath = String.Empty;
            slnPath = TestClassLibrarySolution();
            slnPath = TestXamarinSolution();

            Console.WriteLine($"Done at {slnPath}");
            Console.ReadLine();
        }

        private static string TestXamarinSolution()
        {
            CsProj pcl = new PortableClassLibraryCsProj("TestCrossPlatformPCL");
            pcl.AddFileToFolder(new ProjectFile("App.xaml.cs", true)
            {
                DependentUpon = { "App.xaml" }
            });
            pcl.AddFileToFolder(new ProjectFile("MainPage.xaml.cs", true)
            {
                DependentUpon = { "MainPage.xaml" }
            });
            pcl.AddFileToFolder(new EmbeddedResourceProjectFile("App.xaml", "Designer", "MSBuild:UpdateDesignTimeXaml"));
            pcl.AddFileToFolder(new EmbeddedResourceProjectFile("MainPage.xaml", "Designer", "MSBuild:UpdateDesignTimeXaml"));
            pcl.AddNugetPackage(References.Nuget.XamarinFormsCore);
            pcl.AddNugetPackage(References.Nuget.XamarinFormsPlatform);
            pcl.AddNugetPackage(References.Nuget.XamarinFormsXaml);



            CsProj droidlib = new AndroidCsProj("TestCrossPlatformPCL.Android", "blah", "blah");
            CsProj ioslib = new iOSCsProj("TestCrossPlatformPCL.iOS", "blah", "blah");

            Solution sln = new Solution("TestCrossPlatformPCL");
            sln.AddProject(pcl);
            sln.AddProject(droidlib);
            sln.AddProject(ioslib);

            return sln.GenerateSolutionFiles(@"C:\");
        }

        private static string TestClassLibrarySolution()
        {
            CsProj clProj = new ClassLibraryCsProj("TestClassLibrary", "v4.5.2");

            clProj.AddNugetPackage(References.Nuget.NewtonsoftJson);
            clProj.AddFileToFolder(new ProjectFile("Class1.cs", true, false, CreateEmptyClassFile("TestClassLibrary", "Class1").ToString()));
            clProj.AddFileToFolder(new ProjectFile("Test.txt", false, true, String.Empty));

            CsProj caProj = new ConsoleApplicationCsProj("TestConsoleApplication", "v4.5.2");
            caProj.AddNugetPackage(References.Nuget.NewtonsoftJson);
            caProj.AddProjectReference(new ProjectReference(clProj.AssemblyName, $@"..\{clProj.AssemblyName}\{clProj.AssemblyName}.csproj", clProj.AssemblyGuid));
            caProj.AddFileToFolder(new Files.AppConfigFile());
            caProj.AddFileToFolder(new ProjectFile("help.txt", false, true));

            caProj.AddFileToFolder(new ProjectFile(
                new CGFile("Program.cs")
                {
                    UsingStatements =
                    {
                        new CGUsingStatement("System"),
                        new CGUsingStatement("System.Collections.Generic"),
                        new CGUsingStatement("System.Linq"),
                        new CGUsingStatement("System.Text"),
                        new CGUsingStatement("System.Threading.Tasks")
                    },
                    Namespaces =
                    {
                            new CGNamespace("TestConsoleApplication")
                            {
                                Classes =
                                {
                                    new CGClass(AccessibilityLevel.None, "Program")
                                    {
                                        ClassMethods =
                                        {
                                            new CGMethod(AccessibilityLevel.Private, "Main", "void", true)
                                            {
                                                Arguments =
                                                {
                                                    new CGMethodArgument("string[]", "args")
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                    }
                }));

            Solution sln = new Solution("TestClassLibrary");
            sln.AddProject(clProj);
            sln.AddProject(caProj);

            string slnPath = sln.GenerateSolutionFiles(@"C:\");
            return slnPath;
        }

        public static CGFile CreateEmptyClassFile(string namespaceName, string className)
        {
            return new CGFile(className, "cs",
                "System",
                "System.Collections.Generic",
                "System.Linq",
                "System.Text",
                "System.Threading.Tasks")
            {
                Namespaces =
                {
                    new CGNamespace(namespaceName)
                    {
                        Classes =
                        {
                            new CGClass(AccessibilityLevel.Public, className)
                        }
                    }
                }
            };
        }
    }
}
