using FluentAssertions;
using theRightDirection;

namespace Library.Extensions;
public class LongExtensionsTest
{
    [Theory]
    [InlineData(376832, "368 kb")]
    [InlineData(8255669248, "7,69 Gb")]
    public void Size_To_Text(long fileSize, string expectedResult)
    {
        var text = fileSize.ToFileLengthRepresentation();
        text.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(104857600L, "100 Mb")]
    [InlineData(1678635836L, "1,56 Gb")]
    public void long_as_filesize(long fileSize, string expectedResult)
    {
        var text = fileSize.ToFileLengthRepresentation();
        text.Should().Be(expectedResult);
    }
}