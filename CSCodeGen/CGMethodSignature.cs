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

        bool mIsOverride { get; set; } = false;
        public bool IsOverride => mIsOverride;

        //Type mReturnType { get; set; } = typeof(void);
        //public Type ReturnType => mReturnType;
        string mReturnType { get; set; } = "void";
        public string ReturnType => mReturnType;

        string mMethodName { get; set; }
        public string MethodName => mMethodName;

        //List<CGMethodArgument> mArguments { get; set; } = new List<CGMethodArgument>();
        //public List<CGMethodArgument> Arguments => mArguments;
        public List<CGMethodArgument> Arguments { get; set; } = new List<CGMethodArgument>();

        bool mIsGeneric { get; set; } = false;
        public bool IsGeneric => mIsGeneric;

        List<string> mGenericTypeNames { get; set; } = new List<string>();
        public List<string> GenericTypeNames => mGenericTypeNames;

        public CGMethodSignature(string methodName, string returnType = "void", bool isStatic = false, bool isOverride = false, IEnumerable<string> genericTypeNames = null)
        {
            mAccessibilityLevel = AccessibilityLevel.Public;
            mIsStatic = isStatic;
            mIsOverride = isOverride;
            mMethodName = methodName;
            mReturnType = returnType;
            if (genericTypeNames != null)
            {
                mIsGeneric = true;
                mGenericTypeNames = genericTypeNames.ToList();
            }
        }

        public CGMethodSignature(AccessibilityLevel accessibilityLevel, string methodName, string returnType = "void", bool isStatic = false, bool isOverride = false, IEnumerable<string> genericTypeNames = null)
        {
            mAccessibilityLevel = accessibilityLevel;
            mIsStatic = isStatic;
            mIsOverride = isOverride;
            mMethodName = methodName;
            mReturnType = returnType;
            if (genericTypeNames != null)
            {
                mIsGeneric = true;
                mGenericTypeNames = genericTypeNames.ToList();
            }
        }

        //public CGMethodSignature(string methodName, Type returnType = null, bool isStatic = false, IEnumerable<string> genericTypeNames = null)
        //{
        //    if (returnType == null) { returnType = typeof(void); }

        //    mAccessibilityLevel = AccessibilityLevel.Public;
        //    mIsStatic = isStatic;
        //    mMethodName = methodName;
        //    mReturnType = returnType;
        //    if (genericTypeNames != null)
        //    {
        //        mIsGeneric = true;
        //        mGenericTypeNames = genericTypeNames.ToList();
        //    }
        //}

        //public CGMethodSignature(AccessibilityLevel accessibilityLevel, string methodName, Type returnType = null, bool isStatic = false, IEnumerable<string> genericTypeNames = null)
        //{
        //    if (returnType == null) { returnType = typeof(void); }

        //    mAccessibilityLevel = accessibilityLevel;
        //    mIsStatic = isStatic;
        //    mMethodName = methodName;
        //    mReturnType = returnType;
        //    if (genericTypeNames != null)
        //    {
        //        mIsGeneric = true;
        //        mGenericTypeNames = genericTypeNames.ToList();
        //    }
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel} ");
            if (IsStatic) { sb.Append("static "); }
            if (IsOverride) { sb.Append("override "); }
            sb.Append($"{ReturnType} ");
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
