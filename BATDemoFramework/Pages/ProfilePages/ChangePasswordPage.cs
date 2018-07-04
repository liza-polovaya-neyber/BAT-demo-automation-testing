using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using BATDemoFramework.Generators;

namespace BATDemoFramework
{
    public class ChangePasswordPage
    {
        [FindsBy(How = How.XPath, Using = "//input")]
        private IWebElement passwordInput;

        [FindsBy(How = How.XPath, Using = "//button/span")]
        private IWebElement setNewPasswordButton;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorPasswordInput;

        [FindsBy(How = How.XPath, Using = "//div[2]")]
        private IWebElement showHidePasswordToggle;

        [FindsBy(How = How.LinkText, Using = "Register")]
        private IWebElement registerLink;

        public void EntersNewPassword(string text)
        {
            passwordInput.SendKeys(text);
        }

        public void ClickToShowHidePassword()
        {
            showHidePasswordToggle.Click();
        }

        public void GoToJoinPage()
        {
            registerLink.Click();
        }
        public bool IsAtTitle()
        {
            return Browser.Title.Contains("Neyber New Password");
        }

        public bool WaitUntilTitleIsShown()
        {
            var changePasswordPage = Browser.WaitUntilPageTitleIsShown("Neyber New Password", 10);
            return Pages.ChangePassword.IsAtTitle();
        }

        public string GetErrorMessage()
        {
            return errorPasswordInput.Text;
        }

        public void SetNewPassword()
        {  
           passwordInput.SendKeys(PasswordGenerator.GeneratePassword());
           setNewPasswordButton.Click();
        }
    }
}