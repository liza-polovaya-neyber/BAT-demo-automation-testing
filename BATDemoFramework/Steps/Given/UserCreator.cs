using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Given
{
    public class UserCreator
    {
        private UserGenerator userGen = new UserGenerator();

        private async Task<UserLoginModel> CreateAndVerifyUserAsync()
        {
            var user = await userGen.CreateDefaultUser();
            await userGen.VerifyEmail(user);
            return user;
        }

        public async Task<UserLoginModel> CreateUserWithTenantAsync()
        {
            var user = await userGen.CreateUserWithTenant();
            await userGen.VerifyEmail(user);
            return user;
        }

        public Task<UserLoginModel> CreateUserAsync()
        {
            return CreateAndVerifyUserAsync();
        }

        public async Task<UserLoginModel> CreateUserAndSelectEmployerAsync()
        {
            var user = await CreateAndVerifyUserAsync();
            var accessToken = await userGen.CreateAutoLogin(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            return user;
        }

        public async Task<UserLoginModel> CreateUserAndSkipAlternativeEmailAsync()
        {
            var user = await CreateAndVerifyUserAsync();
            var accessToken = await userGen.CreateAutoLogin(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            await userGen.SkipSecondaryEmail();
            return user;
        }

        public async Task<UserLoginModel> CreateUserAndSetMarketingPreferencesAsync()
        {
            var user = await CreateAndVerifyUserAsync();
            var accessToken = await userGen.CreateAutoLogin(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            await userGen.SkipSecondaryEmail();
            await userGen.SetMarketingPreferences();
            return user;
        }

        public async Task<UserLoginModel> CreateUserAndPassFmrAsync()
        {
            var user = await CreateAndVerifyUserAsync();
            var accessToken = await userGen.CreateAutoLogin(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            await userGen.SkipSecondaryEmail();
            await userGen.SetMarketingPreferences();
            string loanApplicationId = await userGen.SetConsent();
            await userGen.SetTotalIncome(loanApplicationId);
            await userGen.SetSufficentIncome(loanApplicationId);
            await userGen.SetAddress(loanApplicationId);
            await userGen.TriggerCreditCheck();
            return user;
        }
    }
}