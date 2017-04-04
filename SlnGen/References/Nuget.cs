using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.References
{
    public static class Nuget
    {
        public static readonly NugetPackage NewtonsoftJson = new NugetPackage("Newtonsoft.Json",
            "Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
            @"..\..\packages\Newtonsoft.Json.10.0.1\lib\net45\Newtonsoft.Json.dll", true);

        public static readonly NugetPackage XamarinFormsCore = new NugetPackage("Xamarin.Forms.Core",
            "Xamarin.Forms.Core, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Core.dll", true);

        public static readonly NugetPackage XamarinFormsPlatform = new NugetPackage("Xamarin.Forms.Platform",
            "Xamarin.Forms.Platform, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Platform.dll", true);

        public static readonly NugetPackage XamarinFormsXaml = new NugetPackage("Xamarin.Forms.Xaml",
            "Xamarin.Forms.Xaml, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Xaml.dll", true);

        public static readonly NugetPackage XamarinFormsPlatformiOS = new NugetPackage("Xamarin.Forms.Platform.iOS",
            "Xamarin.Forms.Platform.iOS, Version = 2.0.0.0, Culture = neutral, processorArchitecture = MSIL",
            @"..\..\packages\Xamarin.Forms.2.2.0.45\lib\Xamarin.iOS10\Xamarin.Forms.Platform.iOS.dll", true);

        public static readonly NugetPackage FormsViewGroup = new NugetPackage("FormsViewGroup",
            "FormsViewGroup, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Forms.2.2.0.45\lib\MonoAndroid10\FormsViewGroup.dll", true);

        public static readonly NugetPackage XamarinAndroidSupportDesign = new NugetPackage("Xamarin.Android.Support.Design",
            "Xamarin.Android.Support.Design, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Android.Support.Design.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.Design.dll", true);

        public static readonly NugetPackage XamarinAndroidSupportv4 = new NugetPackage("Xamarin.Android.Support.v4",
            "Xamarin.Android.Support.v4, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Android.Support.v4.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.v4.dll", true);

        public static readonly NugetPackage XamarinAndroidSupportv7AppCompat = new NugetPackage("Xamarin.Android.Support.v7.AppCompat",
            "Xamarin.Android.Support.v7.AppCompat, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Android.Support.v7.AppCompat.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.v7.AppCompat.dll", true);

        public static readonly NugetPackage XamarinAndroidSupportv7CardView = new NugetPackage("Xamarin.Android.Support.v7.CardView",
            "Xamarin.Android.Support.v7.CardView, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Android.Support.v7.CardView.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.v7.CardView.dll", true);

        public static readonly NugetPackage XamarinAndroidSupportv7MediaRouter = new NugetPackage("Xamarin.Android.Support.v7.MediaRouter",
            "Xamarin.Android.Support.v7.MediaRouter, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Android.Support.v7.MediaRouter.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.v7.MediaRouter.dll", true);

        public static readonly NugetPackage XamarinFormsPlatformAndroid = new NugetPackage("Xamarin.Forms.Platform.Android",
            "Xamarin.Forms.Platform.Android, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
            @"..\..\packages\Xamarin.Forms.2.2.0.45\lib\MonoAndroid10\Xamarin.Forms.Platform.Android.dll", true);
    }
}
