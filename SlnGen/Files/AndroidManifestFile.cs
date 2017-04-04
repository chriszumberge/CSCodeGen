using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class AndroidManifestFile : ProjectFile
    {
        public AndroidManifestFile() : base("AndroidManifest.xml", false, false)
        {

        }
    }
}
