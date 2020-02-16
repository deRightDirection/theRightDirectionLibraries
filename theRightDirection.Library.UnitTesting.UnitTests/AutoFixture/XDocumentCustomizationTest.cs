using System;
using theRightDirection.Library.UnitTesting.AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace theRightDirection.Library.UnitTesting.UnitTests
{
    [TestClass]
    public class XDocumentCustomizationTest
    {
        [TestMethod]
        public void AutoFixture_Freeze_Valid_XDocument()
        {
            var fixture = new Fixture();
            fixture.Customize(new XDocumentCustomization());
            var xDocument = fixture.Freeze<XDocument>();
            var rootNodeName = xDocument.Root.Name.ToString();
            Assert.AreEqual("MannusUnitTesting", rootNodeName);
        }

        [TestMethod]
        public void AutoFixture_Create_Valid_XDocument()
        {
            var fixture = new Fixture();
            fixture.Customize(new XDocumentCustomization());
            var xDocument = fixture.Create<XDocument>();
            var rootNodeName = xDocument.Root.Name.ToString();
            Assert.AreEqual("MannusUnitTesting", rootNodeName);
        }

        [TestMethod]
        public void AutoFixture_Create_Valid_XDocument_On_TestObject_Property()
        {
            var fixture = new Fixture();
            fixture.Customize(new XDocumentCustomization());
            var testObject = fixture.Create<TestObject>();
            var rootNodeName = testObject.GetXDocument.Root.ToString();
            Assert.AreEqual("<MannusUnitTesting />", rootNodeName);
        }

        [TestMethod]
        public void AutoFixture_Create_Valid_XDocument_On_TestObject_Method_But_Empty_XDocument()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Customize(new XDocumentCustomization());
            var testObject = fixture.Create<TestObject2>();
            var documentType = testObject.GetXml().NodeType;
            Assert.AreEqual(XmlNodeType.Document, documentType);
        }

        [TestMethod]
        public void AutoFixture_Create_Valid_XDocument_On_TestObject_Method_And_Mock_XDocument()
        {
            Assert.Inconclusive(" doesn't work right now");
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Customize(new XDocumentCustomization());
            fixture.Customize(new AutoPopulatedMoqPropertiesCustomization());
            var testObject = fixture.Create<TestObject2>();
            var rootNodeName = testObject.GetXml().Root.ToString();
            Assert.AreEqual("<MannusUnitTesting />", rootNodeName);
        }
    }
}