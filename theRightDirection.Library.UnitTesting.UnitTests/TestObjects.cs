using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace theRightDirection.Library.UnitTesting.UnitTests
{
    public class TestObject
    {
        public TestObject()
        {
        }

        public XDocument GetXDocument { get; set; }
    }

    public class TestObject2
    {
        private TestInterface _testInterfaceImplementation;

        public TestObject2(TestInterface testInterfaceImplementation)
        {
            _testInterfaceImplementation = testInterfaceImplementation;
        }

        public int GetNumber()
        {
            return _testInterfaceImplementation.GetNumber() + 5;
        }

        public XDocument GetXml()
        {
            return _testInterfaceImplementation.GetXml();
        }

        public string GetName()
        {
            return _testInterfaceImplementation.Name;
        }
    }

    public class TestObject3 : TestInterface
    {
        public XDocument GetXml()
        {
            throw new NotImplementedException();
        }

        public int GetNumber()
        {
            throw new NotImplementedException();
        }

        public string Name {get;set;}
    }
}