using FluentAssertions;
using HR.KvkConnector.Model.Zoeken;
using theRightDirection.KvKConnector;
using theRightDirection.KvKConnector.Model;

namespace theRightDirection.Tests.KvKConnector;
public class KvkApiClientTest
{

    [Fact]
    public async Task Zoek_By_KvkNummer()
    {
        var kvkClient = KvKApiClientFactory.CreateKvKClient();
        var result = await kvkClient.Search(new SearchParameters { KvkNummer = "85058769" }, "l7xxd760b2c8441c490c941d6f2f81e6e387");
        var result2 = await kvkClient.SearchRaw(new SearchParameters { KvkNummer = "85058769" }, "l7xxd760b2c8441c490c941d6f2f81e6e387");
        var result3 = await kvkClient.Search(new SearchParameters { Handelsnaam = "mannus"}, "l7xxd760b2c8441c490c941d6f2f81e6e387");
        var trdResult = result.Resultaten.FirstOrDefault(x => x.Type == Vestigingstype.Rechtspersoon);
        trdResult.Naam.Should().Be("the Right Direction B.V.");
    }
}