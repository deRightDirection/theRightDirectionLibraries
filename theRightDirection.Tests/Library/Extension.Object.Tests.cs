using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace theRightDirection.Tests.Library
{
    [TestClass]
    public class ObjectExtensionsTest
    {
        public class TypeA
        {
            public string Id { get; set; }
            public List<string> Names { get; set; }
        }
        public class TypeB
        {
            public string Id { get; set; }
            public List<string> Names { get; set; }
        }

        [TestMethod]
        public void CopyAllPropertiesIncludingLists()
        {
            var a = new TypeA()
            {
                Id = "1",
                Names = new List<string>() {"abc", "def"}
            };
            var b = new TypeB();
            a.CopyProperties(b);
            b.Id.Should().Be("1");
            b.Names.Count.Should().Be(2);
        }
        [TestMethod]
        public void CopyAllPropertiesExcludingLists()
        {
            var a = new TypeA()
            {
                Id = "1",
                Names = new List<string>() { "abc", "def" }
            };
            var b = new TypeB();
            a.CopyProperties(b, skipListOrArrayProperties:true);
            b.Id.Should().Be("1");
            b.Names.Should().BeNull();
        }
    }
}
