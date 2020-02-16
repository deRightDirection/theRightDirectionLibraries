using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theRightDirection.Library.Configuration;

namespace theRightDirection.Library.UnitTesting
{
    public class UnitTestingConfigurationProperty : ConfigurationElementBase
    {
        [ConfigurationProperty("value", IsRequired = false)]
        public string Value
        {
            get { return this["value"].ToString(); }
        }

        public override string ElementName
        {
            get { return "UnitTestingConfigurationProperty"; }
        }
    }
}