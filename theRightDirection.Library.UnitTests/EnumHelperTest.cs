using FluentAssertions;
using Xunit;

namespace theRightDirection.Library.UnitTests
{
    public class EnumHelperTest
    {
        [Fact]
        public void Storymap()
        {
            var typeValue = "StoryMap";
            var enumValueIsCorrect = EnumHelper.TryParseTextToEnumValue(typeValue, out PortalItemType result);
            enumValueIsCorrect.Should().BeTrue();
            result.Should().Be(PortalItemType.StoryMap);
        }

        [Fact]
        public void Storymap_Incorrect()
        {
            var typeValue = "StoryMap2";
            var enumValueIsCorrect = EnumHelper.TryParseTextToEnumValue(typeValue, out PortalItemType result);
            enumValueIsCorrect.Should().BeFalse();
            result.Should().Be(PortalItemType.ArcGISProAddIn);
        }
    }

    public enum PortalItemType
    {
        ArcGISProAddIn,
        StoryMap
    }
}