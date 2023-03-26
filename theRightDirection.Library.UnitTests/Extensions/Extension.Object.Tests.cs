using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace theRightDirection.Library.UnitTests.Extensions
{
    public class ObjectExtensionsTest
    {
        [Fact]
        public void DeepClone_With_No_Cloning_Copy_Has_Same_Value()
        {
            var original = new Company()
            {
                GBRank = 123,
                desc = new CompanyDescription("Mannus", "Mannus")
            };
            var sut = original;
            sut.GBRank = 456;
            sut.desc.CompanyName = "Mannus BV";
            original.GBRank.Should().Be(456);
            original.desc.CompanyName.Should().Be("Mannus BV");
            sut.GBRank.Should().Be(456);
            sut.desc.CompanyName.Should().Be("Mannus BV");
        }
        [Fact]
        public void DeepClone_With_Cloning_Copy_Has_Not_Same_Value()
        {
            var original = new Company()
            {
                GBRank = 123,
                desc = new CompanyDescription("Mannus", "Mannus")
            };
            var sut = original.DeepClone();
            sut.GBRank = 456;
            sut.desc.CompanyName = "Mannus BV";
            original.GBRank.Should().Be(123);
            original.desc.CompanyName.Should().Be("Mannus");
            sut.GBRank.Should().Be(456);
            sut.desc.CompanyName.Should().Be("Mannus BV");
        }
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

        [Fact]
        public void CopyAllPropertiesIncludingBaseTypeProperties()
        {
            var a = new TypeA()
            {
                Id = "1",
                Count = 10,
                Names = new List<string>() { "abc", "def" }
            };
            var c = new TypeC();
            a.CopyProperties(c, true, true);
            c.Id.Should().Be("1");
            c.Count.Should().Be(10);
        }

        [Fact]
        public void CopyAllPropertiesIncludingLists()
        {
            var a = new TypeA()
            {
                Id = "1",
                Names = new List<string>() { "abc", "def" }
            };
            var b = new TypeB();
            a.CopyProperties(b);
            b.Id.Should().Be("1");
            b.Names.Count.Should().Be(2);
        }

        [Fact]
        public void CopyAllPropertiesExcludingLists()
        {
            var a = new TypeA()
            {
                Id = "1",
                Names = new List<string>() { "abc", "def" }
            };
            var b = new TypeB();
            a.CopyProperties(b, skipListOrArrayProperties: true);
            b.Id.Should().Be("1");
            b.Names.Should().BeNull();
        }
    }

    [Serializable]
    class Company
    {
        public int GBRank { get; set; }
        public CompanyDescription desc { get; set; }
    }
    [Serializable]
    class CompanyDescription
    {

        public string CompanyName;
        public string Owner;
        public CompanyDescription(string c, string o)
        {
            CompanyName = c;
            Owner = o;
        }
    }
}