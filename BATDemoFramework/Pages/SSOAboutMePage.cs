using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class SSOAboutMePage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailField;

        [FindsBy(How = How.Name, Using = "verify_email")]
        private IWebElement emailVerifyField;

        [FindsBy(How = How.Name, Using = "mobile_number")]
        private IWebElement mobileNumberField;

        [FindsBy(How = How.CssSelector, Using = "div.control.control_active > input.control__input")]
        private IWebElement passwordField;

        [FindsBy(How = How.CssSelector, Using = "label.checkbox__label")]
        private IWebElement policyTickbox;

        [FindsBy(How = How.XPath, Using = "//button/span")]
        private IWebElement submitBtn;


        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.SSOAboutMePage);
        }

    }
}