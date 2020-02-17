using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using theRightDirection.Library.UnitTesting.AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace theRightDirection.Library.UnitTesting.UnitTests.AutoFixture
{
    [TestClass]
    public class AutoPopulatedMoqPropertiesCustomizationTest
    {
        [TestMethod]
        public void Name_With_Customization()
        {
            var fixture = new Fixture().Customize(new AutoPopulatedMoqPropertiesCustomization());
            var testObject = fixture.Create<Mock<TestInterface>>();
            testObject.Object.Name = fixture.Create<string>();
            Assert.AreNotEqual(default(string), testObject.Object.Name);
        }

        [TestMethod]
        public void Name_With_Customization2()
        {
            var fixture = new Fixture().Customize(new AutoPopulatedMoqPropertiesCustomization());
            var testObject = fixture.Create<TestInterface>();
            Assert.AreNotEqual(default(string), testObject.Name);
        }

        [TestMethod]
        public void Name_With_Customization3()
        {
            var fixture = new Fixture().Customize(new AutoPopulatedMoqPropertiesCustomization());
            var testObject = fixture.Create<TestInterface>();
            var testDouble = Mock.Get(testObject);
            Assert.AreNotEqual(default(string), testDouble.Object.Name);
        }


        [TestMethod]
        public void Name_Without_Customization()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            var testObject = fixture.Create<Mock<TestInterface>>();
            Assert.IsNull(testObject.Object.Name);
        }
    }
}