using System.Linq;
using FluentAssertions;
using Xunit;

namespace theRightDirection.Library.UnitTests;

public class ResourceReaderTest
{
    private readonly ResourceReader _resourceReader;

    public ResourceReaderTest()
    {
        _resourceReader = new ResourceReader();
    }

    [Fact]
    public void GetResources_No_Folder()
    {
        var resources = _resourceReader.GetResources();
        resources.Count().Should().Be(1);
        var data = _resourceReader.ReadDataFromResourceAsFile(resources.First());
        data.Length.Should().BeGreaterThanOrEqualTo(9);
    }
    [Fact]
    public void GetResources_With_Folder()
    {
        var resources = _resourceReader.GetResources("TestResources");
        resources.Count().Should().Be(1);
        var data = _resourceReader.ReadDataFromResourceAsFile(resources.First(), "TestResources");
        data.Length.Should().BeGreaterThanOrEqualTo(9);
    }

    [Fact]
    public void Logo_Is_Found_As_Resource()
    {
        var image = _resourceReader.ReadDataFromResourceAsFile("tRD-logo-1regel.png");
        image.Length.Should().BeGreaterOrEqualTo(10);
    }
    [Fact]
    public void Text_Is_Read_From_Resource()
    {
        var text = _resourceReader.ReadDataFromResource("test.txt", "TestResources");
        text.Should().Be("hallo!");
    }
    [Fact]
    public void Text_In_Bytes_From_Resource()
    {
        var text = _resourceReader.ReadDataFromResourceAsFile("test.txt", "TestResources");
        text.Length.Should().BeGreaterOrEqualTo(9);
    }
}