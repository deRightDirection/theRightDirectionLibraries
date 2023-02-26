using System;
using FluentAssertions;
using Xunit;

namespace theRightDirection.Library.UnitTests;
public class ObjectExtensionsTest
{
    [Fact]
    public void DeepClone_With_No_Cloning_Copy_Has_Same_Value()
    {
        var original = new Company()
        {
            GBRank = 123,
            desc = new CompanyDescription("Mannus", "Mannus")
        };
        var sut = original;
        sut.GBRank = 456;
        sut.desc.CompanyName = "Mannus BV";
        original.GBRank.Should().Be(456);
        original.desc.CompanyName.Should().Be("Mannus BV");
        sut.GBRank.Should().Be(456);
        sut.desc.CompanyName.Should().Be("Mannus BV");
    }
    [Fact]
    public void DeepClone_With_Cloning_Copy_Has_Not_Same_Value()
    {
        var original = new Company()
        {
            GBRank = 123,
            desc = new CompanyDescription("Mannus", "Mannus")
        };
        var sut = original.DeepClone();
        sut.GBRank = 456;
        sut.desc.CompanyName = "Mannus BV";
        original.GBRank.Should().Be(123);
        original.desc.CompanyName.Should().Be("Mannus");
        sut.GBRank.Should().Be(456);
        sut.desc.CompanyName.Should().Be("Mannus BV");
    }
}

[Serializable]
class Company
{
    public int GBRank { get; set; }
    public CompanyDescription desc { get; set; }
}
[Serializable]
class CompanyDescription
{

    public string CompanyName;
    public string Owner;
    public CompanyDescription(string c, string o)
    {
        CompanyName = c;
        Owner = o;
    }
}