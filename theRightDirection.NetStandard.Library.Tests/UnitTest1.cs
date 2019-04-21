using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace theRightDirection.NetStandard.Library.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var x = "Mannus";
            var encryptedX = x.Encrypt("Etten");
            Assert.AreEqual("0txln/1QimcoEH9SELjXLg==", encryptedX);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var x = "0txln/1QimcoEH9SELjXLg==";
            var decryptedX = x.Decrypt("Etten");
            Assert.AreEqual("Mannus", decryptedX);
        }
    }
}
