using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class ExpiredLinkPage
    {
        [FindsBy(How = How.LinkText, Using = "Return to Neyber")]
        private IWebElement returnToNeyberBtn;

        [FindsBy(How = How.CssSelector, Using = "a.logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.LinkText, Using = "Our Terms")]
        private IWebElement ourTermsLink;

        [FindsBy(How = How.LinkText, Using = "Privacy Policy")]
        private IWebElement privacyPolicyLink;

        [FindsBy(How = How.LinkText, Using = "Cookie Policy")]
        private IWebElement cookiePolicyLink;

        [FindsBy(How = How.LinkText, Using = "Complaints Policy")]
        private IWebElement complaintsPolicyLink;

        [FindsBy(How = How.LinkText, Using = "Some legal bits we need to tell you")]
        private IWebElement someLegalBitsMenu;

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.ExpiredLink);
        }

        public bool WaitUntilPageIsLoaded(IWebDriver driver)
        {
            var expiredLinkPage = Browser.WaitUntilUrlIsLoaded(Urls.ExpiredLink, 25);
            return Pages.ExpiredLink.IsAtUrl();
        }

        public void ClickOnLogo()
        {
            logoNeyber.Click();
        }

        public void ClickOnReturnBtn()
        {
            returnToNeyberBtn.Click();
        }


    }
}