using Refit;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace theRightDirection.KvKConnector;
internal class KvKApiClientFactory
{
    internal static IKvKApiClient CreateKvKClient()
    {
        var resourceReader = new ResourceReader(Assembly.GetExecutingAssembly());
        var certFolder = "Certificates";
        var resources = resourceReader.GetResources(certFolder);
        var certificates = new X509Certificate2Collection();
        resources.ForEach(x =>
        {
            if (x.EndsWith(".crt"))
            {
                var certificate = resourceReader.ReadDataFromResourceAsCertificate(x, certFolder);
                certificates.Add(certificate);
                // TODO loggen van verloopdatum certificaten
            }
        });
        var handler = new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls13
        };
        handler.ServerCertificateCustomValidationCallback =
            (requestMessage, certificate, chain, sslErrors) => {
                chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
                chain.ChainPolicy.CustomTrustStore.AddRange(certificates);
                return chain.Build(certificate);
            };
        return RestService.For<IKvKApiClient>(new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.kvk.nl/api/v2")
        });
    }
}