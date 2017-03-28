using System;
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

        List<string> mImplementations { get; set; } = new List<string>();
        public List<string> Implementations => mImplementations;

        string mBaseClassName { get; set; }
        public string BaseClassName => mBaseClassName;

        List<CGClassProperty> mClassProperties { get; set; } = new List<CGClassProperty>();
        public List<CGClassProperty> ClassProperties => mClassProperties;

        List<CGClassConstructor> mClassConstructors { get; set; } = new List<CGClassConstructor>();
        public List<CGClassConstructor> ClassConstructors => mClassConstructors;

        List<CGClassMethod> mClassMethods { get; set; } = new List<CGClassMethod>();
        public List<CGClassMethod> ClassMethods => mClassMethods;

        List<CGClass> mInternalClasses { get; set; } = new List<CGClass>();
        public List<CGClass> InternalClasses => mInternalClasses;

        List<CGInterface> mInternalInterfaces { get; set; } = new List<CGInterface>();
        public List<CGInterface> InternalInterfaces => mInternalInterfaces;

        List<CGEnum> mInternalEnums { get; set; } = new List<CGEnum>();
        public List<CGEnum> InternalEnums => mInternalEnums;


    }
}
