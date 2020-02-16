using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using theRightDirection.Library.UnitTesting.Moq;
using Moq;
using AutoFixture.AutoMoq;
using AutoFixture;

namespace theRightDirection.Library.UnitTesting.UnitTests.Moq
{
    [TestClass]
    public class MoqExtensionsTest
    {
        [TestMethod]
        public void ReturnsInOrder_Works_Correctly()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            var mock = fixture.Freeze<Mock<TestInterface>>();
            mock.Setup(m => m.GetNumber()).ReturnsInOrder(5, 10, 3, 0);
            var sut = fixture.Create<TestObject2>();
            var result1 = sut.GetNumber();
            var result2 = sut.GetNumber();
            var result3 = sut.GetNumber();
            var result4 = sut.GetNumber();
            Assert.AreEqual(10, result1);
            Assert.AreEqual(15, result2);
            Assert.AreEqual(8, result3);
            Assert.AreEqual(5, result4);
        }
    }
}