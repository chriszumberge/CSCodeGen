using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSCodeGen.VsTests
{
    [TestClass]
    public class AccessibilityLevelTests
    {
        [TestMethod]
        public void PublicToStringIsCorrect()
        {
            Assert.AreEqual("Public", AccessibilityLevel.Public.ToString());
        }

        [TestMethod]
        public void PrivateToStringIsCorrect()
        {
            Assert.AreEqual("Private", AccessibilityLevel.Private.ToString());
        }

        [TestMethod]
        public void ProtectedToStringIsCorrect()
        {
            Assert.AreEqual("Protected", AccessibilityLevel.Protected.ToString());
        }

        [TestMethod]
        public void InternalToStringIsCorrect()
        {
            Assert.AreEqual("Internal", AccessibilityLevel.Internal.ToString());
        }

        [TestMethod]
        public void PublicCastIntIsCorrect()
        {
            Assert.AreEqual(1, (int)AccessibilityLevel.Public);
        }

        [TestMethod]
        public void PrivateCastIntIsCorrect()
        {
            Assert.AreEqual(2, (int)AccessibilityLevel.Private);
        }

        [TestMethod]
        public void ProtectedCastIntIsCorrect()
        {
            Assert.AreEqual(3, (int)AccessibilityLevel.Protected);
        }

        [TestMethod]
        public void InternalCastIntIsCorrect()
        {
            Assert.AreEqual(4, (int)AccessibilityLevel.Internal);
        }

    }
}
