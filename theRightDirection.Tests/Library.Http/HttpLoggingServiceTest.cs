using Shouldly;
using theRightDirection.Http;

namespace theRightDirection.Tests.Library.Http;

public class HttpLoggingServiceTest
{
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
