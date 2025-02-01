using FluentAssertions;

namespace theRightDirection.Tests;
public class ExtensionStringTest
{
    [Fact]
    public void GetNameWithoutSpecialCharacters()
    {
        var allowedCharacters = new[] { ' ', '_', '-', '.' };
        var folderName = "KLIC Genius";
        folderName = folderName.RemoveSpecialCharactersFromString(extraAllowedCharacters: allowedCharacters);
        folderName.Should().Be("KLIC Genius");
    }
}
