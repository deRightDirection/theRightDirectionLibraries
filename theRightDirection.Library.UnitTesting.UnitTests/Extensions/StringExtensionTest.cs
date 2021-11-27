using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using theRightDirection.Library.Enumerations;

namespace theRightDirection.Library.UnitTesting.UnitTests.Extensions
{
    [TestClass]
    public class StringExtensionTest
    {
        [TestMethod]
        public void String_Has_Only_First_Word_Capital()
        {
            var s = "Mannus & Verena";
            var result = s.ToTitleCase();
            result.Should().Be("Mannus & verena");
        }
        [TestMethod]
        public void String_Has_Only_First_Word_Capital2()
        {
            var s = "mannus & verena";
            var result = s.ToTitleCase();
            result.Should().Be("Mannus & verena");
        }
        [TestMethod]
        public void String_Has_All_Words_Start_With_Capital()
        {
            var s = "mannus & verena";
            var result = s.ToTitleCase(TitleCase.All);
            result.Should().Be("Mannus & Verena");
        }
    }
}
