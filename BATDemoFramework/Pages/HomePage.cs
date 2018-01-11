using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class HomePage
    {
        [FindsBy(How = How.ClassName, Using = "login-logout-module__logout___1lxME")]
        private IWebElement logoutLink;

        public bool IsAt()
        {
            return Browser.Title.Contains("https://hellotest1.neyber.co.uk/home");
        }
    }
}