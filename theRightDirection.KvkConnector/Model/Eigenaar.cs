using HR.KvkConnector.Model;
using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;

public class Eigenaar
{
    /// <summary>
    /// Rechtspersonen Samenwerkingsverbanden Informatie Nummer.
    /// </summary>
    public string Rsin { get; set; }

    public string Rechtsvorm { get; set; }

    public string UitgebreideRechtsvorm { get; set; }

    [JsonPropertyName("adressen")]
    public IEnumerable<Adres> Adressen { get; set; } = Enumerable.Empty<Adres>();

    public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();

}