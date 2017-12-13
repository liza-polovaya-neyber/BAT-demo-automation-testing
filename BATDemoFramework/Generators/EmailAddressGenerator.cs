using System;

namespace BATDemoFramework.Generators
{
    public class EmailAddressGenerator
    {
        private static Random random = new Random();

        public static string GenerateEmailAddress()
        {
            return string.Format("liza.polovaya.auto{0}@yopmail.com", random.Next(10000, 99999));
        }
    }
}