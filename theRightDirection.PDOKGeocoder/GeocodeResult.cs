using System.Text.Json.Serialization;

namespace theRightDirection.PDOKGeocoder;

public record GeocodeResult
{
    [JsonPropertyName("centroide_rd")]
    public string CentroidRD { get; set; }
    [JsonPropertyName("bron")]
    public string Bron { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("nummeraanduiding_id")]
    public string BAGId { get; set; }
    [JsonPropertyName("huisnummer")]
    public int Huisnummer { get; set; }
}