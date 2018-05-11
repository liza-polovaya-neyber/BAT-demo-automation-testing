using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class AlternativeEmailPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement alternativeEmailField;

        [FindsBy(How = How.LinkText, Using = "Skip this step")]
        private IWebElement skipEmailLink;

        [FindsBy(How = How.XPath, Using = "//form/div[2]/button/span")]
        private IWebElement submitBtn;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorMessage;

        [FindsBy(How = How.ClassName, Using = "secure-hint-module__root___4OSbU")]
        private IWebElement securityBlock;

        public void GoTo()
        {
            Browser.GoTo("join/alternative");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.AlternativeEmail);
        }

        public void EnterTextIntoEmailField(string text)
        {
            alternativeEmailField.SendKeys(text);
        }

        public void ClickOnSubmitBtn()
        {
            submitBtn.Click();
        }

        public void ClickOnSkipLink()
        {
            skipEmailLink.Click();
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public bool WaitUntilSecurityBlockIsLoaded(IWebDriver driver)
        {
            var alternativeEmailPage = Browser.WaitUntilElementIsVisible(driver, By.ClassName("secure-hint-module__root___4OSbU"), 13);
            return securityBlock.IsDisplayed();
        }
    }
}