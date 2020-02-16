using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using theRightDirection.Library.UnitTesting.SqlServer;
using System.Data.SqlClient;
using System.Configuration;

namespace theRightDirection.Library.UnitTesting.UnitTests.SqlServer
{
    [TestClass]
    public class SqlServerAttributesTest : UnitTestingBase
    {   
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        [PreSqlExecute]
        [PostSqlExecute]
        public void CheckDataIsNotInDatabase()
        {
            var startValue = GetCount();
            Assert.AreEqual(14, startValue);
            AddNumber();
            var endValue = GetCount();
            Assert.AreEqual(15, endValue);
        }

        private void AddNumber()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Testdatabase"];
            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                using (var command = new SqlCommand("INSERT INTO ids (id) VALUES (11)", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private int GetCount()
        {
            int result = 0;
            var connectionString = ConfigurationManager.ConnectionStrings["Testdatabase"];
            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                using (var command = new SqlCommand("SELECT count(*) FROM ids", connection))
                {
                    connection.Open();
                    var count = command.ExecuteScalar();
                    result = int.Parse(count.ToString());
                    connection.Close();
                }
            }
            return result;
        }
    }
}