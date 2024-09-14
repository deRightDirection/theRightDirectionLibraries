using Refit;
using theRightDirection.Http;

namespace theRightDirection.PDOKGeocoder;
public class Geocoder
{
    private IPDOKGeocoder _geocoder;

    public Geocoder()
    {
        _geocoder = RestService.For<IPDOKGeocoder>(new HttpClient(new HttpLoggingService())
        {
            BaseAddress = new Uri("https://api.pdok.nl/bzk/locatieserver/search/v3_1")
        });
    }

    public async Task<GeocodeResult> GeocodeBagId(string bagid)
    {
        var result = await _geocoder.GeocodeBAGId(bagid).ConfigureAwait(false);
        return result?.response?.docs?.FirstOrDefault();
    }
    public async Task<GeocodeResult> GetBAGId(string adres)
    {
        var result = await _geocoder.GetBAGId(adres).ConfigureAwait(false);
        var adresses = result?.response?.docs;
        if (adresses == null)
        {
            return null;
        }
        var candidates = adresses.Where(x => x.Type.Equals("adres"));
        return candidates.FirstOrDefault(x => adres.Contains(x.Huisnummer.ToString()));
    }
}