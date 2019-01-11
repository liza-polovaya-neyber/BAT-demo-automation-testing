using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework.Constants;
using BATDemoFramework.EmailService;
using BATDemoFramework.Helpers;
using BATDemoFramework.Models;
using Newtonsoft.Json;

namespace BATDemoFramework.Generators
{
    public class UserGenerator
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly RestClient restClient = new RestClient();
        public static User LastGeneratedUser { get; set; }
        public static SSOUser LastSSOGeneratedUser { get; set; }
        public static UserLoginModel LastQuickGeneratedUser { get; set; }

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

            string userCreationUrl = ConfigurationManager.AppSettings["UserCreation.Url"];
            await restClient.ExecuteAsync<string>(userCreationUrl, "POST", user);

            /*var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(userCreationUrl, content);*/


            var emailService = new EmailService.EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.Email);
            if (messages.Count == 0)
            {
                throw new Exception($"Can't get confirmation email: {user.Email}");
            }
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);

            LastQuickGeneratedUser = user;
            return user;
        }

        public async Task<String> CreateAutoLoginAsync(string userName, string password)
        {
            var user = new AutoLoginModel
            {
                UserName = userName,
                Password = password,
                ClientId = "profileui",
                ClientSecret = "C04FBFC8-D59B-49D7-8968-7A249EA874F6",
                Scope = "openid profile heracles atlas mercury finwell"
            };

            string loginUrl = ConfigurationManager.AppSettings["Login.Url"];


            var response = await restClient.ExecuteAsync<Dictionary<string, string>>(loginUrl, "POST", user);

            return response["AccessToken"];
        }

        public void UpdateAuthenticationHeader(string accessToken)
        {
            restClient.UpdateAuthenticationHeader(accessToken);
        }

        public async Task SetTenant()
        {
            string setClientIdUrl = ConfigurationManager.AppSettings["SetClientId.Url"];

            await restClient.ExecuteAsyncNoResponseExpected(setClientIdUrl, "PATCH", new {clientId = UserDefaultValues.ClientId});
        }

        public async Task SkipSecondaryEmail()
        {
            string skipSecondaryEmailUrl = ConfigurationManager.AppSettings["SkipSecondaryEmail.Url"];

            await restClient.ExecuteAsyncNoResponseExpected(skipSecondaryEmailUrl, "PATCH");
        }

        public async Task SetMarketingPreferences()
        {
            var user = new MarketingPreferenceModel
            {
                Email = false,
                Post = false,
                Sms = false,
                Telephone = false
            };

            string setMarketingPreferencesUrl = ConfigurationManager.AppSettings["SetMarketingPreferences.Url"];

            await restClient.ExecuteAsyncNoResponseExpected(setMarketingPreferencesUrl, "PATCH", user);
        }

        public async Task<string> SetConsent()
        {
            var user = new ConsentModel()
            {
                Consent = true,
                IsTopup = false
            };

            string setConsentUrl = ConfigurationManager.AppSettings["SetAcceptConsent.Url"];

            var response = await restClient.ExecuteAsync<Dictionary<string, string>>(setConsentUrl, "POST", user);

            return response["LoanApplicationId"];
        }

        public async Task SetTotalIncome(string loanApplicationId)
        {
            var user = new TotalIncomeModel()
            {
                TotalIncome = UserDefaultValues.TotalIncome
            };

            string setTotalIncome = ConfigurationManager.AppSettings["FMR.Url"];
            setTotalIncome = setTotalIncome + loanApplicationId + "/totalIncome";

            await restClient.ExecuteAsyncNoResponseExpected(setTotalIncome, "PATCH", user);
        }

        public async Task SetSufficentIncome(string loanApplicationId)
        {
            string setSufficentIncome = ConfigurationManager.AppSettings["FMR.Url"];
            setSufficentIncome = setSufficentIncome + loanApplicationId + "/fmrStatus/Sufficient%20Income";

            await restClient.ExecuteAsyncNoResponseExpected(setSufficentIncome, "PATCH");
        }

        public async Task SetAddress(string loanApplicationId)
        {
            var user = new AddressModel
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

            string setAddress = ConfigurationManager.AppSettings["FMR.Url"];
            setAddress = setAddress + loanApplicationId + "/address";

            await restClient.ExecuteAsyncNoResponseExpected(setAddress, "POST", user);
        }

        public async Task TriggerCreditCheck()
        {
            string triggerCreditCheckUrl = ConfigurationManager.AppSettings["TriggerCreditCheck.Url"];

            await restClient.ExecuteAsyncNoResponseExpected(triggerCreditCheckUrl, "POST");
        }
    }
}