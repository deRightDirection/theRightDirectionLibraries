using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FluentAssertions;
namespace theRightDirection.Library.UnitTesting.UnitTests
{
    [TestClass]
    public class UnitTestingBaseTest
    {
        private UnitTestHelper _testHelper;

        [TestInitialize]
        public void Setup()
        {
            _testHelper = new UnitTestHelper();
        }

        [TestMethod]
        public void DeleteFile_Is_Correct()
        {
            var fileName = "mannus.txt";
            var path = Path.Combine(_testHelper.UnitTestDirectory, fileName);
            File.WriteAllText(path, "mannus");
            File.Exists(path).Should().BeTrue();
            _testHelper.DeleteFileFromUnitTestDirectory(fileName);
            File.Exists(path).Should().BeFalse(); ;
        }
    }

    public class UnitTestHelper : UnitTestingBase
    {
    }
}
