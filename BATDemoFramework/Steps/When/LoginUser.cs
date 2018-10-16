using BATDemoFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.When
{
    public class LoginUser
    {
        public static void UserLogin(UserLoginModel user)
        {
            Pages.Login.GoTo();
            Pages.Login.LogInQuickUser(user);
            Pages.Home.WaitUntilHomeUrlIsLoaded();
        }       
    }
}
