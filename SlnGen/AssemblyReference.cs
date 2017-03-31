using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public class AssemblyReference
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => mName;
        readonly string mName;

        public string HintPath => mHintPath;
        readonly string mHintPath;

        public string IsPrivate => mIsPrivate.ToString();
        readonly bool mIsPrivate;

        public AssemblyReference(string name)
        {
            mName = name;
        }

        public AssemblyReference(string name, string hintPath, bool isPrivate)
        {
            mName = name;
            mHintPath = hintPath;
            mIsPrivate = isPrivate;
        }
    }
}
