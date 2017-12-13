using System.Runtime.InteropServices;
using BATDemoFramework.Generators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class LoginPage
    {
       [FindsBy(How = How.TagName, Using = "email")]
        private IWebElement emailAddressTextField;

        [FindsBy(How = How.TagName, Using = "password")]
        private IWebElement passwordTextField;

        [FindsBy(How = How.ClassName, Using = "button auth__button login-form-module__button___xa_3Q button-module__button___p4iTs")]
        private IWebElement logInButton;

        [FindsBy(How = How.ClassName, Using = "auth__reg-link")]
        private IWebElement registerButton;

        [FindsBy(How = How.LinkText, Using = "Forgotten your password?")]
        private IWebElement forgottenYourPasswordLink;


        public void GoToJoinPage()
        {
            registerButton.Click();
        }

        public void GoToResetPasswordPage()
        {
            forgottenYourPasswordLink.Click();
        }

        public void LogInAsLastRegisteredUser()
        {
            LogIn(UserGenerator.LastGeneratedUser);
        }

        public void LogInAsLastRegisteredUser(LoginOptions useLastGeneratedPassword)
        {
            var user = new User()
            {
                EmailAddress = UserGenerator.LastGeneratedUser.EmailAddress,
                Password = PasswordGenerator.LastGeneratedPassword
            };

            LogIn(user);
        }

        private void LogIn(User user)
        {
            emailAddressTextField.SendKeys(user.EmailAddress);
            passwordTextField.SendKeys(user.Password);

            logInButton.Click();
        }

        public enum LoginOptions
        {
            UseLastGeneratedPassword
        }


         public bool IsAt()
         {
             return Browser.Title.Contains("/login");
         }
    }
}