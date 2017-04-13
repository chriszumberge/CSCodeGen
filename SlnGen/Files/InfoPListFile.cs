using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen
{
    public sealed class InfoPListFile : ProjectFile
    {
        public InfoPListFile(string appName, string bundleIdentifier) : base("Info.plist", false, false)
        {
            XDocument plistDoc = new XDocument(
                new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", ""),
                new XElement("plist",
                    new XAttribute("version", "1.0"),
                    new XElement("dict",
                        new XElement("key",
                            new XText("UIDeviceFamily")
                        ),
                        new XElement("array",
                            new XElement("integer",
                                new XText("1")
                            ),
                            new XElement("integer",
                                new XText("2")
                            )
                        ),
                        new XElement("key",
                            new XText("UISupportedInterfaceOrientations")
                        ),
                        new XElement("array",
                            new XElement("string",
                                new XText("UIInterfaceOrientationPortrait")
                            ),
                            new XElement("string",
                                new XText("UIInterfaceOrientationLandscapeLeft")
                            ),
                            new XElement("string",
                                new XText("UIInterfaceOrientationLandscapeRight")
                            )
                        ),
                        new XElement("key",
                            new XText("UISupportedInterfaceOrientations~ipad")
                        ),
                        new XElement("array",
                            new XElement("string",
                                new XText("UIInterfaceOrientationPortrait")
                            ),
                            new XElement("string",
                                new XText("UIInterfaceOrientationPortraitUpsideDown")
                            ),
                            new XElement("string",
                                new XText("UIInterfaceOrientationLandscapeLeft")
                            ),
                            new XElement("string",
                                new XText("UIInterfaceOrientationLandscapeRight")
                            )
                        ),
                        new XElement("key",
                            new XText("MinimumOSVersion")
                        ),
                        new XElement("string",
                            new XText("8.0")
                        ),
                        new XElement("key",
                            new XText("CFBundleDisplayName")
                        ),
                        new XElement("string",
                            new XText(appName)
                        ),
                        new XElement("key",
                            new XText("CFBundleIdentifier")
                        ),
                        new XElement("string",
                            new XText(bundleIdentifier)
                        ),
                        new XElement("key",
                            new XText("CFBundleVersion")
                        ),
                        new XElement("string",
                            new XText("1.0")
                        ),
                         new XElement("key",
                            new XText("CFBundleIconFiles~ipad")
                        ),
                        new XElement("array",
                            new XElement("string",
                                new XText("Icon-60@2x")
                            ),
                            new XElement("string",
                                new XText("Icon-60@3x")
                            ),
                            new XElement("string",
                                new XText("Icon-76")
                            ),
                            new XElement("string",
                                new XText("Icon-76@2x")
                            ),
                            new XElement("string",
                                new XText("Default")
                            ),
                            new XElement("string",
                                new XText("Default@2x")
                            ),
                            new XElement("string",
                                new XText("Default-568h@2x")
                            ),
                            new XElement("string",
                                new XText("Default-Portrait")
                            ),
                            new XElement("string",
                                new XText("Default-Portrait@2x")
                            ),
                            new XElement("string",
                                new XText("Icon-Small-40")
                            ),
                            new XElement("string",
                                new XText("Icon-Small-40@2x")
                            ),
                            new XElement("string",
                                new XText("Icon-Small-40@3x")
                            ),
                            new XElement("string",
                                new XText("Icon-Small")
                            ),
                            new XElement("string",
                                new XText("Icon-Small@2x")
                            ),
                            new XElement("string",
                                new XText("Icon-Small@3x")
                            )
                        ),
                        new XElement("key",
                            new XText("UILaunchStoryboardName")
                        ),
                        new XElement("string",
                            new XText("LaunchScreen")
                        )
                    )
                )
            );

            using (var memoryStream = new MemoryStream())
            {
                plistDoc.Save(memoryStream);

                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    string contents = streamReader.ReadToEnd();
                    FileContents = contents;
                }
            }
        }
    }
}
