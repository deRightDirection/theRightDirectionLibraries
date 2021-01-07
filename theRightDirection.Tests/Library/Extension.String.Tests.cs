using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace theRightDirection.Tests
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void IsValidJson_Is_True()
        {
            string json = "{\"component\":1,\"releaseDate\":\"2020-12-21T00:00:00+01:00\",\"versionNumber\":{\"major\":2021,\"minor\":2,\"build\":0,\"revision\":-1,\"majorRevision\":-1,\"minorRevision\":-1},\"preRelease\":false}";
            var result = json.IsValidJson<JsonClass>();
            result.Should().BeTrue();
        }
    }

    class JsonClass
    {
        public JsonClassComponent Component { get; set; }
        public DateTime ReleaseDate { get; set; }
        [JsonIgnore]
        public Version VersionNumber { get; set; }
        public bool PreRelease { get; set; }
    }

    public enum JsonClassComponent
    {
        /// <summary>
        /// Foutieve waarde
        /// </summary>
        Unknown,

        /// <summary>
        /// WPF-desktop applicatie
        /// </summary>
        Desktop
    }
}
