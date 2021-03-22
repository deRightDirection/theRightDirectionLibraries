using System;
using System.Collections.Generic;
using System.Text;
using Esri.ArcGISRuntime.Portal;
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
            enumValueIsCorrect.Should().BeFalse();
            result.Should().Be(PortalItemType.Unknown);
        }
    }
}