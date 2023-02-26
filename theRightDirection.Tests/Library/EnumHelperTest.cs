using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace theRightDirection.Library.UnitTesting
{
    [TestClass]
    public class EnumHelperTest
    {
        [TestMethod]
        public void Storymap()
        {
            var typeValue = "StoryMap";
            var enumValueIsCorrect = EnumHelper.TryParseTextToEnumValue(typeValue, out PortalItemType result);
            enumValueIsCorrect.Should().BeTrue();
            result.Should().Be(PortalItemType.StoryMap);
        }

        [TestMethod]
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