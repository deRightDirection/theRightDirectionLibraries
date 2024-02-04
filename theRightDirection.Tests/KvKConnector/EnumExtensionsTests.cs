using HR.KvkConnector.Model;
using HR.KvkConnector.Model.Zoeken;

namespace theRightDirection.Tests.KvKConnector;

public class EnumExtensionsTests
{
    [Fact]
    public void GetStringValue_ByDefault_ReturnsStringValue()
    {
        // Arrange
        var enumMember = Adrestype.Bezoekadres;

        // Act
        var stringValue = enumMember.GetStringValue();

        // Assert
        Assert.AreEqual("bezoekadres", stringValue);
    }

    [Theory]
    [InlineData(Vestigingstype.Rechtspersoon | Vestigingstype.Hoofdvestiging, "hoofdvestiging, rechtspersoon")]
    [InlineData(Vestigingstype.Hoofdvestiging | Vestigingstype.Nevenvestiging, "hoofdvestiging, nevenvestiging")]
    [InlineData(Vestigingstype.Rechtspersoon | Vestigingstype.Hoofdvestiging | Vestigingstype.Nevenvestiging, "hoofdvestiging, nevenvestiging, rechtspersoon")]
    public void GetStringValue_GivenFlagsEnum_ReturnsExpectedStringValue(Vestigingstype enumMember, string expectedStringValue)
    {
        // Arrange

        // Act
        var stringValue = enumMember.GetStringValue();

        // Assert
        Assert.AreEqual(expectedStringValue, stringValue);
    }

    [Fact]
    public void GetStringValue_GivenDuplicateFlagsEnum_ReturnsNonduplicatedStringValue()
    {
        // Arrange
        var enumMember = Vestigingstype.Hoofdvestiging | Vestigingstype.Hoofdvestiging;

        // Act
        var stringValue = enumMember.GetStringValue();

        // Assert
        Assert.AreEqual("hoofdvestiging", stringValue);
    }

    [Fact]
    public void GetStringValue_GivenEnumWithoutEnumMemberAttribute_ReturnsToStringResult()
    {
        // Arrange
        var enumMember = JaNeeIndicatie.Ja;

        // Act
        var stringValue = enumMember.GetStringValue();

        // Assert
        Assert.AreEqual("Ja", stringValue);
    }
}