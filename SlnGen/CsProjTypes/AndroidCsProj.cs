using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen
{
    public sealed class AndroidCsProj : CsProj
    {
        public readonly string AppName;
        public AndroidCsProj(string appName, string assemblyName, string outputType, string targetFrameworkVersion) : base(assemblyName, "Library", "v6.0")
        {
            AppName = appName;

            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "Any CPU", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhone", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhoneSimulator", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "Any CPU", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhone", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhoneSimulator", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhone", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhoneSimulator", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhone", true, true));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhoneSimulator", true, true));

            mAssemblyReferences.Clear();
            this.AddAssemblyReference(References.Assemblies.System);
            this.AddAssemblyReference(References.Assemblies.SystemCore);
            this.AddAssemblyReference(References.Assemblies.SystemObjectModel);
            this.AddAssemblyReference(References.Assemblies.SystemXmlLinq);
            this.AddAssemblyReference(References.Assemblies.SystemXml);

            this.AddNugetPackage(References.Nuget.XamarinForms_monoandroid60);
            //this.AddNugetPackage(References.Nuget.FormsViewGroup);
            //this.AddNugetPackage(References.Nuget.XamarinAndroidSupportDesign);
            //this.AddNugetPackage(References.Nuget.XamarinAndroidSupportv4);
            //this.AddNugetPackage(References.Nuget.XamarinAndroidSupportv7AppCompat);
            //this.AddNugetPackage(References.Nuget.XamarinAndroidSupportv7CardView);
            //this.AddNugetPackage(References.Nuget.XamarinAndroidSupportv7MediaRouter);
            //this.AddNugetPackage(References.Nuget.XamarinFormsCore);
            //this.AddNugetPackage(References.Nuget.XamarinFormsPlatform);
            //this.AddNugetPackage(References.Nuget.XamarinFormsPlatformAndroid);
            //this.AddNugetPackage(References.Nuget.XamarinFormsXaml);
        }

        protected override void AddFilesAndFoldersToProject()
        {
            base.AddFilesAndFoldersToProject();

            this.AddFileToFolder(new ProjectFile("AboutAssets.txt", false, false), "Assets");
            this.AddFileToFolder(new AndroidManifestFile(AssemblyName), "Properties");
            mFolders.Add(new ProjectFolder("Resources")
            {
                Folders =
                {
                    new ProjectFolder("drawable")
                    {
                        Files =
                        {
                            new AndroidResourceProjectFile("icon.png")
                        }
                    },
                    new ProjectFolder("drawable-hdpi")
                    {
                        Files =
                        {
                            new AndroidResourceProjectFile("icon.png")
                        }
                    },
                    new ProjectFolder("drawable-xhdpi")
                    {
                        Files =
                        {
                            new AndroidResourceProjectFile("icon.png")
                        }
                    },
                    new ProjectFolder("drawable-xxhdpi")
                    {
                        Files =
                        {
                            new AndroidResourceProjectFile("icon.png")
                        }
                    },
                    new ProjectFolder("layout")
                    {
                        Files =
                        {
                            new AndroidResourceProjectFile("Tabbar.axml"),
                            new AndroidResourceProjectFile("Toolbar.axml")
                        }
                    },
                    new ProjectFolder("values")
                    {
                        Files =
                        {
                            new AndroidResourceProjectFile("styles.xml")
                        }
                    }
                },
                Files =
                {
                    new ProjectFile("AboutResources.txt", false, false),
                    //new ProjectFile("Resource.Designer.cs")
                    new ProjectFile(DefaultAndroidResourceDesignerCreator.GetFile(AssemblyName))
                }
            });
            this.AddFileToFolder(new MainActivityFile(AppName, AssemblyName));
        }

        protected override XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace, Guid solutionGuid)
        {
            return new XElement[]
            {
                new XElement(xNamespace+"ProductVersion",
                    new XText("8.0.30703")
                ),
                new XElement(xNamespace+"SchemaVersion",
                    new XText("2.0")
                ),
                new XElement(xNamespace+"ProjectTypeGuids",
                    new XText($"{{EFBA0AD7-5A72-4C68-AF49-83D382785DCF}};{{{solutionGuid.ToString().ToUpper()}}}")
                ),
                new XElement(xNamespace+"AndroidApplication",
                    new XText("true")
                ),
                new XElement(xNamespace+"AndroidResgenFile",
                    new XText(@"Resources\Resource.Designer.cs")
                ),
                new XElement(xNamespace+"GenerateSerializationAssemblies",
                    new XText("Off")
                ),
                new XElement(xNamespace+"AndroidManifest",
                    new XText(@"Properties\AndroidManifest.xml")
                ),
                new XElement(xNamespace+"AndroidUseLatestPlatformSdk",
                    new XText("true")
                ),
                new XElement(xNamespace+"AndroidSupportedAbis",
                    new XText("armeabi,armeabi-v7a,x86")
                ),
                new XElement(xNamespace+"AndroidStoreUncompressedFileExtensions"),
                new XElement(xNamespace+"MandroidI18n"),
                new XElement(xNamespace+"JavaMaximumHeapSize"),
                new XElement(xNamespace+"JavaOptions"),
                new XElement(xNamespace+"NuGetPackageImportStamp")
            };
        }

        protected override XElement ConstructBuildConfigurationPropertyGroup(XNamespace xNamespace, SupportedBuildConfiguration config)
        {
            XElement node = base.ConstructBuildConfigurationPropertyGroup(xNamespace, config);
            if (node != null)
            {
                if (config.Platform.Equals("Any CPU") && config.Configuration.Equals("Debug"))
                {
                    node.Add(
                        new XElement[]
                        {
                            new XElement(xNamespace+"AndroidUseSharedRuntime",
                                new XText("True")
                            ),
                            new XElement(xNamespace+"AndroidLinkMode",
                                new XText("None")
                            )
                        }
                    );
                }
                else if (config.Platform.Equals("Any CPU") && config.Configuration.Equals("Release"))
                {
                    node.Add(
                        new XElement[]
                        {
                            new XElement(xNamespace+"AndroidUseSharedRuntime",
                                new XText("False")
                            ),
                            new XElement(xNamespace+"AndroidLinkMode",
                                new XText("SdkOnly")
                            )
                        }
                    );
                }
            }

            return node;
        }

        protected override XElement[] GetImportProjectItems(XNamespace xNamespace)
        {
            // Don't call base, it uses a project not needed here
            //return base.GetImportProjectItems(xNamespace);
            return new XElement[]
            {
                new XElement(xNamespace+"Import",
                    new XAttribute("Project", @"$(MSBuildExtensionsPath)\Xamarin\Android\Xamarin.Android.CSharp.targets")
                ),
                new XElement(xNamespace+"Import",
                    new XAttribute("Project", @"..\..\packages\Xamarin.Forms.2.2.0.45\build\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+MonoTouch10+Xamarin.iOS10\Xamarin.Forms.targets"),
                    new XAttribute("Condition", @"Exists('..\..\packages\Xamarin.Forms.2.2.0.45\build\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+MonoTouch10+Xamarin.iOS10\Xamarin.Forms.targets')")
                )
            };
        }

        protected override XElement[] GetTargetItems(XNamespace xNamespace)
        {
            return new XElement[]
            {
                new XElement(xNamespace+"Target",
                    new XAttribute("Name", "EnsureNuGetPackageBuildImports"),
                    new XAttribute("BeforeTargets", "PrepareForBuild"),
                    new XElement(xNamespace+"PropertyGroup",
                        new XElement(xNamespace+"ErrorText",
                            new XText("This project references NuGet package(s) that are missing on this computer. Use NuGet Package Restore to download them.  For more information, see http://go.microsoft.com/fwlink/?LinkID=322105. The missing file is {0}.")
                        )
                    ),
                    new XElement(xNamespace+"Error",
                        new XAttribute("Condition", @"!Exists('..\..\packages\Xamarin.Forms.2.2.0.45\build\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+MonoTouch10+Xamarin.iOS10\Xamarin.Forms.targets')"),
                        new XAttribute("Text", @"$([System.String]::Format('$(ErrorText)', '..\..\packages\Xamarin.Forms.2.2.0.45\build\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+MonoTouch10+Xamarin.iOS10\Xamarin.Forms.targets'))")
                    )
                )
            };
        }
    }
}
