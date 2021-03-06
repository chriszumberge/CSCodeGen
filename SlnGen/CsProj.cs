﻿using System;
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

        public List<SupportedBuildConfiguration> SupportedBuildConfigurations => mSupportedBuildConfigurations;
        protected List<SupportedBuildConfiguration> mSupportedBuildConfigurations { get; set; }

        protected string mDefaultBuildConfiguration = "Debug";
        protected string mDefaultBuildPlatform = "AnyCPU";

        internal CsProj(string assemblyName, string outputType, string targetFrameworkVersion)
        {
            mAssemblyGuid = Guid.NewGuid();

            mAssemblyReferences = new List<AssemblyReference>();
            mProjectReferences = new List<ProjectReference>();
            mNugetPackages = new List<NugetPackage>();
            mFiles = new List<ProjectFile>();
            mFolders = new List<ProjectFolder>();
            mSupportedBuildConfigurations = new List<SupportedBuildConfiguration>();

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

            AddFilesAndFoldersToProject();
        }

        protected virtual void AddFilesAndFoldersToProject()
        {
            mFolders.Add(new ProjectFolder("Properties")
            {
                Files = {
                    new AssemblyInfoFile(mAssemblyName, mAssemblyGuid, new Version(1, 0, 0, 0), new Version(1, 0, 0, 0))
                }
            });
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

        public void AddFileToFolder(XamlProjectFile file, params string[] folderNameHierarchy)
        {
            this.AddFileToFolder(file.XamlCsFile, folderNameHierarchy);
            this.AddFileToFolder(file.XamlFile, folderNameHierarchy);
        }

        List<ProjectFile> IFileContainer.GetFiles() => mFiles;
        List<ProjectFolder> IFileContainer.GetFolders() => mFolders;

        void IFileContainer.AddFile(ProjectFile file) => mFiles.Add(file);

        void IFileContainer.AddFolder(ProjectFolder folder) => mFolders.Add(folder);

        XNamespace xNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        internal string GenerateProjectFiles(string solutionDirectoryPath, Guid solutionGuid)
        {
            string csprojDirectoryPath = Path.Combine(solutionDirectoryPath, AssemblyName);
            DirectoryInfo csprojDirectory = Directory.CreateDirectory(csprojDirectoryPath);

            tempCsProjDirectoryPath = csprojDirectoryPath;

            // Do packages.config with all the added nuget packages
            var packageRoot = new XElement("packages");
            ProjectFile packagesConfig = new ProjectFile("packages.config", false, false);
            foreach (NugetPackage package in mNugetPackages)
            {
                XElement packageElement =
                        new XElement("package",
                            new XAttribute("id", package.Id),
                            new XAttribute("version", package.Version),
                            new XAttribute("targetFramework", package.TargetFramework)
                        );
                packageRoot.Add(packageElement);
            }
            using (var memoryStream = new MemoryStream())
            {
                packageRoot.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    string contents = streamReader.ReadToEnd();
                    packagesConfig.FileContents = contents;
                }
            }
            mFiles.Add(packagesConfig);

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
                                                new XText(mDefaultBuildConfiguration)
                                            ),
                                            new XElement(xNamespace+"Platform",
                                                new XAttribute("Condition", " '$(Platform)' == '' "),
                                                new XText(mDefaultBuildPlatform)
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
                                            GetProjectSpecificPropertyNodes(xNamespace, solutionGuid)
                                        ), // END PROPERTY GROUP
                                        GetBuildConfigurationPropertyGroups(xNamespace),
                                        GetAssemblyReferenceItemGroup(),
                                        GetProjectReferenceItemGroup(),
                                        GetCompileFilesItemGroup(),
                                        GetOtherFileItemGroup(),
                                        GetContentFilesItemGroup(),
                                        GetNoneFilesItemGroup(),
                                        GetCustomFilesItemGroups(xNamespace),
                                        GetImportProjectItems(xNamespace),
                                        GetTargetItems(xNamespace),
                                        GetPreBuildEventPropertyGroups(xNamespace),
                                        GetPostBuildEventPropertyGroups(xNamespace)
                                    ); // END PROJECT
            string csprojFilePath = Path.Combine(csprojDirectoryPath, String.Concat(AssemblyName, ".csproj"));
            xmlNode.Save(csprojFilePath);

            return csprojFilePath;
        }

        private XElement[] GetBuildConfigurationPropertyGroups(XNamespace xNamespace)
        {
            List<XElement> nodes = new List<XElement>();
            foreach(SupportedBuildConfiguration config in mSupportedBuildConfigurations)
            {
                if (config.Build)
                {
                    XElement buildConfigNode = this.ConstructBuildConfigurationPropertyGroup(xNamespace, config);
                    if (buildConfigNode != null)
                    {
                        nodes.Add(buildConfigNode);
                    }
                }
            }

            return nodes.ToArray();
        }

        protected virtual XElement ConstructBuildConfigurationPropertyGroup(XNamespace xNamespace, SupportedBuildConfiguration config)
        {
            if (config.Platform.Equals("Any CPU") && config.Configuration.Equals("Debug"))
            {
                return GetDebugAnyCPUPropertyGroup();
            }
            else if (config.Platform.Equals("Any CPU") && config.Configuration.Equals("Release"))
            {
                return GetReleaseAnyCPUPropertyGroup();
            }
            else
            {
                return null;
            }
        }

        private XElement GetAssemblyReferenceItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace+"ItemGroup");
            foreach (NugetPackage package in NugetPackages)
            {
                foreach (NugetAssembly assembly in package.Assemblies)
                {
                    XElement packageElement =
                        new XElement(xNamespace + "Reference",
                            new XAttribute("Include", assembly.Include),
                            new XElement(xNamespace + "HintPath",
                                new XText(assembly.HintPath)
                            ),
                            new XElement(xNamespace + "Private",
                                new XText(assembly.IsPrivate)
                            )
                        );
                    itemGroup.Add(packageElement);
                }
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

                foreach (string dependent in compilableFile.Key.DependentUpon)
                {
                    compilableElement.Add(
                        new XElement(xNamespace+"DependentUpon",
                            new XText(dependent)
                        )
                    );
                }

                itemGroup.Add(compilableElement);
            }
            return itemGroup;
        }

        private XElement GetOtherFileItemGroup()
        {
            XElement itemGroup = new XElement(xNamespace + "ItemGroup");

            List<KeyValuePair<ProjectFile, string>> embeddedFiles = tempFileRelativePathDictionary.Where(x => x.Key is EmbeddedResourceProjectFile).ToList();
            foreach (KeyValuePair<ProjectFile, string> embeddedFile in embeddedFiles)
            {
                EmbeddedResourceProjectFile typeCastedFile = embeddedFile.Key as EmbeddedResourceProjectFile;

                XElement embeddedElement =
                    new XElement(xNamespace+"EmbeddedResource",
                        new XAttribute("Include", embeddedFile.Value),
                        new XElement(xNamespace+"SubType",
                            new XText(typeCastedFile.SubType)
                        ),
                        new XElement(xNamespace+"Generator",
                            new XText(typeCastedFile.Generator)
                        )
                    );
                itemGroup.Add(embeddedElement);
            }

            List<KeyValuePair<ProjectFile, string>> androidResourceFiles = tempFileRelativePathDictionary.Where(x => x.Key is AndroidResourceProjectFile).ToList();
            foreach (KeyValuePair<ProjectFile, string> androidResourceFile in androidResourceFiles)
            {
                AndroidResourceProjectFile typeCastedFile = androidResourceFile.Key as AndroidResourceProjectFile;

                XElement resourceElement =
                    new XElement(xNamespace + "AndroidResource",
                        new XAttribute("Include", androidResourceFile.Value)
                    );
                itemGroup.Add(resourceElement);
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
            List<KeyValuePair<ProjectFile, string>> noneTypeFiles = tempFileRelativePathDictionary.Where(x => !x.Key.ShouldCompile && !x.Key.IsContent 
                && !(x.Key is EmbeddedResourceProjectFile || x.Key is AndroidResourceProjectFile)).ToList();
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

        private XElement GetDebugAnyCPUPropertyGroup()
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

        private XElement GetReleaseAnyCPUPropertyGroup()
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

        protected virtual XElement[] GetPreBuildEventPropertyGroups(XNamespace xNamespace)
        {
            //<PropertyGroup>
            //    <PreBuildEvent>$(SolutionDir)\BuildUtilities\IncrementBuildiOS.exe "$(ProjectDir)\Info.plist"</PreBuildEvent>
            //</PropertyGroup>
            return new XElement[] { };
        }

        protected virtual XElement[] GetPostBuildEventPropertyGroups(XNamespace xNamespace)
        {
            //<PropertyGroup>
            //    <PostBuildEvent>$(SolutionDir)\BuildUtilities\IncrementBuildiOS.exe "$(ProjectDir)\Info.plist"</PostBuildEvent>
            //</PropertyGroup>
            return new XElement[] { };
        }

        protected virtual XElement[] GetCustomFilesItemGroups(XNamespace xNamespace)
        {
            return new XElement[] { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Probably should not call base when overriding this method. If you're overriding it there's a chance you do not want the default features.
        /// </remarks>
        /// <param name="xNamespace"></param>
        /// <returns></returns>
        protected virtual XElement[] GetImportProjectItems(XNamespace xNamespace)
        {
            return new XElement[] {
                new XElement(xNamespace + "Import",
                    new XAttribute("Project", @"$(MSBuildToolsPath)\Microsoft.CSharp.targets")
                )
            };
        }

        protected virtual XElement[] GetTargetItems(XNamespace xNamespace)
        {
            return new XElement[] { };
        }

        protected virtual XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace, Guid solutionGuid)
        {
            return new XElement[] { };
        }

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
                string filePath = Path.Combine(currentPath, file.FileName);
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


    public class SupportedBuildConfiguration
    {
        string mConfiguration { get; set; }
        public string Configuration => mConfiguration;
        string mPlatform { get; set; }
        public string Platform => mPlatform;
        bool mBuild { get; set; }
        public bool Build => mBuild;
        bool mDeploy { get; set; }
        public bool Deploy => mDeploy;

        public SupportedBuildConfiguration(string configuration, string platform, bool build = true, bool deploy = false)
        {
            mConfiguration = configuration;
            mPlatform = platform;
            mBuild = build;
            mDeploy = deploy;
        }
    }
}
