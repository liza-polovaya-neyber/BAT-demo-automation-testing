﻿using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class SSOAccountConfirmPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailField;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement passwordField;

        [FindsBy(How = How.XPath, Using = "//button/span")]
        private IWebElement loginBtn;

        [FindsBy(How = How.LinkText, Using = "Continue")]
        private IWebElement continueBtn;


        public void ClickToContinue()
        {
            continueBtn.Click();
        }

        public void LogIn(SSOUser user, User newUser)
        {
            emailField.SendKeys(user.Email);
            passwordField.SendKeys(newUser.Password);
            loginBtn.Click();
        }

        public bool WaitUntilUrlIsLoaded()
        {
            Browser.WaitUntilUrlIsLoaded(Urls.SSOAccountConfirm, 60);
            return Pages.SSOAccountConfirm.IsAtUrl();
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.SSOAccountConfirm);
        }
    }
}