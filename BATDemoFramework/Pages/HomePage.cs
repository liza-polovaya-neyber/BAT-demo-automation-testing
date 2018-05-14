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

        public bool WaitUntilHomeUrlIsLoaded(IWebDriver driver)
        {
            var homePage = Browser.WaitUntilUrlIsLoaded(driver, Urls.HomePage, 10);
            return Pages.Home.IsAtUrl();
        }

        public bool AvatarIsDisplayed(IWebDriver driver)
        {
            var avatarBlock = Browser.WaitUntilElementIsClickable(driver, userAvatar, 11);
            return avatarBlock.Displayed;
        }

    }
}