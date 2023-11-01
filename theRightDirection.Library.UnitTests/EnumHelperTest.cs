using FluentAssertions;
using System;
using theRightDirection.PortalGenius.Domain.ArcGIS;
using Xunit;

namespace theRightDirection.Library.UnitTests
{
    public class EnumHelperTest
    {
        [Fact]
        public void IntToEnumValue_With_Non_Existing_Value_Throws_Exception()
        {
            var typeValue = 2000;
            Action act = () => EnumHelper.ParseIntToEnumValue<PortalItemType>(typeValue);
            act.Should().Throw<EnumHelperException>().WithMessage("there is no value in the enum type theRightDirection.PortalGenius.Domain.ArcGIS.PortalItemType with a value of 2000");
        }
        [Fact]
        public void IntToEnumValue_With_Existing_Value()
        {
            var typeValue = 1014;
            var enumValue = EnumHelper.ParseIntToEnumValue<PortalItemType>(typeValue);
            enumValue.Should().Be(PortalItemType.GroupLayer);
        }
        [Fact]
        public void Storymap()
        {
            var typeValue = "StoryMap";
            var enumValueIsCorrect = EnumHelper.TryParseTextToEnumValue(typeValue, out TestPortalItemType result);
            enumValueIsCorrect.Should().BeTrue();
            result.Should().Be(TestPortalItemType.StoryMap);
        }

        [Fact]
        public void Storymap_Incorrect()
        {
            var typeValue = "StoryMap2";
            var enumValueIsCorrect = EnumHelper.TryParseTextToEnumValue(typeValue, out TestPortalItemType result);
            enumValueIsCorrect.Should().BeFalse();
            result.Should().Be(TestPortalItemType.ArcGISProAddIn);
        }
    }

    public enum TestPortalItemType
    {
        ArcGISProAddIn,
        StoryMap
    }
}