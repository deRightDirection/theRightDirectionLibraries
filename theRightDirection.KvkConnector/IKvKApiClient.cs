using Refit;
using theRightDirection.KvKConnector.Model;
using theRightDirection.KvKConnector.Model.Zoeken;

namespace theRightDirection.KvKConnector;
public interface IKvKApiClient
{
    /// <summary>
    /// Voor een bedrijf zoeken naar basisinformatie.
    /// Er wordt max. 1000 resultaten getoond.
    /// </summary>
    /// <param name="queryParameters"></param>
    /// <returns>lijst met resultaten</returns>
    [Get("/zoeken")]
    Task<Resultaat> Search(SearchParameters queryParameters, [Header("apikey")] string apikey);

    
    [Get("/zoeken")]
    Task<string> SearchRaw(SearchParameters queryParameters, [Header("apikey")] string apikey);
}