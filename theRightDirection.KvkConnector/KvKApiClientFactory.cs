using Refit;
using theRightDirection.Http;

namespace theRightDirection.KvKConnector;

internal class KvKApiClientFactory
{
    internal static IKvKApiClient CreateKvKClient()
    {
        return RestService.For<IKvKApiClient>(new HttpClient(new HttpLoggingService())
        {
            BaseAddress = new Uri("https://api.kvk.nl")
        });
    }
}