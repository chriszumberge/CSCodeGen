using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSCodeGen.VsTests
{
    [TestClass]
    public class CGInterfaceTests
    {
        [TestMethod]
        public void CtorPreservesInterfaceAssemblyName()
        {
            string interfaceName = "IPlugin";
            var @interface = new CGInterface(interfaceName);
            Assert.AreEqual(interfaceName, @interface.InterfaceName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorWithNullInterfaceName_ThrowsNullArgumentException()
        {
            new CGInterface(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CtorWithEmptyInterfaceName_ThrowsArgumentException()
        {
            new CGInterface(String.Empty);
        }

        [TestMethod]
        public void CtorWithAssemblyInterfacenameContainingSpaces_JoinsOnUnderscore()
        {
            string interfaceName = "Some Interface Name";
            var @interface = new CGInterface(interfaceName);
            Assert.AreEqual(interfaceName.Replace(" ", "_"), @interface.InterfaceName);
        }
    }
}
