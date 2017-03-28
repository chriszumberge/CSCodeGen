using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCodeGen
{
    public sealed class CGNamespace
    {
        readonly string mNamespaceName;
        public string NamespaceName => mNamespaceName;

        List<CGInterface> mInterfaces { get; set; } = new List<CGInterface>();
        public IReadOnlyList<CGInterface> Interfaces => mInterfaces.AsReadOnly();

        List<CGClass> mClasses { get; set; } = new List<CGClass>();
        public IReadOnlyList<CGClass> Classes => mClasses.AsReadOnly();

        public CGNamespace(string namespaceName, IEnumerable<CGInterface> interfaces = null)
        {
            if (namespaceName == null)
            {
                throw new ArgumentNullException(nameof(namespaceName));
            }
            if (namespaceName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(namespaceName));
            }

            mNamespaceName = namespaceName.Replace(" ", ".");

            if (interfaces != null)
            {
                mInterfaces = interfaces.ToList();
            }
        }

        public void AddInterface(CGInterface @interface)
        {
            if (@interface != null)
            {
                mInterfaces.Add(@interface);
            }
        }
        
        public void RemoveInterface(CGInterface @interface)
        {
            if (@interface != null)
            {
                mInterfaces.Remove(@interface);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace {mNamespaceName}");
            sb.AppendLine("{");
            foreach (var @interface in Interfaces)
            {
                string[] interfaceLines = @interface.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string interfaceLine in interfaceLines)
                {
                    sb.AppendLine($"\t{interfaceLine}");
                }
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
