using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Given
{
    public class UserCreatedAndMarketingPreferencesSet
    {
        public static async Task<UserLoginModel> CreateUserAndSetMarketingPreferencesAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            var result = await userGen.CreateAutoLoginAsync(user.Email, user.Password);
            await userGen.SetTenant(result);
            await userGen.SkipSecondaryEmail();
            await userGen.SetMarketingPreferences();
            return user;
        }
    }
}
