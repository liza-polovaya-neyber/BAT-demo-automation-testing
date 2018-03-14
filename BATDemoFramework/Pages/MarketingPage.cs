using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class MarketingPage
    {
        [FindsBy(How = How.ClassName, Using = "login-logout-module__logout___1lxME")]
        private IWebElement logoutLink;

        [FindsBy(How = How.Id, Using = "sms")]
        private IWebElement smsOption;

        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement emailOption;

        [FindsBy(How = How.Id, Using = "post")]
        private IWebElement postOption;

        [FindsBy(How = How.Id, Using = "Telephone")]
        private IWebElement phoneOption;

        [FindsBy(How = How.ClassName, Using = "button button email-form-module__button___3vdUD button-module__button___2VX0t")]
        private IWebElement submitBtn;

        public void GoTo()
        {
            Browser.GoTo("join/marketing");
        }

        public void IsAtUrl()
        {
            Browser.Url.Contains(Urls.Marketing);
        }


    }
}