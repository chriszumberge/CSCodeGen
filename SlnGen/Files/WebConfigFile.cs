using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlnGen
{
    public sealed class WebConfigFile : ConfigFile
    {
        public WebConfigFile() : base ("web")
        {
            
        }
    }
}
