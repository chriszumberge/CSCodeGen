using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCodeGen
{
    public sealed class CGMethodSignature
    {
        AccessibilityLevel mAccessibilityLevel { get; set; }
        public AccessibilityLevel AccessibilityLevel => mAccessibilityLevel;

        bool mIsStatic { get; set; } = false;
        public bool IsStatic => mIsStatic;

        Type mReturnType { get; set; } = typeof(void);
        public Type ReturnType => mReturnType;

        string mMethodName { get; set; }
        public string MethodName => mMethodName;

        List<CGMethodArgument> mArguments { get; set; } = new List<CGMethodArgument>();
        public List<CGMethodArgument> Arguments => mArguments;

        bool mIsGeneric { get; set; } = false;
        public bool IsGeneric => mIsGeneric;

        List<string> mGenericTypeNames { get; set; } = new List<string>();
        public List<string> GenericTypeNames => mGenericTypeNames;

        public CGMethodSignature(string methodName, Type returnType = null, bool isStatic = false, IEnumerable<CGMethodArgument> arguments = null, IEnumerable<string> genericTypeNames = null)
        {
            if (returnType == null) { returnType = typeof(void); }

            mAccessibilityLevel = AccessibilityLevel.Public;
            mIsStatic = isStatic;
            mMethodName = methodName;
            if (arguments != null)
            {
                mArguments = arguments.ToList();
            }
            mReturnType = returnType;
            if (genericTypeNames != null)
            {
                mIsGeneric = true;
                mGenericTypeNames = genericTypeNames.ToList();
            }
        }

        public CGMethodSignature(AccessibilityLevel accessibilityLevel, string methodName, Type returnType = null, bool isStatic = false, IEnumerable<CGMethodArgument> arguments = null, IEnumerable<string> genericTypeNames = null)
        {
            if (returnType == null) { returnType = typeof(void); }

            mAccessibilityLevel = accessibilityLevel;
            mIsStatic = isStatic;
            mMethodName = methodName;
            if (arguments != null)
            {
                mArguments = arguments.ToList();
            }
            mReturnType = returnType;
            if (genericTypeNames != null)
            {
                mIsGeneric = true;
                mGenericTypeNames = genericTypeNames.ToList();
            }
        }

        public void AddArgument(CGMethodArgument argument)
        {
            mArguments.Add(argument);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel} ");
            if (IsStatic) { sb.Append("static "); }
            sb.Append($"{ReturnType.Name} ");
            sb.Append($"{MethodName}");

            if (IsGeneric && GenericTypeNames.Count > 0)
            {
                sb.Append("<");
                sb.Append(String.Join(", ", GenericTypeNames));
                sb.Append(">");
            }

            sb.Append(" (");
            sb.Append(String.Join(", ", Arguments.Select(x => x.ToString())));
            sb.Append(")");

            return sb.ToString();
        }
    }
}
