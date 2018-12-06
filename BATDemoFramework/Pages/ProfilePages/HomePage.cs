using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace BATDemoFramework
{
    public class HomePage
    {
        [FindsBy(How = How.ClassName, Using = "login-logout-module__logout___C4bdQ")]
        private IWebElement logoutLink;

        [FindsBy(How = How.CssSelector, Using = "span.login-logout-module__defaultavatar___3VRWc")]
        private IWebElement userAvatar;

        [FindsBy(How = How.CssSelector, Using = "span.login-logout-module__defaultavatar___3VRWc")]
        private IWebElement loanTile;

        [FindsBy(How = How.XPath, Using = "//section[2]/div/button/span")]
        private IWebElement finWellTile;

        private IWebDriver driver;

        public bool UserAvatarIsDisplayed()
        {
            return userAvatar.IsDisplayed();
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.HomePage);
        }

        public bool WaitUntilHomeUrlIsLoaded()
        {
            var homePage = Browser.WaitUntilUrlIsLoaded(Urls.HomePage, 45);
            return Pages.Home.IsAtUrl();
        }

        public bool AvatarIsDisplayed()
        {
            var avatarBlock = Browser.WaitUntilElementIsClickable(userAvatar, 11);
            return avatarBlock.Displayed;
        }

    }
}