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
            CsProj clProj = new ClassLibraryCsProj("TestClassLibrary", "v4.5.2");

            clProj.AddNugetPackage(References.Nuget.NewtonsoftJson);
            clProj.AddFileToFolder(new ProjectFile("Class1", "cs", true, false, CreateEmptyClassFile("TestClassLibrary", "Class1").ToString()));
            clProj.AddFileToFolder(new ProjectFile("Test", "txt", false, true, String.Empty));

            CsProj caProj = new ConsoleApplicationCsProj("TestConsoleApplication", "v4.5.2");
            caProj.AddNugetPackage(References.Nuget.NewtonsoftJson);
            caProj.AddProjectReference(new ProjectReference(clProj.AssemblyName, $@"..\{clProj.AssemblyName}\{clProj.AssemblyName}.csproj", clProj.AssemblyGuid));
            caProj.AddFileToFolder(new Files.AppConfigFile());
            caProj.AddFileToFolder(new ProjectFile("help", "txt", false, true));

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

            //CGClass ProgramClass = new CGClass(AccessibilityLevel.Private, "Program");
            //ProgramClass.AddClassMethod(new CGMethod(AccessibilityLevel.Private, "Main", typeof(void), true, new CGMethodArgument[] { new CGMethodArgument(typeof(string[]), "args") }));

            Solution sln = new Solution("TestClassLibrary");
            sln.AddProject(clProj);
            sln.AddProject(caProj);

            string slnPath = sln.GenerateSolutionFiles(@"C:\");

            Console.WriteLine($"Done at {slnPath}");
            Console.ReadLine();
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
