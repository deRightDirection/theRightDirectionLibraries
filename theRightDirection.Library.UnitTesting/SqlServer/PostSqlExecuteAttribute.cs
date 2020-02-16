using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library.UnitTesting.SqlServer
{
    public class PostSqlExecuteAttribute : SqlExecuteAttribute
    {
        public PostSqlExecuteAttribute(string databaseName = null, string serverName = null)
        {
            ReadProperties();
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch (Exception e)
            {

            }
        }
    }
}