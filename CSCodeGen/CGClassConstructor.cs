//
// CGClassConstructor.cs
//
//
// Author:
//       Chris Zumberge <chriszumberge@gmail.com>
//
// Copyright (c) 2017 Christopher Zumberge
//
// All rights reserved
//
using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodeGen
{
    public class CGClassConstructor
    {
        protected AccessibilityLevel mAccessibilityLevel { get; set; }
        public AccessibilityLevel AccessibilityLevel => mAccessibilityLevel;

        protected string mClassName { get; set; }
        public string ClassName => mClassName;

        //List<CGMethodArgument> mArguments { get; set; } = new List<CGMethodArgument>();
        //public List<CGMethodArgument> Arguments => mArguments;
        public List<CGMethodArgument> Arguments { get; set; } = new List<CGMethodArgument>();

        //StringBuilder mConstructorTextBuilder { get; set; } = new StringBuilder();
        //public string ConstructorText => mConstructorTextBuilder.ToString();
        //public string ConstructorText { get; set; } = String.Empty;
        public List<string> ConstructorText { get; set; } = new List<string>();

        public List<string> BaseClassArguments { get; set; } = new List<string>();

        public CGClassConstructor(string className)
        {
            mClassName = className;
            mAccessibilityLevel = AccessibilityLevel.Public;
        }

        public CGClassConstructor(AccessibilityLevel accessibilityLevel, string className)
        {
            mClassName = className;
            mAccessibilityLevel = accessibilityLevel;
        }

        //public void AppendConstructorText(string text)
        //{
        //    mConstructorTextBuilder.Append(text);
        //}

        //public void AppendLineToConstructorText(string textLine)
        //{
        //    mConstructorTextBuilder.AppendLine(textLine);
        //}

        //public void ClearConstructorText()
        //{
        //    mConstructorTextBuilder = new StringBuilder();
        //}

        //public void AddArgument(CGMethodArgument argument)
        //{
        //    mArguments.Add(argument);
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel.ToString()} ");
            sb.Append($"{ClassName} (");

            for (int i = 0; i < Arguments.Count; i++)
            {
                var argument = Arguments[i];
                sb.Append($"{argument.ToString()}");
                if (i + 1 < Arguments.Count)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(")");

            if (BaseClassArguments.Count > 0)
            {
                sb.Append(" : base (");
                sb.Append(String.Join(", ", BaseClassArguments));
                sb.Append(")");
            }

            sb.AppendLine();
            sb.AppendLine("{");

            //string[] ctorTextLines = ConstructorText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            //foreach (string ctorTextLine in ctorTextLines)
            //{
            //    sb.AppendLine($"\t{ctorTextLine}");
            //}
            foreach (string ctorLine in ConstructorText)
            {
                sb.AppendLine($"\t{ctorLine}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
