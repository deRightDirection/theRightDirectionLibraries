using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace theRightDirection.Library.UnitTesting.UnitTests
{
    [TestClass]
    public class MockBuilderTest
    {
        [TestMethod]
        public void BuildMock_Is_Ok()
        {
            Assert.Inconclusive();
            /*
            var mock = MockBuilder.BuildMock<TestInterface>();
            mock.Verify(m => m.GetNumber(), Times.Never());
            var sut = new TestObject2(mock.Object);
            Assert.IsFalse(string.IsNullOrEmpty(sut.GetName()));
            */
        }
    }
}