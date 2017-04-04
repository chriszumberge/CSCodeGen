using CSCodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen
{
    public class DefaultAndroidResourceDesignerCreator
    {
        public static CGFile GetFile(string assemblyName)
        {
            return new CGFile("Resource.Designer.cs")
            {
                Namespaces =
                {
                    new CGNamespace(assemblyName)
                    {
                        Classes =
                        {
                            new PartialCGClass("Resource")
                            {
                                InternalClasses =
                                {
                                    new PartialCGClass("Attribute")
                                    {
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "Attribute")
                                        }
                                    },
                                    new PartialCGClass("Drawable")
                                    {
                                        ClassFields =
                                        {
                                            new ConstCGClassField("int", "Icon", "2130837504")
                                        },
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "Drawable")
                                        }
                                    },
                                    new PartialCGClass("Id")
                                    {
                                        ClassFields =
                                        {
                                            new ConstCGClassField("int", "MyButton", "2131034112")
                                        },
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "Id")
                                        }
                                    },
                                    new PartialCGClass("Layout")
                                    {
                                        ClassFields =
                                        {
                                            new ConstCGClassField("int", "Main", "2130903040")
                                        },
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "Layout")
                                        }
                                    },
                                    new PartialCGClass("String")
                                    {
                                        ClassFields =
                                        {
                                            new ConstCGClassField("int", "ApplicationName", "2130968577"),
                                            new ConstCGClassField("int", "Hello", "2130968576")
                                        },
                                        ClassConstructors =
                                        {
                                            new CGClassConstructor(AccessibilityLevel.Private, "String")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
