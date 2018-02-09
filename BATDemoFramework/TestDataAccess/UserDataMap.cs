using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace BATDemoFramework.TestDataAccess
{
    public class UserDataMap : ClassMap<UserData>
    {
        public UserDataMap()
        {
            try
            {
                Map(m => m.Key).Name("Key");
                Map(m => m.Email).Name("Email");
                Map(m => m.Password).Name("Password");
                Map(m => m.FirstName).Name("FirstName");
                Map(m => m.LastName).Name("LastName");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
