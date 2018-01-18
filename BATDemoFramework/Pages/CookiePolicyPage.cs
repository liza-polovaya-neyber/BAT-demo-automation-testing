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
            return Browser.Url.Contains("/policies#cookie");
        }
    }
}