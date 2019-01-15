using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework.Models;
using Neyber.Atlas.Services.Services;
using RestSharp;

namespace BATDemoFramework.Services
{
    public class UserService
    {
        private readonly RestService restService = new RestService();
        private readonly string ProfileUrl = ConfigurationManager.AppSettings["ProfileUrl"];
        private const string UserCreationUrl = "api/v2/profile";
        private const string LoginUrl = "api/v2/account/login";
        private const string SetClientIdUrl = "api/v2/profile/organisationDetails";
        private const string SkipSecondaryEmailUrl = "api/v2/profile/skipSecondaryEmail";
        private const string SetMarketingPreferencesUrl = "api/v2/profile/marketingPreferences";
        private const string SetConsentUrl = "api/v2/loanApplications/createFMRApplication";
        private const string SetTotalIncomeUrl = "api/v2/loanApplications/{0}/totalIncome";
        private const string SetSufficentIncomeUrl = "api/v2/loanApplications/{0}/fmrStatus/Sufficient%20Income";
        private const string SetAddressUrl = "api/v2/loanApplications/{0}/address";
        private const string TriggerCreditCheckUrl = "api/v2/loanApplications/creditCheck/trigger";
        

        public UserService()
        {
            restService.AddBaseUrl(ProfileUrl);
        }

        public async Task CreateDefaultUserAsync(object user)
        {
            var response = await restService.ExecuteAsync(UserCreationUrl, Method.POST, user);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Unable to create user");
            }
        }

        public async Task<string> CreateAutoLoginAsync(object user)
        {
            var response = await restService.ExecuteAsync<Dictionary<string, string>>(LoginUrl, Method.POST, user);
            return response["AccessToken"];
        }

        public void UpdateAuthenticationHeader(string accessToken)
        {
            restService.AddDefaultHeader("Authorization", $"Bearer {accessToken}");
        }

        public async Task SetTenantAsync(object clientId)
        {
            var response = await restService.ExecuteAsync(SetClientIdUrl, Method.PATCH, clientId);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Unable to set client ID");
            }
        }

        public async Task SkipSecondaryEmailtAsync()
        {
            var response = await restService.ExecuteAsync(SkipSecondaryEmailUrl, Method.PATCH);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Unable to set marketing preferences");
            }
        }

        public async Task SetMarketingPreferencesAsync(object marketingPreferences)
        {
            var response = await restService.ExecuteAsync(SetMarketingPreferencesUrl, Method.PATCH, marketingPreferences);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Unable to set marketing preferences");
            }
        }

        public async Task<string> SetConsentAsync(object consent)
        {
            var response = await restService.ExecuteAsync<Dictionary<string, string>>(SetConsentUrl, Method.POST, consent);
            return response["LoanApplicationId"];
        }

        public async Task SetTotalIncomeAsync(object totalIncome, string loanApplicationId)
        {
            var setTotalIncomeUrl = string.Format(SetTotalIncomeUrl, loanApplicationId);
            var response = await restService.ExecuteAsync(setTotalIncomeUrl, Method.PATCH, totalIncome);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Unable to set total income");
            }
        }

        public async Task SetSufficentIncomeAsync(string loanApplicationId)
        {
            var setSufficentIncomeUrl = string.Format(SetSufficentIncomeUrl, loanApplicationId);
            var response = await restService.ExecuteAsync(setSufficentIncomeUrl, Method.PATCH);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Unable to set sufficient income");
            }
        }

        public async Task SetAddressAsync(object address, string loanApplicationId)
        {
            var setAddressUrl = string.Format(SetAddressUrl, loanApplicationId);
            var response = await restService.ExecuteAsync(setAddressUrl, Method.POST, address);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Unable to set address");
            }
        }

        public async Task TriggerCreditCheckAsync()
        {
            await restService.ExecuteAsync<string>(TriggerCreditCheckUrl, Method.POST);
        }
    }
}
