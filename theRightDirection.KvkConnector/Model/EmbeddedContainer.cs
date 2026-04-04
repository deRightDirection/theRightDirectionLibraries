using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;

public class EmbeddedContainer
{
    [JsonPropertyName("hoofdvestiging")]
    public Vestiging Hoofdvestiging { get; set; }

    [JsonPropertyName("eigenaar")]
    public Eigenaar Eigenaar { get; set; }
}