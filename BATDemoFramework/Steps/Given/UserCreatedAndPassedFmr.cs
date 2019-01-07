using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Given
{
    public class UserCreatedAndPassedFmr
    {
        public static async Task<UserLoginModel> CreateUserAndPassFmrAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            var result = await userGen.CreateAutoLoginAsync(user.Email, user.Password);
            await userGen.SetTenant(result);
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
