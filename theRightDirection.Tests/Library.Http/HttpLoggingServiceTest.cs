using FluentAssertions;
using Refit;
using Shouldly;
using theRightDirection.Http;
using theRightDirection.PDOKGeocoder;

namespace theRightDirection.Tests.Library.Http;

public class HttpLoggingServiceTest
{
    [Fact]
    public async Task GeocodeBagId()
    {
        var geocoder = RestService.For<IPDOKGeocoder>(new HttpClient(new HttpLoggingService())
        {
            BaseAddress = new Uri("https://api.pdok.nl/bzk/locatieserver/search/v3_1")
        });
        var result = await geocoder.GetBAGId("Oldehoofsterkerkhof 2 8911DH Leeuwarden");
        result?.response?.docs?.FirstOrDefault().BAGId.Should().Be("0080200010085646");
    }

    [Theory]
    [InlineData(
        "/portal/sharing/rest/content/users/KLICGenius/items/33a9f912911f4619a7651382b325c50b/move?token=OY6lAQ-ooh&f=json",
        "/portal/sharing/rest/content/users/KLICGenius/items/33a9f912911f4619a7651382b325c50b/move?token=OY6..ooh&f=json")]
    public void StripPathAndQuery(string original, string expected)
    {
        var loggingService = new HttpLoggingService();
        var result = loggingService.StripPathAndQuery(original);
        result.ShouldBe(expected);
    }
}
