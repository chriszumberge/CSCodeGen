//
// CGClassMethod.cs
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
    public class CGMethod
    {
        CGMethodSignature mMethodSignature { get; set; }
        public CGMethodSignature MethodSignature => mMethodSignature;

        StringBuilder mMethodTextBuilder { get; set; } = new StringBuilder();
        public string MethodText => mMethodTextBuilder.ToString();

        public CGMethod(string methodName, Type returnType = null, bool isStatic = false, params CGMethodArgument[] arguments)
        {
            mMethodSignature = new CGMethodSignature(methodName, returnType, isStatic, arguments);
        }

        public CGMethod(string methodName, Type returnType = null, bool isStatic = false, List<CGMethodArgument> arguments = null)
        {
            mMethodSignature = new CGMethodSignature(methodName, returnType, isStatic, arguments);
        }

        public CGMethod(AccessibilityLevel accessibilityLevel, string methodName, Type returnType = null, bool isStatic = false, params CGMethodArgument[] arguments)
        {
            mMethodSignature = new CGMethodSignature(accessibilityLevel, methodName, returnType, isStatic, arguments);
        }

        public CGMethod(AccessibilityLevel accessibilityLevel, string methodName, Type returnType = null, bool isStatic = false, List<CGMethodArgument> arguments = null)
        {
            mMethodSignature = new CGMethodSignature(accessibilityLevel, methodName, returnType, isStatic, arguments);
        }

        public void AppendMethodText(string text)
        {
            mMethodTextBuilder.Append(text);
        }

        public void AppendLineToMethodText(string textLine)
        {
            mMethodTextBuilder.AppendLine(textLine);
        }

        public void ClearMethodText()
        {
            mMethodTextBuilder = new StringBuilder();
        }

        public void AddArgument(CGMethodArgument argument)
        {
            mMethodSignature.AddArgument(argument);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mMethodSignature.ToString());
            sb.AppendLine("{");

            string[] methodTextLines = MethodText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string methodTextLine in methodTextLines)
            {
                sb.AppendLine($"\t{methodTextLine}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
