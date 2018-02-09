using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using Dapper;
using System.Collections.Generic;
using System;
using CsvHelper;
using System.IO;
using System.Reflection;

namespace BATDemoFramework.TestDataAccess
{
    public static class CsvDataAccess
    {
        private static List<UserData> GetData()
        {


            var path = $@"{AppDomain.CurrentDomain.BaseDirectory}\Data\TestData.csv";
            using (var csv = new CsvReader(File.OpenText(path)))
            {
                try {
                    csv.Configuration.MissingFieldFound = null;
                    
                    var Data = csv.Configuration.RegisterClassMap<UserDataMap>();
                    
                
                return csv.GetRecords<UserData>().ToList();
                }
                catch (Exception ex)
                {
                    return new List<UserData>();
                }
            }

        }

        //public static string TestDataFileConnection()
        //{
        //    var fileName = ConfigurationManager.AppSettings["TestDataSheetPath"];
        //    var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=XML 1.0 Spreadsheet;", fileName);
        //    return con;
        //}

        public static UserData GetTestData(string keyName)
        {
            var items = GetData();
            return items.FirstOrDefault(i => i.Key == keyName);

        //using (var connection = new OleDbConnection(TestDataFileConnection()))
        //{
        //    connection.Open();
        //    var query = string.Format("select * from [DataSet$] where key='{0}'", keyName);
        //    var value = connection.Query<UserData>(query).FirstOrDefault();
        //    connection.Close();
        //    return value;
        //}
    }
    }
}
