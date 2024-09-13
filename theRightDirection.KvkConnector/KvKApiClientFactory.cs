using Refit;
using System.Security.Authentication;

namespace theRightDirection.KvKConnector;
internal class KvKApiClientFactory
{
    internal static IKvKApiClient CreateKvKClient()
    {
        var handler = new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls13
        };

        return RestService.For<IKvKApiClient>(new HttpClient
        {
            BaseAddress = new Uri("https://api.kvk.nl/api/v2")
        });
    }
}