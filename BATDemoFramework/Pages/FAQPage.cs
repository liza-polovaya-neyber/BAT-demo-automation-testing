using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class FAQPage
    {
        [FindsBy(How = How.LinkText, Using = "Personal loans")]
        private IWebElement personalLoansLink;

        public void ClickOnPersonalLoans()
        {
            personalLoansLink.Click();
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.FAQPage);
        }

        public bool WaitUntilUrlIsLoaded()
        {
            var faqPage = Browser.WaitUntilUrlIsLoaded(Urls.FAQPage, 20);
            return Pages.FAQ.IsAtUrl();
        }
    }
}