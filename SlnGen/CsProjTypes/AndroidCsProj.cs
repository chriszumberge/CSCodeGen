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
        public AndroidCsProj(string assemblyName, string outputType, string targetFrameworkVersion) : base(assemblyName, outputType, targetFrameworkVersion)
        {
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
        }
    }
}
