using FluentAssertions;
using HR.KvkConnector.Model.Zoeken;
using theRightDirection.KvKConnector;
using theRightDirection.KvKConnector.Model;

namespace KvKConnector;
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

    [Fact]
    public async Task Zoek_Hoofdvestiging_Adres_For_Gemeente_Leeuwarden()
    {
        var response = await _kvk.Search(new SearchParameters { Handelsnaam = "Gemeente Leeuwarden" });
        var result = response.Resultaten.Where(x => x.Type == Vestigingstype.Hoofdvestiging && x.KvkNummer == "59734817");
        result.Count().Should().Be(1);
        var vestigingsNummer = result.First().Vestigingsnummer;
        var vestiging = await _kvk.GetVestigingsProfiel(vestigingsNummer);
        vestiging.Adressen.Count().Should().Be(2);
        var bezoekAdres = vestiging.Adressen.First(x => x.Type == Adrestype.Bezoekadres);
        bezoekAdres.VolledigAdres.Should().Be("Oldehoofsterkerkhof 2 8911DH Leeuwarden");
    }
}