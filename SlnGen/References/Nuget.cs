using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.References
{
    public static class Nuget
    {
        public static readonly NugetPackage NewtonsoftJson_net452 = new NugetPackage("Newtonsoft.Json", "10.0.1", "net452")
        {
            Assemblies =
            {
                new NugetAssembly("Newtonsoft.Json",
                    "Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
                    @"..\..\packages\Newtonsoft.Json.10.0.1\lib\net45\Newtonsoft.Json.dll", true)
            }
        };

        public static readonly NugetPackage XamarinForms_portable45 = new NugetPackage("Xamarin.Forms", "2.3.3.193", "portable45-net45+win8+wp8+wpa81")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Forms.Core",
                    "Xamarin.Forms.Core, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Core.dll", true),
                new NugetAssembly("Xamarin.Forms.Platform",
                    "Xamarin.Forms.Platform, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Platform.dll", true),
                new NugetAssembly("Xamarin.Forms.Xaml",
                    "Xamarin.Forms.Xaml, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\portable-win+net45+wp80+win81+wpa81+MonoAndroid10+Xamarin.iOS10+xamarinmac20\Xamarin.Forms.Xaml.dll", true)
            }
        };

        public static readonly NugetPackage XamarinForms_xamarinios10 = new NugetPackage("Xamarin.Forms", "2.3.3.193", "xamarinios10")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Forms.Core",
                    "Xamarin.Forms.Core, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\Xamarin.iOS10\Xamarin.Forms.Core.dll", true),
                new NugetAssembly("Xamarin.Forms.Platform",
                    "Xamarin.Forms.Platform, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\Xamarin.iOS10\Xamarin.Forms.Platform.dll", true),
                new NugetAssembly("Xamarin.Forms.Platform.iOS",
                    "Xamarin.Forms.Platform.iOS, Version = 2.0.0.0, Culture = neutral, processorArchitecture = MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\Xamarin.iOS10\Xamarin.Forms.Platform.iOS.dll", true),
                new NugetAssembly("Xamarin.Forms.Xaml",
                    "Xamarin.Forms.Xaml, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\Xamarin.iOS10\Xamarin.Forms.Xaml.dll", true)
            }
        };

        public static readonly NugetPackage XamarinForms_monoandroid60 = new NugetPackage("Xamarin.Forms", "2.3.3.193", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("FormsViewGroup",
                    "FormsViewGroup, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\MonoAndroid10\FormsViewGroup.dll", true),
                new NugetAssembly("Xamarin.Forms.Core",
                    "Xamarin.Forms.Core, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\MonoAndroid10\Xamarin.Forms.Core.dll", true),
                new NugetAssembly("Xamarin.Forms.Platform",
                    "Xamarin.Forms.Platform, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\MonoAndroid10\Xamarin.Forms.Platform.dll", true),
                new NugetAssembly("Xamarin.Forms.Platform.Android",
                    "Xamarin.Forms.Platform.iOS, Version = 2.0.0.0, Culture = neutral, processorArchitecture = MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\MonoAndroid10\Xamarin.Forms.Platform.Android.dll", true),
                new NugetAssembly("Xamarin.Forms.Xaml",
                    "Xamarin.Forms.Xaml, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Forms.2.3.3.193\lib\MonoAndroid10\Xamarin.Forms.Xaml.dll", true)
            }
        };

        public static readonly NugetPackage XamarinAndroidSupportAnimatedVectorDrawable_monoandroid60 = new NugetPackage("Xamarin.Android.Support.Animated.Vector.Drawable",
            "23.3.0", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Android.Support.Animated.Vector.Drawable",
                    "Xamarin.Android.Support.Animated.Vector.Drawable, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Android.Support.Animated.Vector.Drawable.23.3.0\lib\MonoAndroid403\Xamarin.Android.Support.Animated.Vector.Drawable.dll", true)
            }
        };

        public static readonly NugetPackage XamarinAndroidSupportDesign_monoandroid60 = new NugetPackage("Xamarin.Android.Support.Design", "23.3.0", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Android.Support.Design",
                    "Xamarin.Android.Support.Design, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Android.Support.Design.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.Design.dll", true)
            }
        };

        public static readonly NugetPackage XamarinAndroidSupportv4_monoandroid60 = new NugetPackage("Xamarin.Android.Support.v4", "23.3.0", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Android.Support.v4",
                    "Xamarin.Android.Support.v4, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Android.Support.v4.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.v4.dll", true)
            }
        };

        public static readonly NugetPackage XamarinAndroidSupportv7AppCompat_monoandroid60 = new NugetPackage("Xamarin.Android.Support.v7.AppCompat", "23.3.0", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Android.Support.v7.AppCompat",
                    "Xamarin.Android.Support.v7.AppCompat, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Android.Support.v7.AppCompat.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.v7.AppCompat.dll", true)
            }
        };

        public static readonly NugetPackage XamarinAndroidSupportv7CardView_monoandroid60 = new NugetPackage("Xamarin.Android.Support.v7.CardView", "23.3.0", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Android.Support.v7.CardView",
                    "Xamarin.Android.Support.v7.CardView, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Android.Support.v7.CardView.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.v7.CardView.dll", true)
            }
        };

        public static readonly NugetPackage XamarinAndroidSupportv7MediaRouter_monoandroid60 = new NugetPackage("Xamarin.Android.Support.v7.MediaRouter", "23.3.0", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Android.Support.v7.MediaRouter",
                    "Xamarin.Android.Support.v7.MediaRouter, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Android.Support.v7.MediaRouter.23.0.1.3\lib\MonoAndroid403\Xamarin.Android.Support.v7.MediaRouter.dll", true)
            }
        };

        public static readonly NugetPackage XamarinAndroidSupportv7RecyclerView_monoandroid60 = new NugetPackage("Xamarin.Android.Support.v7.RecyclerView", "23.3.0", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Android.Support.v7.RecyclerView",
                    "Xamarin.Android.Support.v7.RecyclerView, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Android.Support.v7.RecyclerView.23.3.0\lib\MonoAndroid403\Xamarin.Android.Support.v7.RecyclerView.dll", true)
            }
        };

        public static readonly NugetPackage XamarinAndroidSupportVectorDrawable_monoandroid60 = new NugetPackage("Xamarin.Android.Support.Vector.Drawable", "23.3.0", "monoandroid60")
        {
            Assemblies =
            {
                new NugetAssembly("Xamarin.Android.Support.Vector.Drawable",
                    "Xamarin.Android.Support.Vector.Drawable, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL",
                    @"..\..\packages\Xamarin.Android.Support.Vector.Drawable.23.3.0\lib\MonoAndroid403\Xamarin.Android.Support.Vector.Drawable.dll", true)
            }
        };
    }
}
