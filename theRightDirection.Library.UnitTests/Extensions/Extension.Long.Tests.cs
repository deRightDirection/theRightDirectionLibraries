using FluentAssertions;
using Xunit;

namespace theRightDirection.Library.UnitTests.Extensions;
public class LongExtensionsTest
{
    [Theory]
    [InlineData(104857600L, "100 Mb")]
    [InlineData(1678635836L, "1,56 Gb")]
    public void long_as_filesize(long fileSize, string expectedResult)
    {
        var text = fileSize.ToFileLengthRepresentation();
        text.Should().Be(expectedResult);
    }
}