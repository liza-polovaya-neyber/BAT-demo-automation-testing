using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace BATDemoFramework
{
    public class EmployerSearchPage
    {
        IWebDriver driver;

        [FindsBy(How = How.ClassName, Using = "search-box__button")]
        private IWebElement searchEmployerBtn;

        [FindsBy(How = How.Name, Using = "query")]
        private IWebElement inputField;

        [FindsBy(How = How.LinkText, Using = "Select")]
        private IWebElement selectResultBtn;

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

        [FindsBy(How = How.ClassName, Using = "secure-hint-module__root___4OSbU")]
        private IWebElement securityBlock;

        [FindsBy(How = How.ClassName, Using = "search-results-module__title___qbzsB")]
        private IWebElement searchResultsTitle;

        [FindsBy(How = How.ClassName, Using = "control__error")]
        private IWebElement errorMessage;

        [FindsBy(How = How.Name, Using = "phone")]
        private IWebElement phoneNoField;

        [FindsBy(How = How.XPath, Using = "(//button[@type='submit'])[2]")]
        private IWebElement submitPhoneNo;

        [FindsBy(How = How.ClassName, Using = "hint__title")]
        private IWebElement thankYouBlock;

        [FindsBy(How = How.ClassName, Using = "search-item-module__item___1y90n")]
        private IWebElement searchResult;

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

        public void Logout()
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

        public void EnterTextIntoSearchbox(string text)
        {
            inputField.SendKeys(text);
        }

        public void EnterPhoneNumber(string phoneNo)
        {
            phoneNoField.SendKeys(phoneNo.ToString());
        }

        public void ClickToSubmitPhoneNo()
        {
            submitPhoneNo.Click();
        }

        public void SelectEnteredEmployer(string employerName)
        {
            EnterTextIntoSearchbox(employerName);
            ClickOnSearchBtn();
            WaitUntilSearchResultsAppear(Browser.webDriver);
            SelectEmployer();
            ClickOnSelectResultBtn();
            ClickOnContinueBtn();
        }

        public void ClickOnSelectResultBtn()
        {
            selectResultBtn.Click();
        }

        public void SelectEmployer()
        {
            searchResult.Click();
        }

        public void ClickOnRefineSearchLink()
        {
            refineSearchLink.Click();
        }

        public string GetErrorText()
        {
            return errorMessage.Text;
        }

        public bool EmployerNoutFoundBlockIsShown()
        {
            return phoneNoField.Displayed;
        }

        public bool ThankYouBlockIsShown()
        {
            return thankYouBlock.Displayed;
        }

        public bool InputFieldIsShown()
        {
            return inputField.Displayed;
        }

        public bool WaitUntilUrlIsLoaded(IWebDriver driver)
        {
            var employerSearchPage = Browser.WaitUntilUrlIsLoaded(driver, Urls.EmployerSearch, 40);
            return Pages.EmployerSearch.IsAtUrl();
        }

        public bool WaitUntilSecurityBlockIsLoaded(IWebDriver driver)
        {
            var employerSearchPage = Browser.WaitUntilElementIsVisible(driver, By.ClassName("secure-hint-module__root___4OSbU"), 15);
            return securityBlock.Displayed;
        }

        public bool WaitUntilSearchResultsAppear(IWebDriver driver)
        {
            var employerSearchPage = Browser.WaitUntilElementIsVisible(driver, By.ClassName("search-results-module__title___qbzsB"), 5);
            return searchResultsTitle.Displayed;
        }

        public bool WaitUntilPhoneNumberFieldAppears(IWebDriver driver)
        {
            var employerSearchPage = Browser.WaitUntilElementIsVisible(driver, By.Name("phone"), 5);
            return phoneNoField.Displayed;
        }

    }
}