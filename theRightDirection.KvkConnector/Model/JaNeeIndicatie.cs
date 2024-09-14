using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;

[JsonConverter(typeof(JsonStringEnumConverter<JaNeeIndicatie>))]
public enum JaNeeIndicatie
{
    Onbekend = -1,
    Ja = 1,
    Nee = 0
}