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
        public iOSCsProj(string assemblyName, string outputType, string targetFrameworkVersion) : base(assemblyName, outputType, targetFrameworkVersion)
        {
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
        }

        protected override void AddFilesAndFoldersToProject()
        {
            base.AddFilesAndFoldersToProject();

            mFolders.Add(new ProjectFolder("Resources"));

        }
    }
}
