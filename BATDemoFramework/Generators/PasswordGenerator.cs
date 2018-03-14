namespace BATDemoFramework.Generators
{
    public static class PasswordGenerator
    {
        //private static bool toggle = true;
        public static string GetNewPassword()
        {
            var pwd = System.Guid.NewGuid().ToString();
            //pwd = pwd.Replace("-", char.ToUpper());
            LastGeneratedPassword = pwd;
            return pwd;
        }

        public static string GetNewPassword(int length, int numberOfNonAlphanumericCharacters)
        {
            var pwd = System.Web.Security.Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
            LastGeneratedPassword = pwd;
            return pwd;
        }

        public static string LastGeneratedPassword { get; set; }
    }
}