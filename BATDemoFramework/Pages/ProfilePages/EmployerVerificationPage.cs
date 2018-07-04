using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace BATDemoFramework
{
    public class EmployerVerificationPage
    {
        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.CssSelector, Using = "label.checkbox__label")]
        private IWebElement consentCheckbox;

        [FindsBy(How = How.XPath, Using = "//form/div[2]/button/span")]
        private IWebElement submitBtn;

        [FindsBy(How = How.CssSelector, Using = "h4.hint__title")]
        private IWebElement thankYouBlock;

        public void CheckConsentCheckbox()
        {
            consentCheckbox.Click();
        }

        public void Submit()
        {
            submitBtn.Click();
        }

        public bool WaitUntilEmployerVerificationUrlIsLoaded(IWebDriver driver)
        {
            var employerVerificationEmailPage = Browser.WaitUntilUrlIsLoaded(driver, Urls.EmployerVerification, 10);
            return Pages.EmployerVerification.IsAtUrl();
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.EmployerVerification);
        }
        public bool ThankYouBlockIsShown()
        {
            return thankYouBlock.Displayed;
        }

        public bool WaitUntilThankYouBlockIsVisible(IWebDriver driver)
        {
            var employerVerificationPage = Browser.WaitUntilElementIsVisible(driver, By.CssSelector("h4.hint__title"), 5);
            return thankYouBlock.Displayed;

        }
    }
}