using System;
using System.Globalization;
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

        public static SSOUser GetNewSSOUser(bool isJoiningDateValid = true)
        {
            var user = new SSOUser
            {
                FirstName = "Randomfirstname",
                Surname = "Randomlastname",
                DOB = "04/01/1980",
                CompanyName = "British Transport Police",
                JoiningDate = GetJoiningDate(isJoiningDateValid), //can be less than 6 months
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


        private static string GetJoiningDate(bool isJoiningDateValid)
        {
            if (isJoiningDateValid)
                return DateTime.Now.AddMonths(-7).ToString("dd/MM/yyyy");
            else
                return DateTime.Now.AddMonths(-5).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static SSOUser ConvertUserToSSOUser(User newUser)
        {
            return new SSOUser
            {
                Email = newUser.EmailAddress,
                FirstName = newUser.FirstName,
                Surname = newUser.LastName,
                DOB = "04/01/1980",
                CompanyName = "British Transport Police",
                JoiningDate = "04/04/2016",
                Salary = "50000",
                EmployeeId = PasswordGenerator.GeneratePassword(),
                JobBand = "Serving Officer",
                JobTitle = "Admin",
                NiNumber = PasswordGenerator.GeneratePassword(),
                CapitaGUID = GuidGenerator.GenerateGuid()
            };
        }
    }
}    
