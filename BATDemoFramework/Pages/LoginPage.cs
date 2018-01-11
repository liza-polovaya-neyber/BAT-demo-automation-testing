using System.Runtime.InteropServices;
using BATDemoFramework.Generators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using BATDemoFramework.TestDataAccess;
using System;

namespace BATDemoFramework
{
    public class LoginPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailAddressField;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement passwordField;

        [FindsBy(How = How.CssSelector, Using = "#root > div > div > div > form > button")]
        private IWebElement loginButton;

        [FindsBy(How = How.ClassName, Using = "auth__reg-link")]
        private IWebElement registerButton;

        [FindsBy(How = How.LinkText, Using = "Forgotten your password?")]
        private IWebElement forgottenYourPasswordLink;

        [FindsBy(How = How.ClassName, Using = "auth__title")]
        private IWebElement loginPageHello;

        [FindsBy(How = How.ClassName, Using = "hint hint_alert np-i login-form-module__error___3bEGA")]
        private IWebElement errorInvalidCredentials;

    
        public void GoToLoginPage()
        {
            Browser.GoTo("/login");
        }

        public void GoToJoinPage()
        {
            registerButton.Click();
        }

        public void GoToResetPasswordPage()
        {
            forgottenYourPasswordLink.Click();
        }

        public void ClickOnEmailAddressField()
        {
            emailAddressField.Click();
        }

        public void ClickOnPasswordField()
        {
            passwordField.Click();
        }


        public void LogIn(string testName)
        {
            var userData = CsvDataAccess.GetTestData(testName);

            ClickOnEmailAddressField();
            emailAddressField.SendKeys(userData.Email);
            ClickOnPasswordField();
            passwordField.SendKeys(userData.Password);

            loginButton.Click();

        }

        public bool IsAt()
        {
            return Browser.Title.Contains("/login");
        }


        //public void LogInAsLastRegisteredUser()
        //{
        //    LogIn(UserGenerator.LastGeneratedUser);
        //}

        //public void LogInAsLastRegisteredUser(LoginOptions useLastGeneratedPassword)
        //{
        //    var user = new User()
        //    {
        //        EmailAddress = UserGenerator.LastGeneratedUser.EmailAddress,
        //        Password = PasswordGenerator.LastGeneratedPassword
        //    };

        //    LogIn(user);
        //}

        //public void LogIn(User user)
        //{
        //    emailAddressField.SendKeys(user.EmailAddress);
        //    passwordField.SendKeys(user.Password);

        //    logInButton.Click();
        // }

        //public enum LoginOptions
        //{
        //    UseLastGeneratedPassword
        //}
    }
}