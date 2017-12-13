namespace BATDemoFramework.Generators
{
    public class UserGenerator
    {
        public static User LastGeneratedUser;
        private static int length;
        private static int numberOfAlphaCharacters;
        private static int numberOfNumericCharacters;

        public void Initialize()
        {
            LastGeneratedUser = null;
        }

        public User Generate()
        {
            var user = new User
            {
                EmailAddress = EmailAddressGenerator.GenerateEmailAddress(),
                Password = GetPassword()
            };

            LastGeneratedUser = user;
            return user;
        }


        private string GetPassword()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}    
