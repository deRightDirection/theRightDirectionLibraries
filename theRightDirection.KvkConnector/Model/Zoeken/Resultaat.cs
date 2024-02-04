using HR.KvkConnector.Model;
using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model.Zoeken;

public class Resultaat
{
    /// <summary>
    /// Geeft aan op welke pagina je bent. Start vanaf pagina 1.
    /// </summary>
    [JsonPropertyName("pagina")]
    public int? Pagina { get; set; }

    /// <summary>
    /// Geeft het aantal zoek resultaten per pagina weer.
    /// </summary>
    [JsonPropertyName("resultatenPerPagina")]
    public int? Aantal { get; set; }

    /// <summary>
    /// Totaal aantal zoekresultaten gevonden. De API Zoeken toont max. 1000 resultaten.
    /// </summary>
    [JsonPropertyName("totaal")]
    public int? Totaal { get; set; }

    /// <summary>
    /// Link naar de vorige pagina indien beschikbaar.
    /// </summary>
    [JsonPropertyName("vorige")]
    public string Vorige { get; set; }

    /// <summary>
    /// Link naar de volgende pagina indien beschikbaar.
    /// </summary>
    [JsonPropertyName("volgende")]
    public string Volgende { get; set; }

    public IEnumerable<ResultaatItem> Resultaten { get; set; } = Enumerable.Empty<ResultaatItem>();

    [JsonPropertyName("links")]
    public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();
}