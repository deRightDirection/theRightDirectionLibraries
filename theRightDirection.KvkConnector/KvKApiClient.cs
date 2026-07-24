using Refit;
using Serilog;
using System.Diagnostics;
using System.Net;
using System.Security;
using theRightDirection.KvKConnector.Model;

namespace theRightDirection.KvKConnector;

public class KvKApiClient
{
    private readonly SecureString _apiKey;
    private readonly IKvKApiClient _kvkApi;
    public KvKApiClient(SecureString apikey)
    {
        _apiKey = apikey;
        _kvkApi = KvKApiClientFactory.CreateKvKClient();
    }

    public async Task<IApiResponse<Basisprofiel>> GetBasisProfiel(string kvkNummer)
    {
        if (_apiKey == null)
        {
            Log.Logger.Here().Error("kan de API-key voor de KvK API niet ophalen");
            return new ApiResponse<Basisprofiel>(new HttpResponseMessage(HttpStatusCode.NotFound), new Basisprofiel(), new RefitSettings());
        }
        try
        {
            var result = await _kvkApi.GetBasisProfiel(_apiKey.ToUnsecureString(), kvkNummer).ConfigureAwait(false);
            if (result.IsSuccessful)
            {
                return result;
            }
            Log.Logger.Here().Error(result.GetErrorMessage());
        }
        catch (Exception e)
        {
            Log.Logger.Here().Error(e.ToStringDemystified());
        }
        return new ApiResponse<Basisprofiel>(new HttpResponseMessage(HttpStatusCode.NotFound), new Basisprofiel(), new RefitSettings());
    }


    //public async Task<Vestiging> GetVestigingsProfiel(string vestigingsNummer)
    //{
    //    try
    //    {
    //        var result = await _kvkApi.GetVestigingsProfiel(vestigingsNummer, _apiKey).ConfigureAwait(false);
    //        var result2 = await _kvkApi.GetVestigingsProfielRaw(vestigingsNummer, _apiKey);

    //        if (result.IsSuccessStatusCode)
    //        {
    //            return result.Content;
    //        }
    //        Log.Logger.Here().Error(result.Error.Content);
    //    }
    //    catch (Exception e)
    //    {

    //        // TODO loggen
    //    }
    //    return new Vestiging();

    //}

    //public async Task<Resultaat> Search(SearchParameters queryParameters)
    //{
    //    try
    //    {
    //        var result = await _kvkApi.Search(queryParameters, _apiKey).ConfigureAwait(false);
    //        if (result.IsSuccessStatusCode)
    //        {
    //            return result.Content;
    //        }
    //        Log.Logger.Here().Error(result.Error.Content);
    //    }
    //    catch (Exception e)
    //    {

    //        // TODO loggen
    //    }
    //    return new Resultaat();
    //}
}