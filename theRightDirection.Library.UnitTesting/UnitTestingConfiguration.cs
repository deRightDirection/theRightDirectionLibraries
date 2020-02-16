using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theRightDirection.Library.Configuration;

namespace theRightDirection.Library.UnitTesting
{
    public sealed class UnitTestingConfiguration : ConfigurationSection
    {

        public static UnitTestingConfiguration GetConfig()
        {
            return ConfigurationManager.GetSection("theRightDirection.Library.UnitTesting") as UnitTestingConfiguration;
        }

        [ConfigurationProperty("Configuration")]
        public GenericConfigurationElementCollection<UnitTestingConfigurationProperty> Configuration
        {
            get { return (GenericConfigurationElementCollection<UnitTestingConfigurationProperty>)this["Configuration"]; }
        }

        public string ServerName
        {
            get
            {
                if (Configuration["servername"] == null)
                {
                    return null;
                }
                else
                {
                    return Configuration["servername"].Value;
                }
            }
        }

        public string DatabaseName
        {
            get
            {
                return Configuration["databasename"].Value;
            }
        }
    }
}