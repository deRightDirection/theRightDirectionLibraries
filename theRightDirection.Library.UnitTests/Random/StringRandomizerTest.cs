using System.Text.RegularExpressions;
using Bogus;
using FluentAssertions;
using theRightDirection.Random;
using Xunit;

namespace theRightDirection.Library.UnitTests.Random
{
    public class StringRandomizerTest
    {
        [Fact]
        public void GivenRandomize_WhenNoOptions_ThenStringWithLenght10WithNumAndUpper()
        {
            //a
            var randomizer = new StringRandomizer();
            Regex rgx = new Regex(@"^[A-Z0-9]{10}$");

            //a
            var result = randomizer.Next();

            //a
            rgx.IsMatch(result).Should().BeTrue();
            result.Length.Should().Be(10);
        }

        [Fact]
        public void GivenRandomize_WhenDefaultRandomizerOptionsLength6_ThenStringWithLenght6WithNumAndUpper()
        {
            //a
            var randomizer = new StringRandomizer(6, new DefaultRandomizerOptions());
            Regex rgx = new Regex(@"^[A-Z0-9]{6}$");

            //a
            var result = randomizer.Next();

            //a
            rgx.IsMatch(result).Should().BeTrue();
            result.Length.Should().Be(6);
        }

        [Fact]
        public void GivenRandomize_WhenDefaultRandomizerOptionsLength6AndLowerTrue_ThenStringWithLenght6WithNumAndUpperAndLower()
        {
            //a
            var randomizer = new StringRandomizer(6, new DefaultRandomizerOptions(hasLowerAlphabets: true));
            Regex rgx = new Regex(@"^[A-Za-z0-9]{6}$");

            //a
            var result = randomizer.Next();

            //a
            rgx.IsMatch(result).Should().BeTrue();
            result.Length.Should().Be(6);
        }

        [Fact]
        public void GivenRandomize_WhenDefaultStoreAndLength6_ThenStringWithLenght6WithNumAndUpper()
        {
            //a
            var store = new DefaultRandomizerStore();
            var randomizer = new StringRandomizer(6, store: store);
            Regex rgx = new Regex(@"^[A-Za-z0-9]{6}$");

            //a
            var result = randomizer.Next();

            //a
            rgx.IsMatch(result).Should().BeTrue();
            result.Length.Should().Be(6);
            store.TryAdd(result).Should().BeFalse();
        }

        [Fact]
        public void GivenRandomize_WhenDefaultRandomizerOptionsLength6AndLOnlySpecialTrue_ThenStringWithLenght6WithSpecial()
        {
            //a
            var randomizer = new StringRandomizer(6, new DefaultRandomizerOptions(false, false, false, true));
            Regex rgx = new Regex(@"^[\!\@\#\$\%\^\&\*\(\)\-\+]{6}$");

            //a
            var result = randomizer.Next();

            //a
            rgx.IsMatch(result).Should().BeTrue();
            result.Length.Should().Be(6);
        }
    }
}