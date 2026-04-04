using FluentAssertions;
using Meziantou.Framework.Win32;
using Shouldly;
using System.Reflection;
using System.Text.Json;
using theRightDirection;
using theRightDirection.KvKConnector;
using theRightDirection.KvKConnector.Model;

namespace KvKConnector;

public class KvkApiClientTest
{
    private readonly KvKApiClient _kvk;
    private readonly ResourceReader _resources;

    public KvkApiClientTest()
    {
        var kvkApiKey = CredentialManager.ReadCredential("kvkApiKey").Password.ToSecureString();
        _kvk = new KvKApiClient(kvkApiKey);
        _resources = new ResourceReader(Assembly.GetExecutingAssembly());
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

    [Fact]
    public async Task Find_Delft_BreedBand()
    {
        var response = await _kvk.GetBasisProfiel("27276405");
        if (!response.IsSuccessful)
        {
            Assert.Fail();
        }
        var hoofdVestiging = response.Content?.Embedded?.Hoofdvestiging;
        var bezoekAdres = hoofdVestiging?.Adressen
            ?.FirstOrDefault(x => x.Type == Adrestype.Bezoekadres);
        if (bezoekAdres == null)
        {
            Assert.Fail("geen bezoek adres gevonden");
        }
        var straat = $"{bezoekAdres.Straatnaam} {bezoekAdres.Huisnummer}{bezoekAdres.Huisletter}";
        straat.Should().Contain("Wilhelminakade");
    }

    [Fact]
    public async Task Find_KPN_BezoekAdres()
    {
        var response = await _kvk.GetBasisProfiel("27124701");
        if (!response.IsSuccessful)
        {
            Assert.Fail();
        }
        var hoofdVestiging = response.Content?.Embedded?.Hoofdvestiging;
        var bezoekAdres = hoofdVestiging?.Adressen
            ?.FirstOrDefault(x => x.Type == Adrestype.Bezoekadres);
        if (bezoekAdres == null)
        {
            Assert.Fail();
        }
        var straat = $"{bezoekAdres.Straatnaam} {bezoekAdres.Huisnummer}{bezoekAdres.Huisletter}";
        straat.Should().Contain("Wilhelminakade");
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