using System.Runtime.InteropServices;
using BATDemoFramework.Generators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using BATDemoFramework.TestDataAccess;
using System;
using OpenQA.Selenium.Support.UI;

namespace BATDemoFramework
{
    public class LoginPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailAddressField;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement passwordField;

        [FindsBy(How = How.CssSelector, Using = "button.button.auth__button.login-form-module__button___1OboL.button-module__button___2VX0t")]
        private IWebElement loginButton;

        [FindsBy(How = How.ClassName, Using = "auth__reg-link")]
        private IWebElement registerButton;

        [FindsBy(How = How.LinkText, Using = "Forgotten your password?")]
        private IWebElement forgottenYourPasswordLink;

        [FindsBy(How = How.ClassName, Using = "auth__title")]
        private IWebElement loginPageHello;

        [FindsBy(How = How.XPath, Using = "//p[2]")]
        private IWebElement errorInvalidCredentials;

        private IWebDriver driver;


        //Browser is navigated to Login Page
        public void GoTo()
        {
            Browser.GoTo("login");
        }

        //Go from Login page => Join page
        public void GoToJoinPage()
        {
            registerButton.Click();
        }

        //Go from Login page => Reset Password page
        public void GoToResetPasswordPage()
        {
            forgottenYourPasswordLink.Click();
        }


        //Login by using credentials stored in CSV file
        public void LogIn(string Key)
        {
            var userData = CsvDataAccess.GetTestData(Key);

            emailAddressField.Click();
            emailAddressField.SendKeys(userData.EmailPrimary);
            passwordField.Click();
            passwordField.SendKeys(userData.Password);

            loginButton.Click();
        }

        //Get text property from webElement
        public string GetErrorText()
        {
            return errorInvalidCredentials.Text;
        }


        public bool ErrorBlockIsShown(IWebDriver driver)
        {
            bool result;
            var errorBlock = Browser.WaitUntilElementIsPresent(driver, By.XPath("//p[2]"), 10);
            result = errorBlock.Displayed;
            return result;


        }

        //Verify the page title (url)
        public bool IsAt()
        {
            return Browser.Title.Contains("/login");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.LoginPage);
        }

        public string GetTitle()
        {
            return Browser.Title;
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