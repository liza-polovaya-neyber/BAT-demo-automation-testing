namespace BATDemoFramework.Generators
{
    public class UserGenerator
    {
        public static User LastGeneratedUser { get; set; }

        public static User GetNewUser()
        {
            var user = new User
            {
                EmailAddress = EmailAddressGenerator.GenerateEmailAddress(),
                Password = PasswordGenerator.GetNewPassword()
            };

            LastGeneratedUser = user;
            return user;
        }
    }
}    
