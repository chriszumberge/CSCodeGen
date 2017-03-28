using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCodeGen
{
    public sealed class CGFile
    {
        readonly string mFileName;
        public string FileName => mFileName;

        List<CGUsingStatement> mUsingStatements { get; set; } = new List<CGUsingStatement>();
        public IReadOnlyList<CGUsingStatement> UsingStatments => mUsingStatements.AsReadOnly();

        List<CGNamespace> mNamespaces { get; set; } = new List<CGNamespace>();
        public IReadOnlyList<CGNamespace> Namespaces => mNamespaces;

        public CGFile(string fileName, IEnumerable<CGUsingStatement> usingStatements = null, IEnumerable<CGNamespace> namespaces = null)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileName));
            }

            mFileName = fileName;

            if (usingStatements != null)
            {
                mUsingStatements = usingStatements.ToList();
                mUsingStatements = mUsingStatements.OrderBy(us => us.AssemblyName).ToList();
            }

            if (namespaces != null)
            {
                mNamespaces = namespaces.ToList();
            }
        }

        public void AddUsingStatment(CGUsingStatement usingStatement)
        {
            if (usingStatement != null)
            {
                mUsingStatements.Add(usingStatement);
                mUsingStatements = mUsingStatements.OrderBy(us => us.AssemblyName).ToList();
            }
        }

        public void AddNamespace(CGNamespace @namespace)
        {
            if (@namespace != null)
            {
                mNamespaces.Add(@namespace);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var usingStatment in mUsingStatements)
            {
                sb.AppendLine(usingStatment.ToString());
            }

            foreach (var @namespace in mNamespaces)
            {
                sb.AppendLine(@namespace.ToString());
            }

            return sb.ToString();
        }
    }
}
