using FluentAssertions;
using System;
using Xunit;

namespace theRightDirection.Library.UnitTests.Extensions
{
    public class Extension
    {
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
}