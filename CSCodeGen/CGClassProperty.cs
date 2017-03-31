using System;
using System.Text;

namespace CSCodeGen
{
    public class CGClassProperty
    {
        protected AccessibilityLevel mAccessibilityLevel { get; set; }
        public AccessibilityLevel AccessibilityLevel => mAccessibilityLevel;

        protected bool mIsStatic { get; set; } = false;
        public bool IsStatic => mIsStatic;

        protected Type mPropertyType { get; set; }
        //public Type PropertyType => mPropertyType;
        protected string mCustomPropertyType { get; set; }
        public string PropertyTypeName
        {
            get
            {
                if (String.IsNullOrEmpty(mCustomPropertyType))
                {
                    return mPropertyType.Name;
                }
                else
                {
                    return mCustomPropertyType;
                }
            }
        }

        protected string mPropertyName { get; set; }
        public string PropertyName => mPropertyName;

        AccessibilityLevel mGetterAccessibilityLevel { get; set; }
        public AccessibilityLevel GetterAccessibilityLevel => mGetterAccessibilityLevel;

        //StringBuilder mGetterTextBuilder { get; set; } = new StringBuilder();
        //public string GetterText => mGetterTextBuilder.ToString();
        public string GetterText { get; set; } = String.Empty;

        AccessibilityLevel mSetterAccessibilityLevel { get; set; }
        public AccessibilityLevel SetterAccessibilityLevel => mSetterAccessibilityLevel;

        //StringBuilder mSetterTextBuilder { get; set; } = new StringBuilder();
        //public string SetterText => mSetterTextBuilder.ToString();
        public string SetterText { get; set; } = String.Empty;

        public CGClassProperty(AccessibilityLevel accessibilityLevel, string propertyType, string propertyName, bool isStatic = false,
            AccessibilityLevel getterAccessibilityLevel = null, AccessibilityLevel setterAccessibilityLevel = null)
        {
            mAccessibilityLevel = accessibilityLevel;
            mCustomPropertyType = propertyType;
            mPropertyName = propertyName;
            mIsStatic = isStatic;
            if (getterAccessibilityLevel == null)
            {
                mGetterAccessibilityLevel = AccessibilityLevel.Public;
            }
            else
            {
                mGetterAccessibilityLevel = getterAccessibilityLevel;
            }
            if (setterAccessibilityLevel == null)
            {
                mSetterAccessibilityLevel = AccessibilityLevel.Public;
            }
            else
            {
                mSetterAccessibilityLevel = setterAccessibilityLevel;
            }
        }

        public CGClassProperty(AccessibilityLevel accessibilityLevel, Type propertyType, string propertyName, bool isStatic = false,
            AccessibilityLevel getterAccessibilityLevel = null, AccessibilityLevel setterAccessibilityLevel = null)
        {
            mAccessibilityLevel = accessibilityLevel;
            mPropertyType = propertyType;
            mPropertyName = propertyName;
            mIsStatic = isStatic;
            if (getterAccessibilityLevel == null)
            {
                mGetterAccessibilityLevel = AccessibilityLevel.Public;
            }
            else
            {
                mGetterAccessibilityLevel = getterAccessibilityLevel;
            }
            if (setterAccessibilityLevel == null)
            {
                mSetterAccessibilityLevel = AccessibilityLevel.Public;
            }
            else
            {
                mSetterAccessibilityLevel = setterAccessibilityLevel;
            }
        }

        //public void AppendGetterText(string text)
        //{
        //    mGetterTextBuilder.Append(text);
        //}
        //public void AppendLineToGetterText(string textLine)
        //{
        //    mGetterTextBuilder.AppendLine(textLine);
        //}
        //public void ClearGetterText()
        //{
        //    mGetterTextBuilder = new StringBuilder();
        //}
        //public void AppendSetterText(string text)
        //{
        //    mSetterTextBuilder.Append(text);
        //}
        //public void AppendLineToSetterText(string textLine)
        //{
        //    mSetterTextBuilder.AppendLine(textLine);
        //}
        //public void ClearSetterText()
        //{
        //    mSetterTextBuilder = new StringBuilder();
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel} ");
            if (IsStatic) { sb.Append("static "); }
            sb.Append($"{PropertyTypeName} ");
            sb.AppendLine($"{PropertyName}");
            sb.AppendLine("{");

            sb.Append("\t");
            if (!GetterAccessibilityLevel.Equals(AccessibilityLevel.Public))
            {
                sb.Append($"{GetterAccessibilityLevel} ");
            }
            sb.Append("get");

            if (GetterText.Length == 0) { sb.AppendLine(";"); }
            else
            {
                sb.AppendLine();
                sb.AppendLine("\t{");
                string[] getterTextLines = GetterText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach(string getterTextLine in getterTextLines)
                {
                    sb.AppendLine($"\t\t{getterTextLine}");
                }
                sb.AppendLine("\t}");
            }

            sb.Append("\t");
            if (!SetterAccessibilityLevel.Equals(AccessibilityLevel.Public))
            {
                sb.Append($"{SetterAccessibilityLevel} ");
            }
            sb.Append("set");

            if (SetterText.Length == 0) { sb.AppendLine(";"); }
            else
            {
                sb.AppendLine();
                sb.AppendLine("\t{");
                string[] setterTextLines = SetterText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach(string setterTextLine in setterTextLines)
                {
                    sb.AppendLine($"\t\t{setterTextLine}");
                }
                sb.AppendLine("\t}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}