using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public sealed class AppDelegateFile : ProjectFile
    {
        public AppDelegateFile() : base("AppDelegate.cs", true, false)
        {
            
        }
    }
}
