using OpenQA.Selenium;

namespace BATDemoFramework
{
    public class CookiePolicyPage
    {
        public bool IsAt()
        {
            return Browser.Title.Contains("/policies#cookie");
        }

        public bool IsAtUrl()
        {
            Browser.SwitchTabs(1);
            return Browser.Url.Contains(Urls.CookiePolicy);
        }
    }
}