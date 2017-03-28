using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSCodeGen;

namespace CSCodeGen.VsTests
{
    [TestClass]
    public class CSUsingStatementTests
    {
        [TestMethod]
        public void CtorPreservesAssemblyReferenceName()
        {
            string assemblyName = "testAssembly";
            var usingStatement = new CGUsingStatement(assemblyName);
            Assert.AreEqual(assemblyName, usingStatement.AssemblyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorWithNullAssemblyName_ThrowsNullArgumentException()
        {
            new CGUsingStatement(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CtorWithEmptyAssemblyName_ThrowsArgumentException()
        {
            new CGUsingStatement(String.Empty);
        }

        [TestMethod]
        public void CtorWithAssemblyNameContainingSpaces_StripsSpaces()
        {
            string assemblyName = "Some Test Assembly";
            var usingStatement = new CGUsingStatement(assemblyName);
            Assert.AreEqual(assemblyName.Replace(" ", String.Empty), usingStatement.AssemblyName);
        }

        [TestMethod]
        public void ValidUsingStatmentToString()
        {
            string assemblyName = "System.Text";
            var usingStatment = new CGUsingStatement(assemblyName);
            Assert.AreEqual("using System.Text;", usingStatment.ToString());
        }
    }
}
