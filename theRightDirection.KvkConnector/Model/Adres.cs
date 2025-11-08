using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;
public class Adres
{
    /// <summary>
    /// Correspondentieadres en/of bezoekadres.
    /// </summary>
    [JsonPropertyName("type")]
    public Adrestype? Type { get; set; }

    /// <summary>
    /// Indicatie of het adres is afgeschermd.
    /// </summary>
    [JsonPropertyName("indAfgeschermd")]
    public JaNeeIndicatie? IndAfgeschermd { get; set; }

    [JsonPropertyName("volledigAdres")]
    public string VolledigAdres { get; set; }

    [JsonPropertyName("straatnaam")]
    public string Straatnaam { get; set; }

    [JsonPropertyName("huisnummer")]
    public int? Huisnummer { get; set; }

    [JsonPropertyName("postcode")]
    public string Postcode { get; set; }
    [JsonPropertyName("postbusnummer")]
    public int? Postbusnummer { get; set; }

    [JsonPropertyName("plaats")]
    public string Plaats { get; set; }

    [JsonPropertyName("land")]
    public string Land { get; set; }
    //    public string HuisnummerToevoeging { get; set; }

    [JsonPropertyName("huisletter")]
    public string Huisletter { get; set; }

    //    public string AanduidingBijHuisnummer { get; set; }

    //    public string ToevoegingAdres { get; set; }
    //    public string StraatHuisnummer { get; set; }

    //    public string PostcodeWoonplaats { get; set; }

    //    public string Regio { get; set; }

    /// <summary>
    /// Basisregistratie Adressen en Gebouwen gegevens uit het kadaster.
    /// </summary>
    //    public GeoData GeoData { get; set; }
}
