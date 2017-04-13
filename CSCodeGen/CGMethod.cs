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
        //CGMethodSignature mMethodSignature { get; set; }
        //public CGMethodSignature MethodSignature => mMethodSignature;
        public CGMethodSignature MethodSignature { get; set; }

        public List<CGMethodArgument> Arguments
        {
            get { return MethodSignature.Arguments;  }
            set { MethodSignature.Arguments = value; }
        }

        //StringBuilder mMethodTextBuilder { get; set; } = new StringBuilder();
        //public string MethodText => mMethodTextBuilder.ToString();
        public string MethodText { get; set; } = String.Empty;

        public List<string> MethodComments { get; set; } = new List<string>();

        //public CGMethod(string methodName, Type returnType = null, bool isStatic = false)
        //{
        //    MethodSignature = new CGMethodSignature(methodName, returnType, isStatic);
        //}

        //public CGMethod(AccessibilityLevel accessibilityLevel, string methodName, Type returnType = null, bool isStatic = false)
        //{
        //    MethodSignature = new CGMethodSignature(accessibilityLevel, methodName, returnType);
        //}

        public CGMethod(string methodName, string returnType = "void", bool isStatic = false)
        {
            MethodSignature = new CGMethodSignature(methodName, returnType, isStatic);
        }

        public CGMethod(AccessibilityLevel accessibilityLevel, string methodName, string returnType = "void", bool isStatic = false)
        {
            MethodSignature = new CGMethodSignature(accessibilityLevel, methodName, returnType, isStatic);
        }

        public CGMethod(CGMethodSignature methodSignature)
        {
            MethodSignature = methodSignature;
        }

        //public CGMethod(string methodName, Type returnType = null, bool isStatic = false, params CGMethodArgument[] arguments)
        //{
        //    mMethodSignature = new CGMethodSignature(methodName, returnType, isStatic, arguments);
        //}

        //public CGMethod(string methodName, Type returnType = null, bool isStatic = false, List<CGMethodArgument> arguments = null)
        //{
        //    mMethodSignature = new CGMethodSignature(methodName, returnType, isStatic, arguments);
        //}

        //public CGMethod(AccessibilityLevel accessibilityLevel, string methodName, Type returnType = null, bool isStatic = false, params CGMethodArgument[] arguments)
        //{
        //    mMethodSignature = new CGMethodSignature(accessibilityLevel, methodName, returnType, isStatic, arguments);
        //}

        //public CGMethod(AccessibilityLevel accessibilityLevel, string methodName, Type returnType = null, bool isStatic = false, List<CGMethodArgument> arguments = null)
        //{
        //    mMethodSignature = new CGMethodSignature(accessibilityLevel, methodName, returnType, isStatic, arguments);
        //}

        //public void AppendMethodText(string text)
        //{
        //    mMethodTextBuilder.Append(text);
        //}

        //public void AppendLineToMethodText(string textLine)
        //{
        //    mMethodTextBuilder.AppendLine(textLine);
        //}

        //public void ClearMethodText()
        //{
        //    mMethodTextBuilder = new StringBuilder();
        //}

        //public void AddArgument(CGMethodArgument argument)
        //{
        //    mMethodSignature.AddArgument(argument);
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string methodComment in MethodComments)
            {
                sb.AppendLine($"// {methodComment}");
            }
            sb.AppendLine(MethodSignature.ToString());
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
