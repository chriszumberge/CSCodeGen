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

        //List<CGInterface> mInterfaces { get; set; } = new List<CGInterface>();
        //public IReadOnlyList<CGInterface> Interfaces => mInterfaces.AsReadOnly();
        public List<CGInterface> Interfaces { get; set; } = new List<CGInterface>();

        //List<CGClass> mClasses { get; set; } = new List<CGClass>();
        //public IReadOnlyList<CGClass> Classes => mClasses.AsReadOnly();
        public List<CGClass> Classes { get; set; } = new List<CGClass>();

        //List<CGEnum> mEnums { get; set; } = new List<CGEnum>();
        //public List<CGEnum> Enums => mEnums;
        public List<CGEnum> Enums { get; set; } = new List<CGEnum>();

        //List<CGStruct> mStructs { get; set; } = new List<CGStruct>();
        //public List<CGStruct> Structs => mStructs;
        public List<CGStruct> Structs { get; set; } = new List<CGStruct>();

        public CGNamespace(string namespaceName)
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
        }

        //public CGNamespace(string namespaceName, IEnumerable<CGInterface> interfaces = null, IEnumerable<CGClass> classes = null)
        //{
        //    if (namespaceName == null)
        //    {
        //        throw new ArgumentNullException(nameof(namespaceName));
        //    }
        //    if (namespaceName.Length == 0)
        //    {
        //        throw new ArgumentException("Argument cannot be an empty string.", nameof(namespaceName));
        //    }

        //    mNamespaceName = namespaceName.Replace(" ", ".");

        //    if (interfaces != null)
        //    {
        //        Interfaces = interfaces.ToList();
        //    }

        //    if (classes != null)
        //    {
        //        mClasses = classes.ToList();
        //    }
        //}

        //public void AddInterface(CGInterface @interface)
        //{
        //    if (@interface != null)
        //    {
        //        Interfaces.Add(@interface);
        //    }
        //}

        //public void RemoveInterface(CGInterface @interface)
        //{
        //    if (@interface != null)
        //    {
        //        Interfaces.Remove(@interface);
        //    }
        //}

        //public void AddClass(CGClass @class)
        //{
        //    if (@class != null)
        //    {
        //        mClasses.Add(@class);
        //    }
        //}

        //public void RemoveClass(CGClass @class)
        //{
        //    if (@class != null)
        //    {
        //        mClasses.Remove(@class);
        //    }
        //}


        //public void AddEnum(CGEnum @enum)
        //{
        //    if (@enum != null)
        //    {
        //        mEnums.Add(@enum);
        //    }
        //}

        //public void AddStruct(CGStruct @struct)
        //{
        //    if (@struct != null)
        //    {
        //        mStructs.Add(@struct);
        //    }
        //}

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
            foreach (var @class in Classes)
            {
                string[] classLines = @class.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string classLine in classLines)
                {
                    sb.AppendLine($"\t{classLine}");
                }
            }
            foreach (var @enum in Enums)
            {
                string[] enumLines = @enum.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string enumLine in enumLines)
                {
                    sb.AppendLine($"\t{enumLine}");
                }
            }
            foreach (var @struct in Structs)
            {
                string[] structLines = @struct.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string structLine in structLines)
                {
                    sb.AppendLine($"\t{structLine}");
                }
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
