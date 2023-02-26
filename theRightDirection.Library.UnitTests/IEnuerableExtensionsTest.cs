using AutoBogus;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace theRightDirection.Library.UnitTests
{
    public class IEnuerableExtensionsTest
    {
        [Fact]
        public void ToDictionaryWithSafeGuard1()
        {
            var items = AutoFaker.Generate<ArcGISItem>(5);
            var dictionary = items.ToDictionaryWithSafeGuard(x => x.EntityId, x => x.Owner);
            dictionary.Count.Should().Be(items.Count);
        }

        [Fact]
        public void ToDictionaryWithSafeGuard2()
        {
            var items = AutoFaker.Generate<ArcGISItem>(5);
            var firstItem = items.First();
            items.Add(firstItem);
            var dictionary = items.ToDictionaryWithSafeGuard(x => x.EntityId, x => x.Owner);
            dictionary.Count.Should().Be(items.Count - 1);
        }

        [Fact]
        public void ToDictionaryWithoutSafeguard()
        {
            var items = AutoFaker.Generate<ArcGISItem>(5);
            var firstItem = items.First();
            items.Add(firstItem);
            Action act = () => items.ToDictionary(x => x.EntityId, x => x.Owner);
            act.Should().Throw<ArgumentException>();
        }
    }

    public class ArcGISItem
    {
        public string EntityId { get; set; }
        public string Owner { get; set; }
    }
}