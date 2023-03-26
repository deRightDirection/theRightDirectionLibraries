using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace theRightDirection.Library.UnitTests
{
    public class ResourceReaderTest
    {
        [Fact]
        public void Logo_Is_Found_As_Resource()
        {
            var resourceReader = new ResourceReader();
            var image = resourceReader.ReadDataFromResourceAsFile("tRD-logo-1regel.png");
            image.Length.Should().BeGreaterOrEqualTo(10);
        }
        [Fact]
        public void Text_Is_Read_From_Resource()
        {
            var resourceReader = new ResourceReader();
            var text = resourceReader.ReadDataFromResource("test.txt", "TestResources");
            text.Should().Be("hallo!");
        }
        [Fact]
        public void Text_In_Bytes_From_Resource()
        {
            var resourceReader = new ResourceReader();
            var text = resourceReader.ReadDataFromResourceAsFile("test.txt", "TestResources");
            text.Length.Should().BeGreaterOrEqualTo(9);
        }
    }
}
