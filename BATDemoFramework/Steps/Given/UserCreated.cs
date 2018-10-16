using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Given
{
    public class UserCreated
    {
        public static async Task<UserLoginModel> CreateUserAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            var result = await userGen.CreateAutoLoginAsync(user.Email, user.Password);
            await userGen.SkipSecondaryEmail(result);
            await userGen.SetMarketingPreferences();
            return user;
        }        
    }
}
