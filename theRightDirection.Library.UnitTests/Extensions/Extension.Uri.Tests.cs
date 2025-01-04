using FluentAssertions;
using System;
using Xunit;

namespace theRightDirection.Library.UnitTests.Extensions;

public class Extension
{
    [Fact]
    public void ArcGISOnline_Uri_Is_Null_False()
    {
        Uri agol = null;
        var result = agol.IsArcGISOnline();
        result.Should().BeFalse();
    }
    [Fact]
    public void ArcGISOnline_Uri_Is_True()
    {
        Uri agol = new Uri("https://portalgenius.maps.arcgis.com");
        var result = agol.IsArcGISOnline();
        result.Should().BeTrue();
    }
    [Fact]
    public void ArcGISEnterprise_Uri_Is_Not_ArcGISOnline()
    {
        Uri enterprise = new Uri("https://www.therightdirectionserver.nl/portal");
        var result = enterprise.IsArcGISOnline();
        result.Should().BeFalse();
    }

    [Fact]
    public void Two_Uris_Are_Equal1()
    {
        var uri1 = new Uri("https://services.arcgis.com/v16XTZeIhHAZEpwh/ArcGIS/rest/services/Veiligheidsregios/FeatureServer/");
        var uri2 = new Uri("https://services.arcgis.com/v16XTZeIhHAZEpwh/arcgis/rest/services/Veiligheidsregios/FeatureServer/");
        uri1.IsEqual(uri2).Should().BeTrue();
    }

    [Fact]
    public void Two_Uris_Are_Equal2()
    {
        var uri1 = new Uri("https://services.arcgis.com/v16XTZeIhHAZEpwh/ArcGIS/rest/services/Veiligheidsregios/FeatureServer/");
        var uri2 = new Uri("https://services.arcgis.com/v16XTZeIhHAZEpwh/arcgis/rest/services/Veiligheidsregios/FeatureServer");
        uri1.IsEqual(uri2).Should().BeTrue();
    }
}