using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class FAQPage
    {
        [FindsBy(How = How.LinkText, Using = "Home")]
        private IWebElement homeLink;

        public void ClickOnHomeLink()
        {
            homeLink.Click();
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