using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class ProjectReference
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => mName;
        readonly string mName;

        public string Include => mInclude;
        readonly string mInclude;

        public Guid ProjectGuid => mProjectGuid;
        readonly Guid mProjectGuid;

        public ProjectReference(string name, string include, Guid projectGuid)
        {
            mName = name;
            mInclude = include;
            mProjectGuid = projectGuid;
        }
    }
}
