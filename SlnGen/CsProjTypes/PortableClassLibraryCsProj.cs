using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen
{
    public sealed class PortableClassLibraryCsProj : CsProj
    {
        readonly string mTargetFrameworkProfile;

        public PortableClassLibraryCsProj(string assemblyName) : base(assemblyName, "Library", "v4.5")
        {
            mAssemblyReferences.Clear();

            //this.AddNugetPackage(References.Nuget.XamarinFormsCore);
            //this.AddNugetPackage(References.Nuget.XamarinFormsPlatform);
            //this.AddNugetPackage(References.Nuget.XamarinFormsXaml);
            this.AddNugetPackage(References.Nuget.XamarinForms_portable45);

            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "Any CPU"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhone"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Ad-Hoc", "iPhoneSimulator"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "Any CPU"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhone"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("AppStore", "iPhoneSimulator"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "Any CPU"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhone"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Debug", "iPhoneSimulator"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "Any CPU"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhone"));
            mSupportedBuildConfigurations.Add(new SupportedBuildConfiguration("Release", "iPhoneSimulator"));

            string TargetFrameworkProfile = "Profile259";
            mTargetFrameworkProfile = TargetFrameworkProfile;
        }

        protected override XElement[] GetProjectSpecificPropertyNodes(XNamespace xNamespace, Guid solutionGuid)
        {
            return new XElement[]
            {
                new XElement(xNamespace+"MinimumVisualStudioVersion",
                    new XText("11.0")
                ),
                new XElement(xNamespace+"TargetFrameworkProfile",
                    new XText(mTargetFrameworkProfile)
                ),
                new XElement(xNamespace+"ProjectTypeGuids",
                    new XText($"{{786C830F-07A1-408B-BD7F-6EE04809D6DB}};{{{solutionGuid.ToString().ToUpper()}}}")
                ),
                new XElement(xNamespace+"NuGetPackageImportStamp")
            };
        }

        protected override XElement[] GetImportProjectItems(XNamespace xNamespace)
        {
            // Don't call base, it uses a project not needed here
            //return base.GetImportProjectItems(xNamespace);
            return new XElement[]
            {
                new XElement(xNamespace+"Import",
                    new XAttribute("Project", @"$(MSBuildExtensionsPath32)\Microsoft\Portable\$(TargetFrameworkVersion)\Microsoft.Portable.CSharp.targets")
                ),
                new XElement(xNamespace+"Import",
                    new XAttribute("Project", @"..\..\packages\Xamarin.Forms.2.3.3.193\build\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.targets"),
                    new XAttribute("Condition", @"Exists('..\..\packages\Xamarin.Forms.2.3.3.193\build\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.targets')")
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
                        new XAttribute("Condition", @"!Exists('..\..\packages\Xamarin.Forms.2.3.3.193\build\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.targets')"),
                        new XAttribute("Text", @"$([System.String]::Format('$(ErrorText)', '..\..\packages\Xamarin.Forms.2.3.3.193\build\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.targets'))")
                    )
                )
            };
        }
    }
}
