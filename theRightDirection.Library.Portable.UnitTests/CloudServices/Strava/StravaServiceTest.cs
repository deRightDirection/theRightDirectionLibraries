using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using theRightDirection.CloudServices.Strava;

namespace theRightDirection.Library.Portable.UnitTests.CloudServices.Strava
{
    [TestClass]
    public class StravaServiceTest
    {
        [TestMethod]
        public async Task GetActivity_Private()
        {
            var stravaService = new StravaService("b4acc9e1df52f8e440638b1562e978bf8e81ffdf");
            var activity = await stravaService.GetActivityDataAsync(1944649376);
            activity.Title.Should().Be("Ochtendzwemsessie");
        }

        [TestMethod]
        public async Task GetActivity_Public()
        {
            var stravaService = new StravaService("b4acc9e1df52f8e440638b1562e978bf8e81ffdf");
            var activity = await stravaService.GetActivityDataAsync(1940254665);
            activity.Title.Should().Be("zwemles VI");
        }
    }
}
