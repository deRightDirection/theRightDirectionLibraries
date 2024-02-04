using HR.KvkConnector.Model;
using HR.KvkConnector.Model.Zoeken;
using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model.Zoeken;

public class ResultaatItem
{
    /// <summary>
    /// Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.
    /// </summary>
    [JsonPropertyName("kvkNummer")]
    public string KvkNummer { get; set; }

    /// <summary>
    /// Rechtspersonen Samenwerkingsverbanden Informatie Nummer.
    /// </summary>
    [JsonPropertyName("rsin")]
    public string Rsin { get; set; }

    /// <summary>
    /// Vestigingsnummer: uniek nummer dat bestaat uit 12 cijfers.
    /// </summary>
    [JsonPropertyName("vestigingsnummer")]
    public string Vestigingsnummer { get; set; }

    /// <summary>
    /// De naam waaronder een vestiging of rechtspersoon handelt.
    /// </summary>
    [JsonPropertyName("naam")]
    public string Naam { get; set; }

    //[JsonPropertyName("adres")]
    // TODO
    //public string Adres { get; set; }

    [JsonPropertyName("straatnaam")] public string Straatnaam { get; set; }

    [JsonPropertyName("huisnummer")] public int? Huisnummer { get; set; }

    [JsonPropertyName("huisnummerToevoeging")]
    public string HuisnummerToevoeging { get; set; }

    [JsonPropertyName("postcode")] public string Postcode { get; set; }

    [JsonPropertyName("plaats")] public string Plaats { get; set; }

    /// <summary>
    /// hoofdvestiging/nevenvestiging/rechtspersoon
    /// </summary>
    public Vestigingstype? Type { get; set; }

    [JsonPropertyName("type")]
    protected string TypeString
    {
        get => Type?.GetStringValue();
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Type = null;
            }
            else
            {
                Type = Enum.GetValues(typeof(Vestigingstype))
                    .Cast<Vestigingstype?>()
                    .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    [JsonPropertyName("actief")]
    public string ActiefString { get; set; }

        /// <summary>
    /// Indicatie of inschrijving actief is.
    /// </summary>
//    public JaNeeIndicatie Actief { get; set; }

    /// <summary>
    /// Bevat de vervallen handelsnaam of statutaire naam waar dit zoekresultaat mee gevonden is.
    /// </summary>
    [JsonPropertyName("vervallenNaam")]
    public string VervallenNaam { get; set; }

    [JsonPropertyName("links")]
    public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();

}