using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace theRightDirection.Library.UnitTesting.UnitTests
{
    public interface TestInterface
    {
        XDocument GetXml();

        int GetNumber();

        string Name { get; set; }
    }
}