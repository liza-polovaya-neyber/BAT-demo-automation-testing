using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Constants
{
    public static class UserDefaultValues
    {
        public static string ClientId => "adecco";
        public static string Title  => "Mr";
        public static string Surname  => "Randomlastname";
        public static string FirstName  => "Randomfirstname";        
        public static DateTime DateOfBirth  => new DateTime(1998, 1, 1);
        public static DateTime DateOfBirthAt21  => DateTime.Now.AddYears(-21).Date;
        public static string MobilePhone => "07969122448";
        public static string Feedback => "My employee benefits platform";
        public static int TotalIncome => 30000;
        public static string FlatNumber => "";
        public static string HouseName => "Qw";
        public static string HouseNumber => "359";
        public static string Line1 => "Downham Road";
        public static string Line2 => "";
        public static string County => "";
        public static string PostCode => "CB6 1AF";
        public static string Town => "Ely";
        public static string Country => "United Kingdom";
        public static bool AddedManually => true;
        public static int MovedInMonth => 1;
        public static int MovedInYear => 1999;
    }
}