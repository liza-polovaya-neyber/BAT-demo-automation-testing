namespace BATDemoFramework.Generators
{
    public class UserGenerator
    {
        public static User LastGeneratedUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User GetNewUser()
        {
            var user = new User
            {
                EmailAddress = EmailAddressGenerator.GenerateEmailAddress(),
                Password = PasswordGenerator.GeneratePassword(),
                FirstName = "Randomfirstname",
                LastName = "Randomlastname"

            };

            LastGeneratedUser = user;
            return user;
        }
    }
}    
