using FluentAssertions;
using Shouldly;
using System.Reflection;
using System.Text.Json;
using theRightDirection;
using theRightDirection.KvKConnector;
using theRightDirection.KvKConnector.Model;
using theRightDirection.Tests;

namespace KvKConnector;

public class KvkApiClientTest : UnitTestHelper
{
    private readonly KvKApiClient _kvk;
    private readonly ResourceReader _resources;

    public KvkApiClientTest()
    {
        _resources = new ResourceReader(Assembly.GetExecutingAssembly());
        _kvk = GetKvKClient();
    }

    // TODO 04-04-2026 9155985 uitzoeken waarom deze niets terug geeft

    [Theory]
    [InlineData("kvk1", "Vlinderweg")]
    [InlineData("kvk2", "Van Iddekingeweg")]
    public void GetAddress_From_BasisProfiel(string file, string adresPart)
    {
        var data = _resources.ReadDataFromResource($"{file}.json", "TestResources");
        var profiel = JsonSerializer.Deserialize<Basisprofiel>(data);
        var adres = profiel.GetBezoekAdres();
        adres.ShouldNotBeNull();
        adres.Straatnaam.ShouldContain(adresPart);
    }

    [Theory]
    [InlineData("37026706", "Boven Vredenburgpassage")]
    [InlineData("27276405", "Vlinderweg")]
    [InlineData("27124701", "Wilhelminakade")]
    [InlineData("11111111", "Boven Vredenburgpassage")]
    [InlineData("8155796", "mannus")]
    public async Task Find_Address(string kvk, string street)
    {
        var response = await _kvk.GetBasisProfiel(kvk);
        if (!response.IsSuccessful)
        {
            Assert.Fail("niet gevonden");
        }
        var bezoekAdres = response.Content.GetBezoekAdres();
        if (bezoekAdres == null)
        {
            Assert.Fail("geen bezoek adres gevonden");
        }
        var straat = $"{bezoekAdres.Straatnaam} {bezoekAdres.Huisnummer}{bezoekAdres.Huisletter}";
        straat.Should().Contain(street);
    }

    [Fact]
    public async Task Zoek_By_KvkNummer()
    {
        //var result = await _kvk.Search(new SearchParameters { KvkNummer = "85058769" });
        //result.Resultaten.First().Naam.Should().Be("the Right Direction B.V.");
    }

    [Fact]
    public async Task Zoek_By_Naam()
    {
        //var result = await _kvk.Search(new SearchParameters { Handelsnaam = "the right direction" });
        //result.Resultaten.First().Naam.Should().Be("the Right Direction B.V.");
    }

    [Fact]
    public async Task Zoek_Hoofdvestiging_Adres_For_Gemeente_Leeuwarden()
    {
        //var response = await _kvk.Search(new SearchParameters { Handelsnaam = "Gemeente Leeuwarden" });
        //var result = response.Resultaten.Where(x => x.Type == Vestigingstype.Hoofdvestiging && x.KvkNummer == "59734817");
        //result.Count().Should().Be(1);
        //var vestigingsNummer = result.First().Vestigingsnummer;
        //var vestiging = await _kvk.GetVestigingsProfiel(vestigingsNummer);
        //vestiging.Adressen.Count().Should().Be(2);
        //var bezoekAdres = vestiging.Adressen.First(x => x.Type == Adrestype.Bezoekadres);
        //bezoekAdres.VolledigAdres.Should().Be("Oldehoofsterkerkhof 2 8911DH Leeuwarden");
    }
}