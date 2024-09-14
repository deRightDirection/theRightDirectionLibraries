using Refit;

namespace theRightDirection.PDOKGeocoder;
internal interface IPDOKGeocoder
{
    HttpClient Client { get; }

    [Get("/free")]
    Task<Root> GeocodeBAGId(string q, string fl = "centroide_rd", string fq = "bron:BAG", string df = "nummeraanduiding_id", string wt = "json");
    [Get("/free")]
    Task<Root> GetBAGId(string q, string fl = "nummeraanduiding_id,bron,type,huisnummer,huisletter", string fq = "bron:BAG", string wt = "json");
}