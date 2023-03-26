using FluentAssertions;
using Xunit;

namespace theRightDirection.Library.UnitTests
{
    public class SystemInformationHelperTest
    {
        [Fact]
        public void WindowsVersionName()
        {
            var systemInformation = new SystemInformationHelper();
            var result = systemInformation.WindowsVersionName;
            result.Should().Be("Microsoft Windows NT 10.0.22621.0");
        }
    }
}