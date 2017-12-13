namespace BATDemoFramework.Generators
{
    public static class PasswordGenerator
    {
        //private static bool toggle = true;
        public static void GeneratePassword(int length, int numberOfAlphaCharacters, int numberOfNumericCharacters)
        {
            //var password = "";
            //password = toggle ? "Password" : "New Password";

            //toggle = !toggle;
            //LastGeneratedPassword = password;
            //return password;
        }

        public static string LastGeneratedPassword { get; set; }
    }
}