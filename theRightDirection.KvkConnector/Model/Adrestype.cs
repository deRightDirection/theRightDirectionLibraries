using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;

[JsonConverter(typeof(JsonStringEnumConverter<Adrestype>))]
public enum Adrestype
{
    Onbekend,
    Correspondentieadres,
    Bezoekadres
}