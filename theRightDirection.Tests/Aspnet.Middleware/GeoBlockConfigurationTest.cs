using Shouldly;
using System.Net;
using theRightDirection.AspNet.Middleware.GeoBlock;

namespace theRightDirection.Tests.Aspnet.Middleware;

public class GeoBlockConfigurationTest
{
    [Fact]
    public void Non_Existing_Key()
    {
        var middleWare = new GeoBlockMiddleWareConfiguration("C:\\Github\\KLICGenius\\MaxMind\\GeoLite2-Country.mmdb", new List<string>());
        var result = middleWare.IPIsFromAllowedCountry(IPAddress.Parse("1.2.3.4"));
        result.ShouldBeFalse();
    }
}
