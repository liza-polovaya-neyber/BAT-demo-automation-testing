namespace BATDemoFramework.Generators
{
    public static class PasswordGenerator
    {
        //private static bool toggle = true;
        public static string GetNewPassword()
        {
            var pwd = System.Guid.NewGuid().ToString();
            LastGeneratedPassword = pwd;
            return pwd;
        }

        public static string GetNewPassword(int length, int numberOfNonAlphanumericCharacters)
        {
            return System.Web.Security.Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
        }

        public static string LastGeneratedPassword { get; set; }
    }
}