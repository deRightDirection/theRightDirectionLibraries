using Refit;
using theRightDirection.KvKConnector.Model;

namespace theRightDirection.KvKConnector;
public interface IKvKApiClient
{
    /// <summary>
    /// Voor een bedrijf zoeken naar basisinformatie.
    /// Er worden max. 1000 resultaten getoond.
    /// </summary>
    /// <param name="queryParameters"></param>
    /// <returns>lijst met resultaten</returns>
    //[Get("/api/v2/zoeken")]
    //Task<ApiResponse<Resultaat>> Search(SearchParameters queryParameters, [Header("apikey")] string apikey);


    //[Get("/api/v2/zoeken")]
    //Task<string> SearchRaw(SearchParameters queryParameters, [Header("apikey")] string apikey);

    //[Get("/api/v1/vestigingsprofielen/{vestigingsNummer}")]
    //Task<ApiResponse<Vestiging>> GetVestigingsProfiel(string vestigingsNummer, [Header("apikey")] string apikey);

    //[Get("/api/v1/vestigingsprofielen/{vestigingsNummer}")]
    //Task<string> GetVestigingsProfielRaw(string vestigingsNummer, [Header("apikey")] string apikey);

    [Get("/api/v1/basisprofielen/{kvkNummer}")]
    Task<ApiResponse<Basisprofiel>> GetBasisProfiel([Header("apikey")] string apikey, string kvkNummer);
}