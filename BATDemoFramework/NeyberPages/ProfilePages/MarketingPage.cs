using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework.NeyberPages.ProfilePages
{
    public class MarketingPage
    {
        [FindsBy(How = How.XPath, Using = "//button")]
        private IWebElement logoutLink;

        [FindsBy(How = How.XPath, Using = "//div[2]/button")]
        private IWebElement logoutBlock;

        [FindsBy(How = How.XPath, Using = "//li/button")]
        private IWebElement logoutOption;

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

        public void LogoutOption()
        {
            logoutBlock.Click();
            logoutOption.Click();
        }

        public void ClickOnSubmitBtn()
        {
            submitBtn.Click();
        }

        public bool WaitUntilSecurityBlockIsLoaded()
        {
            var marketingPage = Browser.WaitUntilElementIsVisible(By.ClassName("secure-hint-module__root___4OSbU"), 13);
            return securityBlock.Displayed;
        }

        public bool WaitUntilMarketingUrlIsLoaded()
        {
            var marketingPage = Browser.WaitUntilUrlIsLoaded(Urls.Marketing, 60);
            return Pages.Marketing.IsAtUrl();
        }
        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.Marketing);
        }


    }
}