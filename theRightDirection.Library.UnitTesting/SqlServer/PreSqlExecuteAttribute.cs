using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library.UnitTesting.SqlServer
{
    public class PreSqlExecuteAttribute : SqlExecuteAttribute
    {
        public string SqlScriptToExecute { get; set; }

        public PreSqlExecuteAttribute(string databaseName = null, string serverName = null, string sqlScript = null)
        {
            SqlScriptToExecute = sqlScript;
            ReadProperties();
        }
    }
}