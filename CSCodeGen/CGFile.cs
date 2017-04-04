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

        readonly string mFileExtension;
        public string FileExtension => mFileExtension;

        //List<CGUsingStatement> mUsingStatements { get; set; } = new List<CGUsingStatement>();
        //public IReadOnlyList<CGUsingStatement> UsingStatments => mUsingStatements.AsReadOnly();
        public List<CGUsingStatement> UsingStatements { get; set; } = new List<CGUsingStatement>();

        //List<CGNamespace> mNamespaces { get; set; } = new List<CGNamespace>();
        //public IReadOnlyList<CGNamespace> Namespaces => mNamespaces;
        public List<CGNamespace> Namespaces { get; set; } = new List<CGNamespace>();

        public List<string> AdditionalLines { get; set; } = new List<string>();

        public CGFile(string fileNameWithExtension)
        {
            int idx = fileNameWithExtension.LastIndexOf(".");
            string fileName = fileNameWithExtension.Substring(0, idx);
            string fileExtension = fileNameWithExtension.Substring(idx + 1, fileNameWithExtension.Length - idx - 1);

            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileName));
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }
            if (fileExtension.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileExtension));
            }

            mFileName = fileName;
            mFileExtension = fileExtension;
        }

        public CGFile(string fileNameWithExtension, params string[] usingAssemblies)
        {
            int idx = fileNameWithExtension.LastIndexOf(".");
            string fileName = fileNameWithExtension.Substring(0, idx);
            string fileExtension = fileNameWithExtension.Substring(idx + 1, fileNameWithExtension.Length - idx);

            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileName));
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }
            if (fileExtension.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileExtension));
            }

            mFileName = fileName;
            mFileExtension = fileExtension;

            UsingStatements = usingAssemblies.Select(x => new CGUsingStatement(x)).OrderBy(x => x.AssemblyName).ToList();
        }

        public CGFile(string fileName, string fileExtension)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileName));
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }
            if (fileExtension.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileExtension));
            }

            mFileName = fileName;
            mFileExtension = fileExtension;
        }
        public CGFile(string fileName, string fileExtension, params string[] usingAssemblies)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileName));
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }
            if (fileExtension.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileExtension));
            }

            mFileName = fileName;
            mFileExtension = fileExtension;

            UsingStatements = usingAssemblies.Select(x => new CGUsingStatement(x)).OrderBy(x => x.AssemblyName).ToList();
        }

        public CGFile(string fileName, string fileExtension, IEnumerable<CGUsingStatement> usingStatements = null, IEnumerable<CGNamespace> namespaces = null)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (fileName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileName));
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }
            if (fileExtension.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(fileExtension));
            }

            mFileName = fileName;
            mFileExtension = fileExtension;

            if (usingStatements != null)
            {
                //mUsingStatements = usingStatements.ToList();
                //mUsingStatements = mUsingStatements.OrderBy(us => us.AssemblyName).ToList();
                UsingStatements = usingStatements.ToList();
                UsingStatements = UsingStatements.OrderBy(us => us.AssemblyName).ToList();
            }

            if (namespaces != null)
            {
                //mNamespaces = namespaces.ToList();
                Namespaces = namespaces.ToList();
            }
        }

        public void AddUsingStatment(CGUsingStatement usingStatement)
        {
            if (usingStatement != null)
            {
                //mUsingStatements.Add(usingStatement);
                //mUsingStatements = mUsingStatements.OrderBy(us => us.AssemblyName).ToList();
                UsingStatements.Add(usingStatement);
                UsingStatements = UsingStatements.OrderBy(us => us.AssemblyName).ToList();
            }
        }

        public void AddNamespace(CGNamespace @namespace)
        {
            if (@namespace != null)
            {
                //mNamespaces.Add(@namespace);
                Namespaces.Add(@namespace);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var usingStatment in UsingStatements)
            {
                sb.AppendLine(usingStatment.ToString());
            }

            sb.AppendLine();

            foreach (var @namespace in Namespaces)
            {
                sb.AppendLine(@namespace.ToString());
            }

            sb.AppendLine();

            foreach (var line in AdditionalLines)
            {
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
