using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SlnGen
{
    public abstract class CsProj : IFileContainer
    {
        public string AssemblyName => mAssemblyName;
        readonly string mAssemblyName;

        public Guid AssemblyGuid => mAssemblyGuid;
        readonly Guid mAssemblyGuid;

        public List<AssemblyReference> AssemblyReferences => mAssemblyReferences;
        protected List<AssemblyReference> mAssemblyReferences { get; set; }

        public List<NugetPackage> NugetPackages => mNugetPackages;
        protected List<NugetPackage> mNugetPackages { get; set; }

        public List<ProjectReference> ProjectReferences => mProjectReferences;
        protected List<ProjectReference> mProjectReferences { get; set; }

        public string OutputType => mOutputType;
        readonly string mOutputType;

        public string TargetFrameworkVersion => mTargetFrameworkVersion;
        readonly string mTargetFrameworkVersion;

        public List<ProjectFile> Files => mFiles;
        protected List<ProjectFile> mFiles { get; set; }

        public List<ProjectFolder> Folders => mFolders;
        protected List<ProjectFolder> mFolders { get; set; }

        internal CsProj(string assemblyName, string outputType, string targetFrameworkVersion)
        {
            mAssemblyGuid = Guid.NewGuid();

            mAssemblyReferences = new List<AssemblyReference>();
            mProjectReferences = new List<ProjectReference>();
            mNugetPackages = new List<NugetPackage>();
            mFiles = new List<ProjectFile>();
            mFolders = new List<ProjectFolder>();

            mAssemblyName = assemblyName;
            mOutputType = outputType;
            mTargetFrameworkVersion = targetFrameworkVersion;

            mAssemblyReferences.Add(References.Assemblies.System);
            mAssemblyReferences.Add(References.Assemblies.SystemCore);
            mAssemblyReferences.Add(References.Assemblies.SystemXmlLinq);
            mAssemblyReferences.Add(References.Assemblies.MicrosoftCsharp);
            mAssemblyReferences.Add(References.Assemblies.SystemData);
            mAssemblyReferences.Add(References.Assemblies.SystemNetHttp);
            mAssemblyReferences.Add(References.Assemblies.SystemXml);

            mFolders.Add(new ProjectFolder("Properties")
            {
                Files = {
                    new Files.AssemblyInfoFile(mAssemblyName, mAssemblyGuid, new Version(1, 0, 0, 0), new Version(1, 0, 0, 0))
                }
            });
            mFiles.Add(new ProjectFile("packages", "config", false, false));
        }

        public void AddAssemblyReference(AssemblyReference assemblyReference) => mAssemblyReferences.Add(assemblyReference);

        public void AddNugetPackage(NugetPackage nugetPackage) => mNugetPackages.Add(nugetPackage);

        public void AddProjectReference(ProjectReference projectReference) => mProjectReferences.Add(projectReference);

        /// <summary>
        /// Adds the file to folder. Will traverse the folder hierarchy entering exsting and creating nonexisting folders.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="folderHierarchy">The folder hierarchy.</param>
        public void AddFileToFolder(ProjectFile file, params string[] folderNameHierarchy)
        {
            List<string> folderNames = folderNameHierarchy.ToList();
            IFileContainer fileContainer = this;
            foreach (string folderName in folderNames)
            {
                IFileContainer fileContainerFolder = fileContainer.GetFolders().FirstOrDefault(f => f.FolderName.Equals(folderName));
                // if this file container does not have the folder, create it
                if (fileContainerFolder == null)
                {
                    ProjectFolder newFolder = new ProjectFolder(folderName);

                    fileContainer.AddFolder(newFolder);

                    fileContainerFolder = newFolder;
                }

                // Then, enter it whether it existed previously or not
                fileContainer = fileContainerFolder;
            }
            fileContainer.AddFile(file);
        }

        List<ProjectFile> IFileContainer.GetFiles() => mFiles;
        List<ProjectFolder> IFileContainer.GetFolders() => mFolders;

        void IFileContainer.AddFile(ProjectFile file) => mFiles.Add(file);

        void IFileContainer.AddFolder(ProjectFolder folder) => mFolders.Add(folder);

        XNamespace xNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        internal string GenerateProjectFiles(string solutionDirectoryPath)
        {
            string csprojDirectoryPath = Path.Combine(solutionDirectoryPath, AssemblyName);
            DirectoryInfo csprojDirectory = Directory.CreateDirectory(csprojDirectoryPath);

            tempCsProjDirectoryPath = csprojDirectoryPath;
            AddProjectFilesAndFolders(this, csprojDirectoryPath);

            // create csproj file using xmlwriter
            //using (XmlWriter writer = XmlWriter.Create(String.Concat(AssemblyName, ".csproj")))
            //{
            //    writer.WriteStartDocument();
            //}
            //XmlDocument doc = new XmlDocument();
            //XmlElement root = doc.CreateElement("Project");
            //root.SetAttribute("ToolsVersion", "14.0");
            //root.SetAttribute("DefaultTargets", "Build");
            //root.SetAttribute("xmlns", "http://schemas.microsoft.com/developer/msbuild/2003");
            var xmlNode = new XElement(xNamespace+"Project",
                                        new XAttribute("ToolsVersion", "14.0"),
                                        new XAttribute("DefaultTargets", "Build"),
                                        new XAttribute("xmlns", "http://schemas.microsoft.com/developer/msbuild/2003"),
                                        new XElement(xNamespace+"Import",
                                            new XAttribute("Project", @"$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"),
                                            new XAttribute("Condition", @"Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')")
                                        ),
                                        new XElement(xNamespace+"PropertyGroup",
                                            new XElement(xNamespace+"Configuration",
                                                new XAttribute("Condition", " '$(Configuration)' == '' "),
                                                new XText("Debug")
                                            ),
                                            new XElement(xNamespace+"Platform",
                                                new XAttribute("Condition", " '$(Platform)' == '' "),
                                                new XText("AnyCPU")
                                            ),
                                            new XElement(xNamespace+"ProjectGuid",
                                                new XText(String.Concat("{", AssemblyGuid.ToString(), "}"))
                                            ),
                                            new XElement(xNamespace+"OutputType",
                                                new XText(OutputType)
                                            ),
                                            new XElement(xNamespace+"AppDesignerFolder",
                                                new XText("Properties")
                                            ),
                                            new XElement(xNamespace+"RootNamespace",
                                                new XText(AssemblyName)
                                                ),
                                            new XElement(xNamespace+"AssemblyName",
                                                new XText(AssemblyName)
                                                ),
                                            new XElement(xNamespace+"TargetFrameworkVersion",
                                                new XText(TargetFrameworkVersion)
                                                ),
                                            new XElement(xNamespace+"FileAlignment",
                                                new XText("512")
                                                ),
                                            GetProjectSpecificPropertyNodes(xNamespace)
                                        ), // END PROPERTY GROUP
                                        GetDebugAnyCPUPropertyGroup(),
                                        GetReleaseAnyCPUPropertyGroup(),
                                        GetAssemblyReferenceItemGroup(),
                                        GetProjectReferenceItemGroup(),
                                        GetCompileFilesItemGroup(),
                                        GetContentFilesItemGroup(),
                                        GetNoneFilesItemGroup(),
                                        new XElement(xNamespace+"Import",
                                            new XAttribute("Project", @"$(MSBuildToolsPath)\Microsoft.CSharp.targets")
                                        )
                                    ); // END PROJECT
            string csprojFilePath = Path.Combine(csprojDirectoryPath, String.Concat(AssemblyName, ".csproj"));
            xmlNode.Save(csprojFilePath);

            return csprojFilePath;
        }

        private XElement GetAssemblyReferenceItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace+"ItemGroup");
            foreach (NugetPackage package in NugetPackages)
            {
                XElement packageElement =
                    new XElement(xNamespace+"Reference",
                        new XAttribute("Include", package.Include),
                        new XElement(xNamespace+"HintPath",
                            new XText(package.HintPath)
                        ),
                        new XElement(xNamespace+"Private",
                            new XText(package.IsPrivate)
                        )
                    );
                itemGroup.Add(packageElement);
            }
            foreach (AssemblyReference assembly in AssemblyReferences)
            {
                XElement assemblyElement =
                    new XElement(xNamespace+"Reference",
                        new XAttribute("Include", assembly.Name)
                    );
                itemGroup.Add(assemblyElement);
            }
            return itemGroup;
        }

        private XElement GetProjectReferenceItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace+"ItemGroup");
            foreach (ProjectReference project in ProjectReferences)
            {
                XElement projectElement =
                    new XElement(xNamespace+"ProjectReference",
                        new XAttribute("Include", project.Include),
                        new XElement(xNamespace+"Project",
                            new XText(String.Concat("{", project.ProjectGuid.ToString(), "}"))
                        ),
                        new XElement(xNamespace+"Name",
                            new XText(project.Name)
                        )
                    );
                itemGroup.Add(projectElement);
            }
            return itemGroup;
        }

        private XElement GetCompileFilesItemGroup()
        {
            List<KeyValuePair<ProjectFile, string>> compilableFiles = tempFileRelativePathDictionary.Where(x => x.Key.ShouldCompile).ToList();
            XElement itemGroup = new XElement(xNamespace+"ItemGroup");
            foreach(KeyValuePair<ProjectFile, string> compilableFile in compilableFiles)
            {
                XElement compilableElement =
                    new XElement(xNamespace+"Compile",
                        new XAttribute("Include", compilableFile.Value)
                    );
                itemGroup.Add(compilableElement);
            }
            return itemGroup;
        }

        private XElement GetContentFilesItemGroup()
        {
            List<KeyValuePair<ProjectFile, string>> contentFiles = tempFileRelativePathDictionary.Where(x => !x.Key.ShouldCompile && x.Key.IsContent).ToList();
            XElement itemGroup = new XElement(xNamespace+"ItemGroup");
            foreach (KeyValuePair<ProjectFile, string> contentFile in contentFiles)
            {
                XElement contentElement =
                    new XElement(xNamespace+"Content",
                        new XAttribute("Include", contentFile.Value)
                    );
                itemGroup.Add(contentElement);
            }
            return itemGroup;
        }

        private XElement GetNoneFilesItemGroup()
        {
            List<KeyValuePair<ProjectFile, string>> noneTypeFiles = tempFileRelativePathDictionary.Where(x => !x.Key.ShouldCompile && !x.Key.IsContent).ToList();
            XElement itemGroup = new XElement(xNamespace+"ItemGroup");
            foreach (KeyValuePair<ProjectFile, string> noneTypeFile in noneTypeFiles)
            {
                XElement noneTypeElement =
                    new XElement(xNamespace+"None",
                        new XAttribute("Include", noneTypeFile.Value)
                    );
                itemGroup.Add(noneTypeElement);
            }
            return itemGroup;
        }

        private XElement GetReleaseAnyCPUPropertyGroup()
        {
            return
            new XElement(xNamespace+"PropertyGroup",
                new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "),
                new XElement(xNamespace+"DebugSymbols",
                    new XText("true")
                    ),
                new XElement(xNamespace+"DebugType",
                    new XText("full")
                    ),
                new XElement(xNamespace+"Optimize",
                    new XText("false")
                    ),
                new XElement(xNamespace+"OutputPath",
                    new XText(@"bin\Debug\")
                    ),
                new XElement(xNamespace+"DefineConstants",
                    new XText("DEBUG;TRACE")
                    ),
                new XElement(xNamespace+"ErrorReport",
                    new XText("prompt")
                    ),
                new XElement(xNamespace+"WarningLevel",
                    new XText("4")
                    )
                );

        }

        private XElement GetDebugAnyCPUPropertyGroup()
        {
            return
            new XElement(xNamespace+"PropertyGroup",
                new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "),
                new XElement(xNamespace+"DebugType",
                    new XText("pdbonly")
                    ),
                new XElement(xNamespace+"Optimize",
                    new XText("true")
                    ),
                new XElement(xNamespace+"OutputPath",
                    new XText(@"bin\Release\")
                    ),
                new XElement(xNamespace+"DefineConstants",
                    new XText("TRACE")
                    ),
                new XElement(xNamespace+"ErrorReport",
                    new XText("prompt")
                    ),
                new XElement(xNamespace+"WarningLevel",
                    new XText("4")
                    )
                );
        }

        protected abstract XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace);

        Dictionary<ProjectFile, string> tempFileRelativePathDictionary = new Dictionary<ProjectFile, string>();
        string tempCsProjDirectoryPath;
        private void AddProjectFilesAndFolders(IFileContainer container, string currentPath)
        {
            // Create directory if it doesn't exist
            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }
            // Add files to this directory
            foreach (ProjectFile file in container.GetFiles())
            {
                string filePath = Path.Combine(currentPath, file.GetFileSystemName());
                File.WriteAllText(filePath, file.FileContents);
                tempFileRelativePathDictionary.Add(file, filePath.Replace(String.Concat(tempCsProjDirectoryPath, @"\"), String.Empty));
            }
            // Go into each folder recursively down the chain creating files and folders
            foreach (ProjectFolder folder in container.GetFolders())
            {
                AddProjectFilesAndFolders(folder, Path.Combine(currentPath, folder.FolderName));
            }
        }
    }
}
