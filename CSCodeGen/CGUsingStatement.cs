using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCodeGen
{
    public sealed class CGUsingStatement
    {
        readonly string mAssemblyName;
        public string AssemblyName => mAssemblyName;

        public CGUsingStatement(string assemblyName)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }
            if (assemblyName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(assemblyName));
            }

            mAssemblyName = assemblyName.Replace(" ", String.Empty);
        }

        public override string ToString()
        {
            return $"using {mAssemblyName};";
        }
    }
}
