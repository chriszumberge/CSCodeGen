using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CSCodeGen.VsTests
{
    [TestClass]
    public class CSFile_ToStringTests
    {
        [TestMethod]
        public void CGFileToString()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName);
            Assert.AreEqual(String.Empty, file.ToString());
        }

        [TestMethod]
        public void CGFileWithSingleUsingToString()
        {
            string fileName = "TestFile";
            string assemblyName = "System.Text";
            CGUsingStatement usingStatement = new CGUsingStatement(assemblyName);
            CGFile file = new CGFile(fileName, new List<CGUsingStatement> { usingStatement });
            Assert.AreEqual(FileStrings.CGFileWithSingleUsing, file.ToString());
        }

        [TestMethod]
        public void CGFileWithSingleNamespaceToString()
        {
            string fileName = "TestFile";
            string namespaceName = "System.IO";
            CGNamespace newNamespace = new CGNamespace(namespaceName);
            CGFile file = new CGFile(fileName, null, new List<CGNamespace> { newNamespace });
            Assert.AreEqual(FileStrings.CGFileWithSingleNamespace, file.ToString());
        }

        [TestMethod]
        public void CGFileWithMultipleUsingToString()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName,
                new List<CGUsingStatement>
                {
                    new CGUsingStatement("System.Text"),
                    new CGUsingStatement("System.IO"),
                    new CGUsingStatement("System.IO.SomethingElse")
                });
            Assert.AreEqual(FileStrings.CGFileWithMultipleUsing, file.ToString());
        }

        [TestMethod]
        public void CGFileWithUsingAndNamespaceToString()
        {
            CGFile file = new CGFile("TestFile",
                new List<CGUsingStatement>
                {
                    new CGUsingStatement("System.Text"),
                    new CGUsingStatement("System.IO")
                },
                new List<CGNamespace>
                {
                    new CGNamespace("MyProject.HelloWorld")
                });
            Assert.AreEqual(FileStrings.CGFileWithUsingAndNamespace, file.ToString());
        }
    }

    public static class FileStrings
    {
        public static string CGFileWithSingleUsing =
            String.Concat(
                "using System.Text;", Environment.NewLine
                );
        public static string CGFileWithSingleNamespace =
            String.Concat(
                "namespace System.IO", Environment.NewLine,
                "{", Environment.NewLine,
                "}", Environment.NewLine,
                Environment.NewLine
                );
        public static string CGFileWithMultipleUsing =
            String.Concat(
                "using System.Text;", Environment.NewLine,
                "using System.IO;", Environment.NewLine,
                "using System.IO.SomethingElse;", Environment.NewLine
                );
        public static string CGFileWithUsingAndNamespace =
            String.Concat(
                "using System.Text;", Environment.NewLine,
                "using System.IO;", Environment.NewLine,
                "namespace MyProject.HelloWorld", Environment.NewLine,
                "{", Environment.NewLine,
                "}", Environment.NewLine,
                Environment.NewLine);
    }
}
