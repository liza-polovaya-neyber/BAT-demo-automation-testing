using System;
using System.Linq;

namespace BATDemoFramework.Generators
{
    public static class PasswordGenerator
    {
        public static  string GeneratePassword()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var lowerCasePartOfPassword = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray()).ToLower();
            var upperCasePartOfPassword = new string(Enumerable.Repeat(chars, 1).Select(s => s[random.Next(s.Length)]).ToArray());
            var digitPartOfPassword = random.Next(9);

            var pwd = $"{lowerCasePartOfPassword}{upperCasePartOfPassword}{digitPartOfPassword}";
            LastGeneratedPassword = pwd;
            return pwd;
        }

        public static string LastGeneratedPassword { get; set; }
     }

    //private static bool toggle = true;
    //public static string GetNewPassword()
    //{
    //    var pwd = System.Guid.NewGuid().ToString();
    //    //pwd = pwd.Replace("-", char.ToUpper());
    //    LastGeneratedPassword = pwd;
    //    return pwd;
    //}

    //public static string GetNewPassword(int length, int numberOfNonAlphanumericCharacters)
    //{
    //    var pwd = System.Web.Security.Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
    //    LastGeneratedPassword = pwd;
    //    return pwd;
    //}
}

