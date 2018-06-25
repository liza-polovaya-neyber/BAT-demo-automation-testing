using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class WorkEmailPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement workEmailField;

        [FindsBy(How = How.XPath, Using = "//form/div[2]/button/span")]
        private IWebElement submitBtn;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorMessage;

        [FindsBy(How = How.LinkText, Using = "Neyber")]
        private IWebElement logoNeyber;

        public void Submit()
        {
            submitBtn.Click();
        }
        public void EnterEmail(string text)
        {
            workEmailField.SendKeys(text);
        }

        public bool WaitUntilWorkEmailUrlIsLoaded(IWebDriver driver)
        {
            var workEmailPage = Browser.WaitUntilUrlIsLoaded(driver, Urls.WorkEmail, 10);
            return Pages.WorkEmail.IsAtUrl();
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.WorkEmail);
        }

        public string GetErrorText()
        {
            return errorMessage.Text;
        }

        public void ClickOnLogo()
        {
            logoNeyber.Click();
        }

    }
}