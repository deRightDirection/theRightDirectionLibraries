using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theRightDirection.Json;
using Xunit;
using FluentAssertions;
namespace theRightDirection.Library.UnitTests.Json
{
    public class StringEnumConverterTest
    {
        [Fact]
        public void GetBFromString()
        {
            var json = "{Value:\"B\"}";
            var jsonObject = JsonConvert.DeserializeObject<TestObject>(json);
            jsonObject.Value.Should().Be(TestValues.B);
        }
        [Fact]
        public void GetbFromString()
        {
            var json = "{Value:\"b\"}";
            var jsonObject = JsonConvert.DeserializeObject<TestObject>(json);
            jsonObject.Value.Should().Be(TestValues.B);
        }
        [Fact]
        public void GetDefault()
        {
            var json = "{Value:\"D\"}";
            var jsonObject = JsonConvert.DeserializeObject<TestObject>(json);
            jsonObject.Value.Should().Be(TestValues.A);
        }
    }

    internal class TestObject
    {
        [JsonConverter(typeof(StringEnumConverter<TestValues>))]
        public TestValues Value { get; set; }
    }

    internal enum TestValues
    {
        A,
        B,
        C
    }
}
