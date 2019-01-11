using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Given
{
    public class UserCreatedAndAlternativeEmailSkipped
    {
        public static async Task<UserLoginModel> CreateUserAndSkipAlternativeEmailAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            var accessToken = await userGen.CreateAutoLoginAsync(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            await userGen.SkipSecondaryEmail();
            return user;
        }
    }
}