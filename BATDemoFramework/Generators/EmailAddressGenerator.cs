using System;

namespace BATDemoFramework.Generators
{
    public static class EmailAddressGenerator
    {
        private static Random random = new Random();

        public static string GenerateEmailAddress()
        {
            var email =  $"neyber.test+{random.Next(10000, 99999)}@gmail.com";
            LastGeneratedEmail = email;
            return email;
        }

        public static string LastGeneratedEmail { get; set; }
    }
}