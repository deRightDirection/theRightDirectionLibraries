using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using theRightDirection.Library;

namespace theRightDirection.Tests.Library
{
    [TestClass]
    public class SystemInformationHelperTest
    {
        [TestMethod]
        public void WindowsVersionName()
        {
            var systemInformation = new SystemInformationHelper();
            var result = systemInformation.WindowsVersionName;
            result.Should().Be("Microsoft Windows NT 10.0.18363.0");
        }

    }
}