using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace BATDemoFramework
{
    public class HomePage
    {
        [FindsBy(How = How.ClassName, Using = "login-logout-module__logout___1lxME")]
        private IWebElement logoutLink;

        [FindsBy(How = How.CssSelector, Using = "span.login-logout-module__defaultavatar___3VRWc")]
        private IWebElement userAvatar;

        public bool UserAvatarIsDisplayed()
        {
            bool result;
            try
            {
                result = userAvatar.IsDisplayed();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool IsAt()
        {
            return Browser.Title.Contains("https://hellotest1.neyber.co.uk/home");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.HomePage);
        }
    }
}