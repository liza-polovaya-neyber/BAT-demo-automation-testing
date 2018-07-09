using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class SSOAccountConfirPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailField;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement passwordField;

        [FindsBy(How = How.CssSelector, Using = "//button/span")]
        private IWebElement loginBtn;

        [FindsBy(How = How.LinkText, Using = "Continue")]
        private IWebElement continueBtn;


        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.SSOAccountConfirm);
        }
    }
}