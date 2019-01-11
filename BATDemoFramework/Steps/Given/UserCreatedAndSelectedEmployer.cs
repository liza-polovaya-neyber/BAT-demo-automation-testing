using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Given
{
    public class UserCreatedAndSelectedEmployer
    {
        public static async Task<UserLoginModel> CreateUserAndSelectEmployerAsync()
        {
            var userGen = new UserGenerator();
            var user = await userGen.CreateDefaultUser();
            var accessToken = await userGen.CreateAutoLoginAsync(user.Email, user.Password);
            userGen.UpdateAuthenticationHeader(accessToken);
            await userGen.SetTenant();
            return user;
        }
    }
}
