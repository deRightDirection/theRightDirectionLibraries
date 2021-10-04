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
            public int Count { get; set; }
            public List<string> Names { get; set; }
        }
        public class TypeB
        {
            public string Id { get; set; }
            public List<string> Names { get; set; }
        }

        public class TypeC : BaseTypeOfC
        {
            public string Id { get; set; }
        }

        public class BaseTypeOfC
        {
            public int Count { get; set; }
        }
        [TestMethod]
        public void CopyAllPropertiesIncludingBaseTypeProperties()
        {
            var a = new TypeA()
            {
                Id = "1",
                Count = 10,
                Names = new List<string>() { "abc", "def" }
            };
            var c = new TypeC();
            a.CopyProperties(c,true,true);
            c.Id.Should().Be("1");
            c.Count.Should().Be(10);
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
