using FluentAssertions;
using theRightDirection.PDOKGeocoder;

namespace PDOKGeoCoder;
public class GeoCoderTest
{
    [Fact]
    public async Task Get_BAGID_Of_BezoekAdres_Gemeente_Leeuwarden()
    {
        var geocoder = new Geocoder();
        var result = await geocoder.GetBAGId("Oldehoofsterkerkhof 2 8911DH Leeuwarden");
        result.BAGId.Should().Be("0080200010085646");
    }
    [Fact]
    public async Task Get_Centroid_Of_BAGId_BezoekAdres_Gemeente_Leeuwarden()
    {
        var geocoder = new Geocoder();
        var result = await geocoder.GeocodeBagId("0080200010085646");
        result.BAGId.Should().BeNull();
        result.CentroidRD.Should().Be("POINT(181918.468 579603.441)");
    }
}
