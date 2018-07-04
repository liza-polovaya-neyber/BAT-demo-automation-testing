using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using BATDemoFramework.TestDataAccess;

namespace BATDemoFramework
{
    public class AlternativeEmailPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement alternativeEmailField;

        [FindsBy(How = How.LinkText, Using = "Skip this step")]
        private IWebElement skipEmailLink;

        [FindsBy(How = How.XPath, Using = "//form/div[2]/button/span")]
        private IWebElement submitBtn;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorMessage;

        [FindsBy(How = How.ClassName, Using = "secure-hint-module__root___4OSbU")]
        private IWebElement securityBlock;

        [FindsBy(How = How.XPath, Using = "//form/div/p")]
        private IWebElement errorValidation;

        [FindsBy(How = How.CssSelector, Using = "div.global-message-module__container___1hY2-")]
        private IWebElement errorRedBanner;

        public void GoTo()
        {
            Browser.GoTo("join/alternative");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.AlternativeEmail);
        }

        public void EnterEmail(string text)
        {
            alternativeEmailField.SendKeys(text);
        }

        public void EnterEmailFromCsv(string Key)
        {
            var userData = CsvDataAccess.GetTestData(Key);

            alternativeEmailField.SendKeys(userData.EmailPrimary);
        }

        public void ClickOnSubmitBtn()
        {
            submitBtn.Click();
        }

        public void ClickOnSkipLink()
        {
            skipEmailLink.Click();
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public string GetErrorMessage()
        {
            return errorValidation.Text;
        }

        public bool GetErrorBannerMessage()
        {
            return errorRedBanner.Text.Contains("This email is already registered. Please provide an alternative.");
        }

        public bool WaitUntilSecurityBlockIsLoaded(IWebDriver driver)
        {
            var alternativeEmailPage = Browser.WaitUntilElementIsVisible(driver, By.ClassName("secure-hint-module__root___4OSbU"), 13);
            return securityBlock.IsDisplayed();
        }

        public bool WaitUntilAlternativeUrlIsLoaded(IWebDriver driver)
        {
            var alternativeEmailPage = Browser.WaitUntilUrlIsLoaded(driver, Urls.AlternativeEmail, 10);
            return Pages.AlternativeEmail.IsAtUrl();
        }

        public bool WaitUntilRedBannerIsShown(IWebDriver driver)
        {
            var alternativeEmailPage = Browser.WaitUntilElementIsVisible(driver, By.CssSelector("div.global-message-module__container___1hY2-"), 7);
            return errorRedBanner.IsDisplayed();
        }
    }
}