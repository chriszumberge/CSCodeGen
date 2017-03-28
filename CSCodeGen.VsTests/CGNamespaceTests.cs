using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CSCodeGen.VsTests
{
    [TestClass]
    public class CGNamespaceTests
    {
         [TestMethod]
        public void CtorPreservesNamespaceName()
        {
            string namespaceName = "testNamespace";
            var newNamespace = new CGNamespace(namespaceName);
            Assert.AreEqual(namespaceName, newNamespace.NamespaceName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorWithNullNamespaceName_ThrowsNullArgumentExceptino()
        {
            new CGNamespace(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CtorWithEmptyNamespaceName_ThrowsArgumentException()
        {
            new CGNamespace(String.Empty);
        }

        [TestMethod]
        public void CtorWithNamespaceNameContainingSpaces_JoinsWithPeriods()
        {
            string namespaceName = "System IO Text";
            var newNamespace = new CGNamespace(namespaceName);
            Assert.AreEqual(namespaceName.Replace(" ", "."), newNamespace.NamespaceName);
        }

        [TestMethod]
        public void ValidNamespaceToString()
        {
            string namespaceName = "System.Text";
            var newNamespace = new CGNamespace(namespaceName);
            Assert.AreEqual($"namespace {namespaceName}{Environment.NewLine}{{{Environment.NewLine}}}{Environment.NewLine}",
                newNamespace.ToString());
        }

        [TestMethod]
        public void CtorWithNoInterfaces_InitializesNotNullInterfacesList()
        {
            string namespaceName = "System.Text";
            var @namespace = new CGNamespace(namespaceName);
            Assert.AreNotEqual(null, @namespace.Interfaces);
        }

        [TestMethod]
        public void CtorWithNoInterfaces_InitializesEmptyList()
        {
            string namespaceName = "System.Text";
            var @namespace = new CGNamespace(namespaceName);
            Assert.AreEqual(0, @namespace.Interfaces.Count);
        }

        [TestMethod]
        public void CtorWithNullInterfaces_InitializesNotNullInterfacesList()
        {
            string namespaceName = "System.Text";
            var @namespace = new CGNamespace(namespaceName);
            Assert.AreNotEqual(null, @namespace.Interfaces);
        }

        [TestMethod]
        public void CtorWithNullInterfaces_InitializesEmptyList()
        {
            string namespaceName = "System.Text";
            var @namespace = new CGNamespace(namespaceName);
            Assert.AreEqual(0, @namespace.Interfaces.Count);
        }

        [TestMethod]
        public void CtorPreservesNumberOfUsingStatments()
        {
            string interfaceName = "IPlugin";
            var @interface = new CGInterface(interfaceName);
            var interfaces = new List<CGInterface> { @interface };

            string namespaceName = "System.Text";

            CGNamespace @namespace = new CGNamespace(namespaceName, interfaces);
            Assert.AreEqual(interfaces.Count, @namespace.Interfaces.Count);
        }
    }
}
