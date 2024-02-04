using FluentAssertions;
using theRightDirection.KvKConnector;
using theRightDirection.KvKConnector.Model;

namespace theRightDirection.Tests.KvKConnector;
public class KvkApiClientTest
{
    private readonly KvKApiClient _kvk;

    public KvkApiClientTest()
    {
        _kvk = new KvKApiClient("l7xxd760b2c8441c490c941d6f2f81e6e387");
    }

    [Fact]
    public async Task Zoek_By_KvkNummer()
    {
        var result = await _kvk.Search(new SearchParameters { KvkNummer = "85058769" });
        result.Resultaten.First().Naam.Should().Be("the Right Direction B.V.");
    }
    [Fact]
    public async Task Zoek_By_Naam()
    {
        var result = await _kvk.Search(new SearchParameters { Handelsnaam = "the right direction" });
        result.Resultaten.First().Naam.Should().Be("the Right Direction B.V.");
    }
}