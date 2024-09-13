using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;
public class Adres
{
    [JsonPropertyName("binnenlandsAdres")]
    public BinnenlandsAdres BinnenlandsAdres { get; set; }
}
