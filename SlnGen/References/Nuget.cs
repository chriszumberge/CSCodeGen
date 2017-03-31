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
            @"..\packages\Newtonsoft.Json.10.0.1\lib\net45\Newtonsoft.Json.dll", true);
    }
}
