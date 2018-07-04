using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace BATDemoFramework
{
    public class MarketingPage
    {
        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.XPath, Using = "//label/span")]
        private IWebElement smsOption;

        [FindsBy(How = How.XPath, Using = "//div[2]/label")]
        private IWebElement emailOption;

        [FindsBy(How = How.XPath, Using = "//div[3]/label")]
        private IWebElement postOption;

        [FindsBy(How = How.XPath, Using = "//div[4]/label")]
        private IWebElement phoneOption;

        [FindsBy(How = How.XPath, Using = "//form/button/span")]
        private IWebElement submitBtn;

        [FindsBy(How = How.ClassName, Using = "secure-hint-module__root___4OSbU")]
        private IWebElement securityBlock;

        public void GoTo()
        {
            Browser.GoTo("join/marketing");
        }
       
        public void ChooseSMSOption()
        {
           smsOption.Click();
        }

        public void ChooseEmailOption()
        {
            emailOption.Click();
        }

        public void ChoosePostOption()
        {
            postOption.Click();
        }

        public void ChoosePhoneOption()
        {
            phoneOption.Click();
        }

        public void Logout()
        {
            logoutLink.Click();
        }
        public void ClickOnSubmitBtn()
        {
            submitBtn.Click();
        }

        public bool WaitUntilSecurityBlockIsLoaded(IWebDriver driver)
        {
            var marketingPage = Browser.WaitUntilElementIsVisible(driver, By.ClassName("secure-hint-module__root___4OSbU"), 13);
            return securityBlock.Displayed;
        }

        public bool WaitUntilMarketingUrlIsLoaded(IWebDriver driver)
        {
            var marketingPage = Browser.WaitUntilUrlIsLoaded(driver, Urls.Marketing, 25);
            return Pages.Marketing.IsAtUrl();
        }
        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.Marketing);
        }


    }
}