using System;
using System.Text;

namespace CSCodeGen
{
    public class CGClassField
    {
        protected AccessibilityLevel mAccessibilityLevel { get; set; }
        public AccessibilityLevel AccessibilityLevel => mAccessibilityLevel;

        protected bool mIsStatic { get; set; } = false;
        public bool IsStatic => mIsStatic;

        protected bool mIsConst { get; set; } = false;
        public bool IsConst => mIsConst;

        protected bool mIsReadOnly { get; set; } = false;
        public bool IsReadOnly => mIsReadOnly;

        protected string mFieldType { get; set; }
        public string FieldType => mFieldType;

        protected string mFieldName { get; set; }
        public string FieldName => mFieldName;

        protected string mInitializerValue { get; set; } = null;
        public string InitializerValue => mInitializerValue;

        public CGClassField(string fieldType, string fieldName, string initializerValue = null)
        {
            mAccessibilityLevel = AccessibilityLevel.Public;
            mFieldType = fieldType;
            mFieldName = fieldName;

            mInitializerValue = initializerValue;
        }

        public CGClassField(AccessibilityLevel accessibilityLevel, string fieldType, string fieldName, string initializerValue = null)
        {
            mAccessibilityLevel = accessibilityLevel;
            mFieldName = fieldName;
            mFieldType = fieldType;
            mInitializerValue = initializerValue;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel} ");
            if (IsStatic) { sb.Append("static "); }
            if (IsConst) { sb.Append("const "); }
            if (IsReadOnly) { sb.Append("readonly "); }
            sb.Append($"{FieldType} ");
            sb.Append($"{FieldName}");
            if (!String.IsNullOrEmpty(InitializerValue))
            {
                sb.Append($" = {InitializerValue}");
            }
            sb.AppendLine(";");
            return sb.ToString();
        }
    }

    public sealed class StaticCGClassField : CGClassField
    {
        public StaticCGClassField(string fieldType, string fieldName, string initializerValue = null) : base(fieldType, fieldName, initializerValue)
        {
            mIsStatic = true;
        }

        public StaticCGClassField(AccessibilityLevel accessibilityLevel, string fieldType, string fieldName, string initializerValue = null) : base(accessibilityLevel, fieldType, fieldName, initializerValue)
        {
            mIsStatic = true;
        }
    }

    public sealed class ConstCGClassField : CGClassField
    {
        public ConstCGClassField(string fieldType, string fieldName, string initializerValue = null) : base(fieldType, fieldName, initializerValue)
        {
            mIsConst = true;
        }

        public ConstCGClassField(AccessibilityLevel accessibilityLevel, string fieldType, string fieldName, string initializerValue = null) : base(accessibilityLevel, fieldType, fieldName, initializerValue)
        {
            mIsConst = true;
        }
    }

    public sealed class ReadOnlyCGClassField : CGClassField
    {
        public ReadOnlyCGClassField(string fieldType, string fieldName, string initializerValue = null) : base(fieldType, fieldName, initializerValue)
        {
            mIsReadOnly = true;
        }

        public ReadOnlyCGClassField(AccessibilityLevel accessibilityLevel, string fieldType, string fieldName, string initializerValue = null) : base(accessibilityLevel, fieldType, fieldName, initializerValue)
        {
            mIsReadOnly = true;
        }
    }
}