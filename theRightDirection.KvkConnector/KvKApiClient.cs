using Serilog;
using theRightDirection.KvKConnector.Model;
using theRightDirection.KvKConnector.Model.Zoeken;

namespace theRightDirection.KvKConnector;

public class KvKApiClient
{
    private readonly string _apiKey;
    private readonly IKvKApiClient _kvkApi;
    public KvKApiClient(string apikey)
    {
        _apiKey = apikey;
        _kvkApi = KvKApiClientFactory.CreateKvKClient();
    }

    public async Task<Vestiging> GetVestigingsProfiel(string vestigingsNummer)
    {
        try
        {
            var result = await _kvkApi.GetVestigingsProfiel(vestigingsNummer, _apiKey).ConfigureAwait(false);
            var result2 = await _kvkApi.GetVestigingsProfielRaw(vestigingsNummer, _apiKey);

            if (result.IsSuccessStatusCode)
            {
                return result.Content;
            }
            Log.Logger.Here().Error(result.Error.Content);
        }
        catch (Exception e)
        {

            // TODO loggen
        }
        return new Vestiging();

    }

    public async Task<Resultaat> Search(SearchParameters queryParameters)
    {
        try
        {
            var result = await _kvkApi.Search(queryParameters, _apiKey).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                return result.Content;
            }
            Log.Logger.Here().Error(result.Error.Content);
        }
        catch (Exception e)
        {

            // TODO loggen
        }
        return new Resultaat();
    }
}