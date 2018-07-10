using BATDemoFramework.Models;

namespace BATDemoFramework.Generators
{
    public class UserGenerator
    {
        public static User LastGeneratedUser { get; set; }
        public static SSOUser LastSSOGeneratedUser { get; set; }

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

        public static SSOUser GetNewSSOUser()
        {
            var user = new SSOUser
            {
                FirstName = "Randomfirstname",
                Surname = "Randomlastname",
                DOB = "04/01/1980",
                CompanyName = "British Transport Police",
                JoiningDate = "05/10/2016", //can be less than 6 months
                Email = EmailAddressGenerator.GenerateEmailAddress(),
                Salary = "50000",
                EmployeeId = PasswordGenerator.GeneratePassword(),
                JobBand = "Serving Officer",
                JobTitle = "Admin",
                NiNumber = PasswordGenerator.GeneratePassword(),
                CapitaGUID = GuidGenerator.GenerateGuid()

            };

            LastSSOGeneratedUser = user;
            return user;
        }
    }
}    
