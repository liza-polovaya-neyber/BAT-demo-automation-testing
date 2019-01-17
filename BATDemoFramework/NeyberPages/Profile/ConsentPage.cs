using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework.NeyberPages.Profile
{
    public class ConsentPage
    {
        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.ClassName, Using = "form-layout__back")]
        private IWebElement backLink;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.ClassName, Using = "checkbox__label")]
        private IWebElement consentCheckbox;

        [FindsBy(How = How.CssSelector, Using = "button.button.button_blue.button-module__button___2VX0t > span")]
        private IWebElement continueBtn;

        public void GoTo()
        {
            Browser.GoTo("fmr/consent");
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public void ClickOnBackLink()
        {
            backLink.Click();
        }

        public void ClickOnConsentCheckbox()
        {
            consentCheckbox.Click();
        }

        public void ClickOnContinueBtn()
        {
            continueBtn.Click();
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.ConsentPage);
        }

        public bool WaitUntilUrlIsLoaded()
        {
            var consentPage = Browser.WaitUntilUrlIsLoaded(Urls.ConsentPage, 45);
            return Pages.Consent.IsAtUrl();
        }
    }
}
