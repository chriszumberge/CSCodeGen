using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen
{
    public sealed class iOSCsProj : CsProj
    {
        public iOSCsProj(string assemblyName) : base(assemblyName, "Exe", String.Empty)
        {
            mDefaultBuildPlatform = "iPhoneSimulator";

            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "Any CPU", false));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhone"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhoneSimulator"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "Any CPU", false));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhone"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhoneSimulator"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU", false));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhone"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhoneSimulator"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU", false));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhone"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhoneSimulator"));

            mAssemblyReferences.Clear();
            mAssemblyReferences.Add(References.Assemblies.System);
            mAssemblyReferences.Add(References.Assemblies.SystemXml);
            mAssemblyReferences.Add(References.Assemblies.SystemCore);
            mAssemblyReferences.Add(References.Assemblies.XamariniOS);
            //this.AddNugetPackage(References.Nuget.XamarinFormsCore);
            //this.AddNugetPackage(References.Nuget.XamarinFormsPlatform);
            //this.AddNugetPackage(References.Nuget.XamarinFormsXaml);
            //this.AddNugetPackage(References.Nuget.XamarinFormsPlatformiOS);
            this.AddNugetPackage(References.Nuget.XamarinForms_xamarinios10);
        }

        protected override void AddFilesAndFoldersToProject()
        {
            base.AddFilesAndFoldersToProject();

            mFolders.Add(new ProjectFolder("Resources"));
            this.AddFileToFolder(new AppDelegateFile());
            this.AddFileToFolder(new iOSMainFile());
            this.AddFileToFolder(new EntitlementsPListFile());
            this.AddFileToFolder(new InfoPListFile());
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
                    new XText($"{{FEACFBD2-3405-455C-9665-78FE426C6842}};{{{solutionGuid.ToString().ToUpper()}}}")
                ),
                new XElement(xNamespace+"IPhoneResourcePrefix",
                    new XText("Resources")
                ),
                new XElement(xNamespace+"NuGetPackageImportStamp")
            };
        }

        protected override XElement ConstructBuildConfigurationPropertyGroup(XNamespace xNamespace, SupportedBuildConfiguration config)
        {
            XElement node = base.ConstructBuildConfigurationPropertyGroup(xNamespace, config);
            if (node == null)
            {
                if (config.Platform.Equals("iPhoneSimulator") && config.Configuration.Equals("Debug"))
                {
                    return
                    new XElement(xNamespace + "PropertyGroup",
                        new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Debug|iPhoneSimulator' "),
                        new XElement(xNamespace + "DebugSymbols",
                            new XText("true")
                        ),
                        new XElement(xNamespace + "DebugType",
                            new XText("full")
                        ),
                        new XElement(xNamespace + "Optimize",
                            new XText("false")
                        ),
                        new XElement(xNamespace + "OutputPath",
                            new XText(@"bin\iPhoneSimulator\Debug")
                        ),
                        new XElement(xNamespace + "DefineConstants",
                            new XText("DEBUG")
                        ),
                        new XElement(xNamespace + "ErrorReport",
                            new XText("prompt")
                        ),
                        new XElement(xNamespace + "WarningLevel",
                            new XText("4")
                        ),
                        new XElement(xNamespace + "ConsolePause",
                            new XText("false")
                        ),
                        new XElement(xNamespace + "MtouchArch",
                            new XText("i386, x86_64")
                        ),
                        new XElement(xNamespace + "MtouchLink",
                            new XText("None")
                        ),
                        new XElement(xNamespace + "MtouchDebug",
                            new XText("true")
                        )
                    );
                }
                else if (config.Platform.Equals("iPhoneSimulator") && config.Configuration.Equals("Release"))
                {
                    return
                    new XElement(xNamespace + "PropertyGroup",
                        new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Release|iPhoneSimulator' "),
                        new XElement(xNamespace + "DebugType",
                            new XText("none")
                        ),
                        new XElement(xNamespace + "Optimize",
                            new XText("true")
                        ),
                        new XElement(xNamespace + "OutputPath",
                            new XText(@"bin\iPhoneSimulator\Release")
                        ),
                        new XElement(xNamespace + "ErrorReport",
                            new XText("prompt")
                        ),
                        new XElement(xNamespace + "WarningLevel",
                            new XText("4")
                        ),
                        new XElement(xNamespace + "ConsolePause",
                            new XText("false")
                        ),
                        new XElement(xNamespace + "MtouchArch",
                            new XText("i386, x86_64")
                        ),
                        new XElement(xNamespace + "MtouchLink",
                            new XText("None")
                        )
                    );
                }
                else if (config.Platform.Equals("iPhone") && config.Configuration.Equals("Debug"))
                {
                    return
                    new XElement(xNamespace + "PropertyGroup",
                        new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Debug|iPhone' "),
                        new XElement(xNamespace + "DebugSymbols",
                            new XText("true")
                        ),
                        new XElement(xNamespace + "DebugType",
                            new XText("full")
                        ),
                        new XElement(xNamespace + "Optimize",
                            new XText("false")
                        ),
                        new XElement(xNamespace + "OutputPath",
                            new XText(@"bin\iPhone\Debug")
                        ),
                        new XElement(xNamespace + "DefineConstants",
                            new XText("DEBUG")
                        ),
                        new XElement(xNamespace + "ErrorReport",
                            new XText("prompt")
                        ),
                        new XElement(xNamespace + "WarningLevel",
                            new XText("4")
                        ),
                        new XElement(xNamespace + "ConsolePause",
                            new XText("false")
                        ),
                        new XElement(xNamespace + "MtouchArch",
                            new XText("ARMv7, ARM64")
                        ),
                        new XElement(xNamespace + "CodesignKey",
                            new XText("iPhone Developer")
                        ),
                        new XElement(xNamespace + "MtouchDebug",
                            new XText("true")
                        ),
                        new XElement(xNamespace + "CodesignEntitlements",
                            new XText("Entitlements.plist")
                        )
                    );
                }
                else if (config.Platform.Equals("iPhone") && config.Configuration.Equals("Release"))
                {
                    return
                    new XElement(xNamespace + "PropertyGroup",
                        new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Release|iPhone' "),
                        new XElement(xNamespace + "DebugType",
                            new XText("none")
                        ),
                        new XElement(xNamespace + "Optimize",
                            new XText("true")
                        ),
                        new XElement(xNamespace + "OutputPath",
                            new XText(@"bin\iPhone\Release")
                        ),
                        new XElement(xNamespace + "ErrorReport",
                            new XText("prompt")
                        ),
                        new XElement(xNamespace + "WarningLevel",
                            new XText("4")
                        ),
                        new XElement(xNamespace + "ConsolePause",
                            new XText("false")
                        ),
                        new XElement(xNamespace + "MtouchArch",
                            new XText("ARMv7, ARM64")
                        ),
                        new XElement(xNamespace + "CodesignKey",
                            new XText("iPhone Developer")
                        ),
                        new XElement(xNamespace + "CodesignEntitlements",
                            new XText("Entitlements.plist")
                        )
                    );
                }
                else if (config.Platform.Equals("iPhone") && config.Configuration.Equals("Ad-Hoc"))
                {
                    return
                    new XElement(xNamespace + "PropertyGroup",
                        new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Ad-Hoc|iPhone' "),
                        new XElement(xNamespace + "DebugType",
                            new XText("none")
                        ),
                        new XElement(xNamespace + "Optimize",
                            new XText("true")
                        ),
                        new XElement(xNamespace + "OutputPath",
                            new XText(@"bin\iPhone\Ad-Hoc")
                        ),
                        new XElement(xNamespace + "ErrorReport",
                            new XText("prompt")
                        ),
                        new XElement(xNamespace + "WarningLevel",
                            new XText("4")
                        ),
                        new XElement(xNamespace + "ConsolePause",
                            new XText("false")
                        ),
                        new XElement(xNamespace + "MtouchArch",
                            new XText("ARMv7, ARM64")
                        ),
                        new XElement(xNamespace + "BuildIpa",
                            new XText("true")
                        ),
                        new XElement(xNamespace + "CodesignProvision",
                            new XText("Automatic:AdHoc")
                        ),
                        new XElement(xNamespace + "CodesignKey",
                            new XText("iPhone Distribution")
                        ),
                        new XElement(xNamespace + "CodesignEntitlements",
                            new XText("Entitlements.plist")
                        )
                    );
                }
                else if (config.Platform.Equals("iPhone") && config.Configuration.Equals("AppStore"))
                {
                    return
                    new XElement(xNamespace + "PropertyGroup",
                        new XAttribute("Condition", " '$(Configuration)|$(Platform)' == 'AppStore|iPhone' "),
                        new XElement(xNamespace + "DebugType",
                            new XText("none")
                        ),
                        new XElement(xNamespace + "Optimize",
                            new XText("true")
                        ),
                        new XElement(xNamespace + "OutputPath",
                            new XText(@"bin\iPhone\AppStore")
                        ),
                        new XElement(xNamespace + "ErrorReport",
                            new XText("prompt")
                        ),
                        new XElement(xNamespace + "WarningLevel",
                            new XText("4")
                        ),
                        new XElement(xNamespace + "ConsolePause",
                            new XText("false")
                        ),
                        new XElement(xNamespace + "MtouchArch",
                            new XText("ARMv7, ARM64")
                        ),
                        new XElement(xNamespace + "CodesignProvision",
                            new XText("Automatic:AppStore")
                        ),
                        new XElement(xNamespace + "CodesignKey",
                            new XText("iPhone Distribution")
                        ),
                        new XElement(xNamespace + "CodesignEntitlements",
                            new XText("Entitlements.plist")
                        )
                    );
                }
                // no else, node is already null
            }
            return node;
        }

        protected override XElement[] GetCustomFilesItemGroups(XNamespace xNamespace)
        {
            return new XElement[]
            {
                new XElement(xNamespace+"ItemGroup",
                    new XElement(xNamespace+"ITunesArtwork",
                        new XAttribute("Include", "iTunesArtwork")
                    ),
                    new XElement(xNamespace+"ITunesArtwork",
                        new XAttribute("Include", "iTunesArtwork@2x")
                    )
                ),
                new XElement(xNamespace+"ItemGroup",
                    new XElement(xNamespace+"BundleResource",
                        new XAttribute("Include", @"Resources\Icon-60%202x.png")
                    ),
                    new XElement(xNamespace+"InterfaceDefinition",
                        new XAttribute("Include", @"Resources\LaunchScreen.storyboard")
                    )
                )
            };
        }

        protected override XElement[] GetImportProjectItems(XNamespace xNamespace)
        {
            // Don't call base, it uses a project not needed here
            //return base.GetImportProjectItems(xNamespace);
            return new XElement[]
            {
                new XElement(xNamespace+"Import",
                    new XAttribute("Project", @"$(MSBuildExtensionsPath)\Xamarin\iOS\Xamarin.iOS.CSharp.targets")
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
