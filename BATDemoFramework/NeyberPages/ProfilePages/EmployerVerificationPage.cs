using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework.NeyberPages.ProfilePages
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

        public bool WaitUntilEmployerVerificationUrlIsLoaded()
        {
            var employerVerificationEmailPage = Browser.WaitUntilUrlIsLoaded(Urls.EmployerVerification, 10);
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

        public bool WaitUntilThankYouBlockIsVisible()
        {
            var employerVerificationPage = Browser.WaitUntilElementIsVisible(By.CssSelector("h4.hint__title"), 5);
            return thankYouBlock.Displayed;

        }
    }
}