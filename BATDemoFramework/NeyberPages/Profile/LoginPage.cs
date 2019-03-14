using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using BATDemoFramework.TestDataAccess;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace BATDemoFramework.NeyberPages.Profile
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

        [FindsBy(How = How.CssSelector, Using = "p.hint.hint_alert.np-i")]
        private IWebElement errorMessage;

        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]")]
        private IWebElement showHidePasswordToggle;

        [FindsBy(How = How.CssSelector, Using = "div.global-message-module__container___1hY2-")]
        private IWebElement redBanner;
  
        [FindsBy(How = How.XPath, Using = "//section/div")]
        private IWebElement greenVerificationBanner;

        [FindsBy(How = How.XPath, Using = "//div[2]/div/p")]
        private IWebElement errorWrongPassword;

        private IWebDriver driver;


        public void GoTo()
        {
            Browser.GoTo("login");
        }

        public void GoToJoinPage()
        {
            registerButton.Click();
        }

        public void GoToResetPasswordPage()
        {
            forgottenYourPasswordLink.Click();
        }

        public void EnterPassword(string text)
        {
            passwordField.SendKeys(text);
        }

        public void ClickToShowHidePassword()
        {
            showHidePasswordToggle.Click();
        }

        //Login by using credentials stored in CSV file
        public void LogInFromCsv(string Key)
        {
            var userData = CsvDataAccess.GetTestData(Key);

            emailAddressField.Click();
            emailAddressField.SendKeys(userData.EmailPrimary);
            passwordField.Click();
            passwordField.SendKeys(userData.Password);

            loginButton.Click();
        }

        public void LogIn(User user)
        {
            emailAddressField.Click();
            emailAddressField.SendKeys(user.EmailAddress);
            passwordField.Click();
            passwordField.SendKeys(user.Password);

            loginButton.Click();
        }

        public void LogInQuickUser(UserLoginModel user)
        {
            System.Threading.Thread.Sleep(5000);
            emailAddressField.Click();
            emailAddressField.SendKeys(user.Email);
            passwordField.Click();
            passwordField.SendKeys(user.Password);
            
            loginButton.Click();
        }

        public void LogInBySSOUserPrimaryEmail(SSOUser user, User newUser)
        {
            emailAddressField.Click();
            emailAddressField.SendKeys(user.Email);
            passwordField.Click();
            passwordField.SendKeys(newUser.Password);
            
            loginButton.Click();
        }

        public void LogInBySSOUserAlternativeEmail(User newUser)
        {
            emailAddressField.Click();
            emailAddressField.SendKeys(newUser.EmailAddress);
            passwordField.Click();
            passwordField.SendKeys(newUser.Password);

            loginButton.Click();
        }

        public void LoginByRandomUser()
        {
            var user = new UserGenerator().GetNewUser();

            emailAddressField.SendKeys(user.EmailAddress);
            passwordField.SendKeys(user.Password);

            loginButton.Click();
        }

        public void LoginByUserWithWrongPassword()
        {
            var user = new UserGenerator().GetNewUser();

            emailAddressField.SendKeys(user.EmailAddress);
            passwordField.SendKeys(PasswordGenerator.GeneratePassword());

            loginButton.Click();
        }

        public string GetErrorText()
        {
            return errorMessage.Text;
        }

        public string GetErrorPasswordText()
        {
            return errorWrongPassword.Text;
        }

        public string GetErrorBannerText()
        {
            return redBanner.Text;
        }

        public bool GetGreenBannerText()
        {
            return greenVerificationBanner.Text.Contains("Your email has been verified! Please log in to continue");
        }

        public bool WaitUntilErrorBlockIsShown()
        {
            var errorBlock = Browser.WaitUntilElementIsPresent(By.CssSelector("p.hint.hint_alert.np-i"), 80);
            return errorBlock.IsDisplayed();
        }

        public bool WaitUntilLoginUrlIsLoaded()
        {
            var loginPage = Browser.WaitUntilUrlIsLoaded(Urls.LoginPage, 40);
            return Pages.Login.IsAtUrl();
        }

        public bool IsAtTitle()
        {
            return Browser.Title.Contains("/login");
        }

        public bool IsAtUrl()        {
            return Browser.Url.Contains(Urls.LoginPage);
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

        public enum LoginOptions
        {
            UseLastGeneratedPassword
        }

        //public void LogIn(User user)
        //{
        //    emailAddressField.SendKeys(user.EmailAddress);
        //    passwordField.SendKeys(user.Password);

        //    loginButton.Click();
        //}
    }
}