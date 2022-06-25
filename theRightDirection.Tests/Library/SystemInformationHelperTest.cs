using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            result.Should().Be("Microsoft Windows NT 10.0.22000.0");
        }

    }
}