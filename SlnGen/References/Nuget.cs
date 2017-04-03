using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.References
{
    public static class Nuget
    {
        public static NugetPackage NewtonsoftJson = new NugetPackage("Newtonsoft.Json",
            "Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
            @"..\..\packages\Newtonsoft.Json.10.0.1\lib\net45\Newtonsoft.Json.dll", true);

        public static NugetPackage XamarinFormsCore = new NugetPackage("Xamarin.Forms.Core",
            "Xamarin.Forms.Core, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Core.dll", true);

        public static NugetPackage XamarinFormsPlatform = new NugetPackage("Xamarin.Forms.Platform",
            "Xamarin.Forms.Platform, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Platform.dll", true);

        public static NugetPackage XamarinFormsXaml = new NugetPackage("Xamarin.Forms.Xaml",
            "Xamarin.Forms.Xaml, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Xaml.dll", true);
    }
}
