using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen.Files
{
    public sealed class AppConfigFile : ConfigFile
    {
        public AppConfigFile() : base("App")
        {
            var configNode = new XElement("configuration",
                                    new XElement("startup",
                                        new XElement("supportedRuntime",
                                            new XAttribute("version", "v4.0"),
                                            new XAttribute("sku", ".NETFramework,Version=v4.5.2")
                                        )
                                    )
                                );

            using (var memoryStream = new MemoryStream())
            {
                configNode.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    string content = streamReader.ReadToEnd();
                    mFileContents = content;
                }
            }
        }
    }
}
