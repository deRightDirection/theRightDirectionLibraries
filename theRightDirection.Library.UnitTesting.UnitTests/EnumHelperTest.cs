using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theRightDirection.Library;
namespace theRightDirection.Library.UnitTesting
{
    [TestClass]
    public class EnumHelperTest
    {
        [TestMethod]
        public void EnumHelper_Default_Value()
        {
            var parseResult = EnumHelper.TryParseTextToEnumValue("Thierry", out TestEnum result);
            result.Should().Be(TestEnum.Onbekend);
            parseResult.Should().BeFalse();
        }
        [TestMethod]
        public void EnumHelper_Value()
        {
            var parseResult = EnumHelper.TryParseTextToEnumValue("Mannus", out TestEnum result);
            result.Should().Be(TestEnum.Mannus);
            parseResult.Should().BeTrue();
        }
    }

    public enum TestEnum
    {
        Onbekend = 0,
        Mannus = 1,
        Verena = 2
    }
}
