using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Given
{
    public class UserCreator
    {
        public async Task<UserLoginModel> CreateUserAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            await userGen.VerifyEmail(user);
            return user;
        }

        public async Task<UserLoginModel> CreateUserAndSelectEmployerAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            await userGen.VerifyEmail(user);
            var accessToken = await userGen.CreateAutoLogin(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            return user;
        }

        public async Task<UserLoginModel> CreateUserAndSkipAlternativeEmailAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            await userGen.VerifyEmail(user);
            var accessToken = await userGen.CreateAutoLogin(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            await userGen.SkipSecondaryEmail();
            return user;
        }

        public async Task<UserLoginModel> CreateUserAndSetMarketingPreferencesAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            await userGen.VerifyEmail(user);
            var accessToken = await userGen.CreateAutoLogin(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            await userGen.SkipSecondaryEmail();
            await userGen.SetMarketingPreferences();
            return user;
        }

        public async Task<UserLoginModel> CreateUserAndPassFmrAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            await userGen.VerifyEmail(user);
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