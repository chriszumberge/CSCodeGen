using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCodeGen;

namespace SlnGen
{
    public class AndroidResourceProjectFile : ProjectFile
    {
        public AndroidResourceProjectFile(CGFile file) : base(file, false, false)
        {
        }

        public AndroidResourceProjectFile(string fileName) : base(fileName, false, false)
        {
        }

        public AndroidResourceProjectFile(string fileName, string fileContents) : base(fileName, false, false, fileContents)
        {
        }
    }
}
