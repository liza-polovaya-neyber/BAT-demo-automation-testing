using System;

namespace BATDemoFramework.Generators
{
    public static class EmailAddressGenerator
    {
        private static Random random = new Random();

        public static string GenerateEmailAddress()
        {
            var email =  $"liza.polovaya.auto{random.Next(10000, 99999)}@yopmail.com";
            LastGeneratedEmail = email;
            return email;
        }

        public static string LastGeneratedEmail { get; set; }
    }
}