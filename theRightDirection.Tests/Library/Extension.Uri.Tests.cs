using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace theRightDirection.Tests.Library
{
    [TestClass]
    public class Extension
    {
        [TestMethod]
        public void Two_Uris_Are_Equal1()
        {
            var uri1 = new Uri("https://services.arcgis.com/v16XTZeIhHAZEpwh/ArcGIS/rest/services/Veiligheidsregios/FeatureServer/");
            var uri2 = new Uri("https://services.arcgis.com/v16XTZeIhHAZEpwh/arcgis/rest/services/Veiligheidsregios/FeatureServer/");
            uri1.IsEqual(uri2).Should().BeTrue();
        }

        [TestMethod]
        public void Two_Uris_Are_Equal2()
        {
            var uri1 = new Uri("https://services.arcgis.com/v16XTZeIhHAZEpwh/ArcGIS/rest/services/Veiligheidsregios/FeatureServer/");
            var uri2 = new Uri("https://services.arcgis.com/v16XTZeIhHAZEpwh/arcgis/rest/services/Veiligheidsregios/FeatureServer");
            uri1.IsEqual(uri2).Should().BeTrue();
        }
    }
}