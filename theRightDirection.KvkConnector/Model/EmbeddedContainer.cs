using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;

public class EmbeddedContainer
{
    [JsonPropertyName("hoofdvestiging")]
    public Vestiging Hoofdvestiging { get; set; }

    //public Eigenaar Eigenaar { get; set; }
}