using FluentAssertions;
using System.Text.Json;
using theRightDirection.KvKConnector.Model;

namespace KvKConnector.Model;
public class VestigingTest
{
    [Fact]
    public void Serialize_Json_To_Vestiging()
    {
        var json = "{\"vestigingsnummer\":\"000000013447\",\"kvkNummer\":\"59734817\",\"rsin\":\"001903664\",\"indNonMailing\":\"Ja\",\"formeleRegistratiedatum\":\"20140116\",\"materieleRegistratie\":{\"datumAanvang\":\"19931006\"},\"eersteHandelsnaam\":\"Stadskantoor Gemeente Leeuwarden\",\"indHoofdvestiging\":\"Ja\",\"indCommercieleVestiging\":\"Nee\",\"adressen\":[{\"type\":\"correspondentieadres\",\"indAfgeschermd\":\"Nee\",\"volledigAdres\":\"Postbus 21000 8900JA Leeuwarden\",\"postcode\":\"8900JA\",\"postbusnummer\":21000,\"plaats\":\"Leeuwarden\",\"land\":\"Nederland\"},{\"type\":\"bezoekadres\",\"indAfgeschermd\":\"Nee\",\"volledigAdres\":\"Oldehoofsterkerkhof 2 8911DH Leeuwarden\",\"straatnaam\":\"Oldehoofsterkerkhof\",\"huisnummer\":2,\"postcode\":\"8911DH\",\"plaats\":\"Leeuwarden\",\"land\":\"Nederland\"}],\"websites\":[\"www.leeuwarden.nl\"],\"sbiActiviteiten\":[{\"sbiCode\":\"8411\",\"sbiOmschrijving\":\"Algemeen overheidsbestuur\",\"indHoofdactiviteit\":\"Ja\"}],\"links\":[{\"rel\":\"self\",\"href\":\"https://api.kvk.nl/api/v1/vestigingsprofielen/000000013447\"},{\"rel\":\"basisprofiel\",\"href\":\"https://api.kvk.nl/api/v1/basisprofielen/59734817\"}]}";
        var vestiging = JsonSerializer.Deserialize<Vestiging>(json);
        vestiging.Adressen.Count().Should().Be(2);
    }
}
