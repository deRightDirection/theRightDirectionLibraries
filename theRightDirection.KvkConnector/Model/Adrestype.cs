using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;

[JsonConverter(typeof(JsonStringEnumConverter<Adrestype>))]
public enum Adrestype
{
    [EnumMember(Value = "correspondentieadres")]
    Correspondentieadres = 1,

    [EnumMember(Value = "bezoekadres")]
    Bezoekadres
}