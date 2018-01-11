using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using Dapper;

namespace BATDemoFramework.TestDataAccess
{
    class CsvDataAccess
    {
        public static string TestDataFileConnection()
        {
            var fileName = ConfigurationManager.AppSettings["TestDataSheetPath"];
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties= XML 1.0 Spreadsheet ;", fileName);
            return con;
        }

        public static UserData GetTestData(string testName)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                connection.Open();
                var query = string.Format("select * from [DataSet$] where key='{0}'", testName);
                var value = connection.Query<UserData>(query).FirstOrDefault();
                connection.Close();
                return value;
            }
        }
    }
}
