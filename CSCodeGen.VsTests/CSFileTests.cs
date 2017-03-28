using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSCodeGen;
using System.Collections.Generic;
using System.Linq;

namespace CSCodeGen.VsTests
{
    [TestClass]
    public class CSFileTests
    {
        [TestMethod]
        public void CtorPerservesFileName()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName);
            Assert.AreEqual(file.FileName, fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorWithNullFileName_ThrowsArugmentNullException()
        {
            new CGFile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CtorWithEmptyFileName_ThrowsArgumentException()
        {
            new CGFile(String.Empty);
        }

        [TestMethod]
        public void CtorWithNoAssemblies_InitializesNotNullUsingStatementList()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName);
            Assert.AreNotEqual(null, file.UsingStatments);
        }

        [TestMethod]
        public void CtorWithNoAssemblies_InitializesEmptyList()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName);
            Assert.AreEqual(0, file.UsingStatments.Count);
        }

        [TestMethod]
        public void CtorWithNullAssemblies_InitializesNotNullUsingStatementList()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName, null);
            Assert.AreNotEqual(null, file.UsingStatments);
        }

        [TestMethod]
        public void CtorWithNullAssemblies_InitializesEmptyList()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName, null);
            Assert.AreEqual(0, file.UsingStatments.Count);
        }

        [TestMethod]
        public void CtorPreservesNumberOfUsingStatments()
        {
            string assemblyName = "TestAssembly";
            string fileName = "TestFile";
            CGUsingStatement usingStmt = new CGUsingStatement(assemblyName);
            var assemblies = new List<CGUsingStatement> { usingStmt };
            CGFile file = new CGFile(fileName, assemblies);
            Assert.AreEqual(assemblies.Count, file.UsingStatments.Count);
        }

        [TestMethod]
        public void CtorPreservesUsingStatmentAssemblyNames()
        {
            string assemblyName = "TestAssembly";
            string fileName = "TestFile";
            CGUsingStatement usingStmt = new CGUsingStatement(assemblyName);
            var assemblies = new List<CGUsingStatement> { usingStmt };
            CGFile file = new CGFile(fileName, assemblies);
            Assert.AreEqual(String.Join(" ", assemblies.Select(x => x.AssemblyName)), 
                String.Join(" ", file.UsingStatments.Select(x => x.AssemblyName)));
        }

        [TestMethod]
        public void CtorWithNoNamespaces_InitializesNotNullNamespacesList()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName);
            Assert.AreNotEqual(null, file.Namespaces);
        }

        [TestMethod]
        public void CtorWithNoNamespaces_InitializesEmptyList()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName);
            Assert.AreEqual(0, file.Namespaces.Count);
        }

        [TestMethod]
        public void CtorWithNullNamespaces_InitializesNotNullNamespaceList()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName, null, null);
            Assert.AreNotEqual(null, file.UsingStatments);
        }

        [TestMethod]
        public void CtorWithNullNamespaces_InitializesEmptyList()
        {
            string fileName = "TestFile";
            CGFile file = new CGFile(fileName, null, null);
            Assert.AreEqual(0, file.UsingStatments.Count);
        }

        [TestMethod]
        public void CtorPreservesNumberOfNamespaces()
        {
            string namespaceName = "TestNamespace";
            string fileName = "TestFile";
            CGNamespace nmsp = new CGNamespace(namespaceName);
            var namespaces = new List<CGNamespace> { nmsp };
            CGFile file = new CGFile(fileName, null, namespaces);
            Assert.AreEqual(namespaces.Count, file.Namespaces.Count);
        }

        [TestMethod]
        public void CtorPreservesNamespaceNames()
        {
            string namespaceName = "TestNamespace";
            string fileName = "TestFile";
            CGNamespace nmsp = new CGNamespace(namespaceName);
            var namespaces = new List<CGNamespace> { nmsp };
            CGFile file = new CGFile(fileName, null, namespaces);
            Assert.AreEqual(String.Join(" ", namespaces.Select(x => x.NamespaceName)),
                String.Join(" ", file.Namespaces.Select(x => x.NamespaceName)));
        }

        [TestMethod]
        public void AddingUsingStatementToListProperty_ThrowsException()
        {
            CGFile file = new CGFile("TestFile");
            file.UsingStatments.ToList().Add(new CGUsingStatement("TestAssembly"));
            Assert.AreEqual(0, file.UsingStatments.Count);
        }
    }
}
