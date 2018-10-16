using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework.Constants;
using BATDemoFramework.EmailService;
using BATDemoFramework.Models;
using Newtonsoft.Json;

namespace BATDemoFramework.Generators
{
    public class UserGenerator
    {
        private static readonly HttpClient client = new HttpClient();
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
                return DateTime.Now.AddMonths(-2).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
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
                TenantName = UserDefaultValues.TennantName,
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
            var json = JsonConvert.SerializeObject(user);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(UserDefaultValues.UserCreationURL, content);

            if( response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed to create user.");
            }

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
            var json = JsonConvert.SerializeObject(user);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(UserDefaultValues.LoginURL , content);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<Dictionary<string,string>>(responseString);

            return responseJson["AccessToken"];
        } 

        public async Task<bool> SkipSecondaryEmail(string accessToken)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), UserDefaultValues.SkipSecondaryEmailURL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed to create user.");
            }

            return true;

        }

        public async Task<HttpResponseMessage> SetMarketingPreferences()
        {
            var user = new MarketingPreferenceModel
            {
                Email = false,
                Post = false,
                Sms = false,
                Telephone = false
            };
            var json = JsonConvert.SerializeObject(user);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), UserDefaultValues.SetMarketingPreferencesURL)
            {
                Content = content
            };
            var response = await client.SendAsync(request);

            return response;
        }

    }
}    
