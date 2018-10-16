using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Constants
{
    public static class UserDefaultValues
    {
        public static string TennantName => "Adecco";
        public static string Title  => "Mr";
        public static string Surname  => "Randomlastname";
        public static string FirstName  => "Randomfirstname";        
        public static DateTime DateOfBirth  => new DateTime(1998, 1, 1);
        public static DateTime DateOfBirthAt21  => DateTime.Now.AddYears(-21).Date;
        public static string MobilePhone => "07969122448";
        public static string Feedback => "My employee benefits platform";
        public static string UserCreationURL => "https://hellotest1.neyber.co.uk/api/v2/profile";
        public static string LoginURL => "https://hellotest1.neyber.co.uk/api/v2/account/login";
        public static string SkipSecondaryEmailURL => "https://hellotest1.neyber.co.uk/api/v2/profile/skipSecondaryEmail";
        public static string SetMarketingPreferencesURL => "https://hellotest1.neyber.co.uk/api/v2/profile/marketingPreferences";
    }
}
