using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Files
{
    public class ConfigFile : ProjectFile
    {
        public ConfigFile(string fileName)
            : base(fileName+ ".config", false, false, null)
        {
            
        }
    }
}
