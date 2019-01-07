using BATDemoFramework.Models;

namespace BATDemoFramework.Steps.When
{
    public class LoginUser
    {
        public static void UserLogin(UserLoginModel user)
        {
            NeyberPages.Pages.Login.GoTo();
            NeyberPages.Pages.Login.LogInQuickUser(user);
            NeyberPages.Pages.Home.WaitUntilHomeUrlIsLoaded();
        }       
    }
}
