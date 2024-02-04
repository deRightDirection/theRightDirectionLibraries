using HR.KvkConnector;
using HR.KvkConnector.Model;
using HR.KvkConnector.Model.Zoeken;
using System.Reflection;
using System.Text.Json;
using theRightDirection.KvKConnector.Model.Zoeken;

namespace theRightDirection.Tests.KvKConnector.Fixture;

public class FakeApiClient
{
    public Task<Basisprofiel> GetBasisprofielAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
        => GetFromJsonAsync<Basisprofiel>("basisprofiel.json");

    public Task<Eigenaar> GetEigenaarAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
        => GetFromJsonAsync<Eigenaar>("eigenaar.json");

    public Task<Vestiging> GetHoofdvestigingAsync(string kvkNummer, bool geoData = false, CancellationToken cancellationToken = default)
        => GetFromJsonAsync<Vestiging>("hoofdvestiging.json");

    public Task<VestigingList> GetVestigingenAsync(string kvkNummer, CancellationToken cancellationToken = default)
        => GetFromJsonAsync<VestigingList>("vestigingen.json");

    public Task<Vestiging> GetVestigingsprofielAsync(string vestigingsnummer, bool geoData = false, CancellationToken cancellationToken = default)
        => GetFromJsonAsync<Vestiging>("vestigingsprofiel.json");

    private static async Task<TResult> GetFromJsonAsync<TResult>(string jsonFileName)
    {
        var resourceName = $"theRightDirection.Tests.KvKConnector.Fixture.Json.{jsonFileName}";

        using var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        using var fileReader = new StreamReader(fileStream);

        var jsonString = await fileReader.ReadToEndAsync();

        return JsonSerializer.Deserialize<TResult>(jsonString);
    }
}