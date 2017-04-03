using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCodeGen;

namespace SlnGen
{
    public class EmbeddedResourceProjectFile : ProjectFile
    {
        string mSubType { get; set; } = String.Empty;
        public string SubType => mSubType;

        string mGenerator { get; set; } = String.Empty;
        public string Generator => mGenerator;

        public EmbeddedResourceProjectFile(CGFile file, string subType, string generator) : base(file)
        {
            mSubType = subType;
            mGenerator = generator;
        }

        public EmbeddedResourceProjectFile(string fileName, string subType, string generator, string fileContents = null) : base(fileName, false, false, fileContents)
        {
            mSubType = subType;
            mGenerator = generator;
        }
    }
}
