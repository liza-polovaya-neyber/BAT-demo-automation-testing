using System;
using System.Globalization;
using System.Threading.Tasks;
using BATDemoFramework.Constants;
using BATDemoFramework.EmailServices;
using BATDemoFramework.Models;
using BATDemoFramework.Services;

namespace BATDemoFramework.Generators
{
    public class UserGenerator
    {
        private readonly UserService userService = new UserService();
        private readonly EmailService emailService = new EmailService();
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
                return DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
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

        public async Task<UserLoginModel> CreateDefaultUser()
        {
            var user = new UserLoginModel
            {
                Title = UserDefaultValues.Title,
                Surname = UserDefaultValues.Surname,
                FirstName = UserDefaultValues.FirstName,
                DoB = UserDefaultValues.DateOfBirthAt21,
                Email = EmailAddressGenerator.GenerateEmailAddress(),
                MobilePhone = UserDefaultValues.MobilePhone,
                Password = PasswordGenerator.GeneratePassword(),
                Feedback = UserDefaultValues.Feedback,
                AffinityTermsAndPrivacyPolicyAccepted = false,
                AgreedPoliceMutualMarketing = false,
                OptOutEmailPolicyAccepted = false,
                TermsAndPrivacyPolicyAccepted = true
            };

            await userService.CreateDefaultUserAsync(user);

            return user;
        }

        public async Task VerifyEmail(UserLoginModel user)
        {
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.Email);
            if (messages.Count == 0)
            {
                throw new Exception($"Can't get confirmation email: {user.Email}");
            }
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
        }

        public async Task<String> CreateAutoLogin(string userName, string password)
        {
            var user = new AutoLoginModel
            {
                UserName = userName,
                Password = password,
                ClientId = "profileui",
                ClientSecret = "C04FBFC8-D59B-49D7-8968-7A249EA874F6",
                Scope = "openid profile heracles atlas mercury finwell"
            };

            var response = await userService.CreateAutoLoginAsync(user);

            return response;
        }

        public void UpdateAuthenticationHeader(string accessToken)
        {
            userService.UpdateAuthenticationHeader(accessToken);
        }

        public async Task SetTenant()
        {
            await userService.SetTenantAsync(new {clientId = UserDefaultValues.ClientId});
        }

        public async Task SkipSecondaryEmail()
        {
            await userService.SkipSecondaryEmailtAsync();
        }

        public async Task SetMarketingPreferences()
        {
            var marketingPreferences = new MarketingPreferenceModel
            {
                Email = false,
                Post = false,
                Sms = false,
                Telephone = false
            };

            await userService.SetMarketingPreferencesAsync(marketingPreferences);
        }

        public async Task<string> SetConsent()
        {
            var consent = new ConsentModel()
            {
                Consent = true,
                IsTopup = false
            };

            var response = await userService.SetConsentAsync(consent);

            return response;
        }

        public async Task SetTotalIncome(string loanApplicationId)
        {
            var totalIncome = new TotalIncomeModel()
            {
                TotalIncome = UserDefaultValues.TotalIncome
            };

            await userService.SetTotalIncomeAsync(totalIncome, loanApplicationId);
        }

        public async Task SetSufficentIncome(string loanApplicationId)
        {
            await userService.SetSufficentIncomeAsync(loanApplicationId);
        }

        public async Task SetAddress(string loanApplicationId)
        {
            var address = new AddressModel
            {
                FlatNumber = UserDefaultValues.FlatNumber,
                HouseName = UserDefaultValues.HouseName,
                HouseNumber = UserDefaultValues.HouseNumber,
                Line1 = UserDefaultValues.Line1,
                Line2 = UserDefaultValues.Line2,
                County = UserDefaultValues.County,
                PostCode = UserDefaultValues.PostCode,
                Town = UserDefaultValues.Town,
                Country = UserDefaultValues.Country,
                AddedManually = UserDefaultValues.AddedManually,
                MovedInMonth = UserDefaultValues.MovedInMonth,
                MovedInYear = UserDefaultValues.MovedInYear
            };
            await userService.SetAddressAsync(address, loanApplicationId);
        }

        public async Task TriggerCreditCheck()
        {
            await userService.TriggerCreditCheckAsync();
        }
    }
}