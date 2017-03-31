using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCodeGen
{
    public sealed class CGInterface
    {
        readonly string mInterfaceName;
        public string InterfaceName => mInterfaceName;

        AccessibilityLevel mAccessibilityLevel { get; set; }
        public AccessibilityLevel AccessibilityLevel => mAccessibilityLevel;

        //List<CGMethodSignature> mInterfaceMethods { get; set; } = new List<CGMethodSignature>();
        //public List<CGMethodSignature> InterfaceMethods => mInterfaceMethods;
        List<CGMethodSignature> InterfaceMethods { get; set; } = new List<CGMethodSignature>();

        //List<string> mInterfacesImplemented { get; set; } = new List<string>();
        //public List<string> InterfacesImplemented => mInterfacesImplemented;
        List<string> InterfacesImplemented { get; set; } = new List<string>();

        bool mIsGeneric { get; set; } = false;
        public bool IsGeneric => mIsGeneric;

        List<string> mGenericTypeNames { get; set; } = new List<string>();
        public List<string> GenericTypeNames => mGenericTypeNames;

        public CGInterface(string interfaceName)
        {
            if (interfaceName == null)
            {
                throw new ArgumentNullException(nameof(interfaceName));
            }
            if (interfaceName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(interfaceName));
            }

            mInterfaceName = interfaceName.Replace(" ", "_");

            mAccessibilityLevel = AccessibilityLevel.Public;
        }

        public CGInterface(AccessibilityLevel accessibilityLevel, string interfaceName)
        {
            if (interfaceName == null)
            {
                throw new ArgumentNullException(nameof(interfaceName));
            }
            if (interfaceName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(interfaceName));
            }

            mInterfaceName = interfaceName.Replace(" ", "_");

            mAccessibilityLevel = accessibilityLevel;
        }

        public CGInterface(AccessibilityLevel accessibilityLevel, string interfaceName, IEnumerable<string> genericTypeNames = null)
        {
            if (interfaceName == null)
            {
                throw new ArgumentNullException(nameof(interfaceName));
            }
            if (interfaceName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be an empty string.", nameof(interfaceName));
            }

            mInterfaceName = interfaceName.Replace(" ", "_");

            mAccessibilityLevel = accessibilityLevel;

            if (genericTypeNames != null)
            {
                mIsGeneric = true;
                mGenericTypeNames = genericTypeNames.ToList();
            }
        }

        //public CGInterface(AccessibilityLevel accessibilityLevel, string interfaceName, IEnumerable<CGMethodSignature> interfaceMethods = null)
        //{
        //    if (interfaceName == null)
        //    {
        //        throw new ArgumentNullException(nameof(interfaceName));
        //    }
        //    if (interfaceName.Length == 0)
        //    {
        //        throw new ArgumentException("Argument cannot be an empty string.", nameof(interfaceName));
        //    }

        //    mInterfaceName = interfaceName.Replace(" ", "_");

        //    mAccessibilityLevel = accessibilityLevel;

        //    if (interfaceMethods != null)
        //    {
        //        mInterfaceMethods = interfaceMethods.ToList();
        //    }
        //}

        //public CGInterface(AccessibilityLevel accessibilityLevel, string interfaceName, IEnumerable<string> interfacesImplemented, IEnumerable<CGMethodSignature> interfaceMethods = null, IEnumerable<string> genericTypeNames = null)
        //{
        //    if (interfaceName == null)
        //    {
        //        throw new ArgumentNullException(nameof(interfaceName));
        //    }
        //    if (interfaceName.Length == 0)
        //    {
        //        throw new ArgumentException("Argument cannot be an empty string.", nameof(interfaceName));
        //    }

        //    mInterfaceName = interfaceName.Replace(" ", "_");

        //    mAccessibilityLevel = accessibilityLevel;

        //    if (interfacesImplemented != null)
        //    {
        //        mInterfacesImplemented = interfacesImplemented.ToList();
        //    }

        //    if (interfaceMethods != null)
        //    {
        //        mInterfaceMethods = interfaceMethods.ToList();
        //    }

        //    if (genericTypeNames != null)
        //    {
        //        mIsGeneric = true;
        //        mGenericTypeNames = genericTypeNames.ToList();
        //    }
        //}

        //public void AddInterface(string @interface)
        //{
        //    if (@interface != null)
        //    {
        //        mInterfacesImplemented.Add(@interface);
        //    }
        //}

        //public void RemoveInterface(string @interface)
        //{
        //    if (@interface != null)
        //    {
        //        mInterfacesImplemented.Remove(@interface);
        //    }
        //}

        //public void AddInterfaceMethod(CGMethodSignature interfaceMethod)
        //{
        //    if (interfaceMethod != null)
        //    {
        //        mInterfaceMethods.Add(interfaceMethod);
        //    }
        //}

        //public void RemoveInterfaceMethod(CGMethodSignature interfaceMethod)
        //{
        //    if (interfaceMethod != null)
        //    {
        //        mInterfaceMethods.Remove(interfaceMethod);
        //    }
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel} interface {InterfaceName}");
            if (IsGeneric)
            {
                sb.Append("<");
                sb.Append(String.Join(", ", GenericTypeNames));
                sb.Append(">");
            }
            if (InterfacesImplemented.Count > 0)
            {
                sb.Append(String.Concat(" : ", String.Join(", ", InterfacesImplemented)));
            }
            sb.AppendLine();
            sb.AppendLine("{");
            foreach (var methodSignature in InterfaceMethods)
            {
                sb.AppendLine($"\t{methodSignature.ToString()};");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
