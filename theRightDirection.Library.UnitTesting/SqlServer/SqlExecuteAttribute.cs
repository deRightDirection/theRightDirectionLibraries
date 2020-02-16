using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library.UnitTesting.SqlServer
{
    public abstract class SqlExecuteAttribute : Attribute
    {
        protected void ReadProperties()
        {
            // TODO beter bestand maken tegen null-references
            var configuration = UnitTestingConfiguration.GetConfig();
            DatabaseName = configuration.DatabaseName;
            ServerName = configuration.ServerName;
            if (string.IsNullOrEmpty(ServerName))
            {
                ServerName = Environment.MachineName;
            }
        }
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}