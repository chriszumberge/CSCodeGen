﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCodeGen
{
    public sealed class CGClass
    {
        AccessibilityLevel mAccessibilityLevel { get; set; }
        public AccessibilityLevel AccessibilityLevel => mAccessibilityLevel;

        bool mIsStatic { get; set; } = false;
        public bool IsStatic => mIsStatic;

        bool mIsAbstract { get; set; } = false;
        public bool IsAbstract => mIsAbstract;

        string mClassName { get; set; }
        public string ClassName => mClassName;

        //List<string> mImplementations { get; set; } = new List<string>();
        //public List<string> Implementations => mImplementations;
        public List<string> Implementations { get; set; } = new List<string>();

        string mBaseClassName { get; set; }
        public string BaseClassName => mBaseClassName;

        //List<CGClassProperty> mClassProperties { get; set; } = new List<CGClassProperty>();
        //public List<CGClassProperty> ClassProperties => mClassProperties;
        public List<CGClassProperty> ClassProperties { get; set; } = new List<CGClassProperty>();

        //List<CGClassConstructor> mClassConstructors { get; set; } = new List<CGClassConstructor>();
        //public List<CGClassConstructor> ClassConstructors => mClassConstructors;
        public List<CGClassConstructor> ClassConstructors { get; set; } = new List<CGClassConstructor>();

        //List<CGMethod> mClassMethods { get; set; } = new List<CGMethod>();
        //public List<CGMethod> ClassMethods => mClassMethods;
        public List<CGMethod> ClassMethods { get; set; } = new List<CGMethod>();

        //List<CGClass> mInternalClasses { get; set; } = new List<CGClass>();
        //public List<CGClass> InternalClasses => mInternalClasses;
        public List<CGClass> InternalClasses { get; set; } = new List<CGClass>();

        //List<CGInterface> mInternalInterfaces { get; set; } = new List<CGInterface>();
        //public List<CGInterface> InternalInterfaces => mInternalInterfaces;
        public List<CGInterface> InternalInterfaces { get; set; } = new List<CGInterface>();

        //List<CGEnum> mInternalEnums { get; set; } = new List<CGEnum>();
        //public List<CGEnum> InternalEnums => mInternalEnums;
        public List<CGEnum> InternalEnums { get; set; } = new List<CGEnum>();

        public CGClass(string className, bool isStatic = false, bool isAbstract = false)
        {
            mAccessibilityLevel = AccessibilityLevel.Public;
            mClassName = className;
            mIsStatic = isStatic;
            mIsAbstract = isAbstract;
        }

        public CGClass(string className, string baseClassName, bool isStatic = false, bool isAbstract = false)
        {
            mAccessibilityLevel = AccessibilityLevel.Public;
            mClassName = className;
            mBaseClassName = baseClassName;
            mIsStatic = isStatic;
            mIsAbstract = isAbstract;
        }

        public CGClass(AccessibilityLevel accessibilityLevel, string className, bool isStatic = false, bool isAbstract = false)
        {
            mAccessibilityLevel = accessibilityLevel;
            mClassName = className;
            mIsStatic = isStatic;
            mIsAbstract = isAbstract;
        }

        public CGClass(AccessibilityLevel accessibilityLevel, string className, string baseClassName, bool isStatic = false, bool isAbstract = false)
        {
            mAccessibilityLevel = accessibilityLevel;
            mClassName = className;
            mBaseClassName = baseClassName;
            mIsStatic = isStatic;
            mIsAbstract = isAbstract;
        }

        //public void AddInterfaceImplementation(string interfaceImplementation)
        //{
        //    mImplementations.Add(interfaceImplementation);
        //}

        //public void SetBaseClass(string baseClassName)
        //{
        //    mBaseClassName = baseClassName;
        //}

        //public void AddClassProperty(CGClassProperty classProperty)
        //{
        //    mClassProperties.Add(classProperty);
        //}

        //public void AddClassConstructor(CGClassConstructor classConstructor)
        //{
        //    mClassConstructors.Add(classConstructor);
        //}

        //public void AddClassMethod(CGMethod classMethod)
        //{
        //    mClassMethods.Add(classMethod);
        //}

        //public void AddInternalClass(CGClass internalClass)
        //{
        //    mInternalClasses.Add(internalClass);
        //}

        //public void AddInternalInterface(CGInterface internalInterface)
        //{
        //    mInternalInterfaces.Add(internalInterface);
        //}

        //public void AddInternalEnum(CGEnum internalEnum)
        //{
        //    mInternalEnums.Add(internalEnum);
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessibilityLevel.ToString()} ");
            if (mIsStatic) { sb.Append("static "); }
            if (mIsAbstract) { sb.Append("abstract "); }
            sb.Append($"class {ClassName}");

            if (!String.IsNullOrEmpty(BaseClassName) || Implementations.Count > 0)
            {
                sb.Append(" : ");
                if (!String.IsNullOrEmpty(BaseClassName))
                {
                    sb.Append($"{BaseClassName}");
                }

                if (!String.IsNullOrEmpty(BaseClassName) && Implementations.Count > 0)
                {
                    sb.Append($", ");
                }

                sb.Append(String.Join(", ", Implementations));
            }
            sb.AppendLine();
            sb.AppendLine("{");

            foreach (CGClassProperty property in ClassProperties)
            {
                string[] propertyLines = property.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string propertyLine in propertyLines)
                {
                    sb.AppendLine($"\t{propertyLine}");
                }
            }
            foreach (CGClassConstructor ctor in ClassConstructors)
            {
                string[] ctorLines = ctor.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string ctorLine in ctorLines)
                {
                    sb.AppendLine($"\t{ctorLine}");
                }
            }
            foreach (CGMethod method in ClassMethods)
            {
                string[] methodLines = method.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string methodLine in methodLines)
                {
                    sb.AppendLine($"\t{methodLine}");
                }
            }
            foreach (CGClass cls in InternalClasses)
            {
                string[] classLines = cls.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string classLine in classLines)
                {
                    sb.AppendLine($"\t{classLine}");
                }
            }
            foreach (CGInterface inf in InternalInterfaces)
            {
                string[] infLines = inf.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string infLine in infLines)
                {
                    sb.AppendLine($"\t{infLine}");
                }
            }
            foreach (CGEnum enm in InternalEnums)
            {
                string[] enumLines = enm.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string enumLine in enumLines)
                {
                    sb.AppendLine($"\t{enumLine}");
                }
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
