using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class EmployerSearchPage
    {
        [FindsBy(How = How.ClassName, Using = "search-form-module__button-text___1Xrj1")]
        private IWebElement searchEmployerBtn;

        [FindsBy(How = How.Name, Using = "query")]
        private IWebElement inputField;

        [FindsBy(How = How.LinkText, Using = "Select")]
        private IWebElement selectResultBtn;

        [FindsBy(How = How.LinkText, Using = "Bupa Care Services")]
        private IWebElement selectBupa;

        [FindsBy(How = How.LinkText, Using = "Click here to refine your search")]
        private IWebElement refineSearchLink;

        [FindsBy(How = How.CssSelector, Using = "button.button.button_blue.button-module__button___2VX0t > span")]
        private IWebElement continueBtn;

        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.ClassName, Using = "login-logout-module__defaultavatar___3VRWc")]
        private IWebElement userAvatar;

        [FindsBy(How = How.ClassName, Using = "login-logout-module__profilenav___diW9P")]
        private IWebElement logoutModule;


        public void GoTo()
        {
            Browser.GoTo("join/search");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.EmployerSearch);
        }

        public void ClickOnLogo()
        {
            logoNeyber.Click();
        }

        public void ClickOnLogout()
        {
            logoutLink.Click();
        }

        public void ClickOnSearchBtn()
        {
            searchEmployerBtn.Click();
        }

        public void ClickOnContinueBtn()
        {
            continueBtn.Click();
        }

        public void SelectAnEmployer()
        {
            inputField.SendKeys("Bupa"); //integration with ares needed to be able to pull out the actual tenants names
            ClickOnSearchBtn();
            selectBupa.Click();
            selectResultBtn.Click();
            ClickOnContinueBtn();
        }

    }
}