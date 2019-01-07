using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework.Pages.ProfilePages
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

        [FindsBy(How = How.CssSelector, Using = "button.employer-selected-module__refine___3pyDx")]
        private IWebElement refineSearchLink;

        [FindsBy(How = How.CssSelector, Using = "button.button.button_blue.button-module__button___2VX0t > span")]
        private IWebElement continueBtn;

        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.XPath, Using = "//li/button")]
        private IWebElement logoutLink;

        [FindsBy(How = How.ClassName, Using = "login-logout-module__defaultavatar___1BEAZ")]
        private IWebElement userAvatar;

        [FindsBy(How = How.ClassName, Using = "login-logout-module__menutoggle___31VbT")]
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

        [FindsBy(How = How.CssSelector, Using = "#root > div > div > div > section > section > p.hint.hint_warning.np-i")]
        private IWebElement warningBlock;

        [FindsBy(How = How.CssSelector, Using = "button.button.button_square.button_white.global-message-module__button___23Qpy")]
        private IWebElement closeButton;

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
            WaitUntilSearchResultsAppear();
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

        public bool WarningBlockIsShown()
        {
            return warningBlock.Displayed;
        }

        public bool WaitUntilUrlIsLoaded()
        {
            Browser.WaitUntilUrlIsLoaded(Urls.EmployerSearch, 60);
            return NeyberPages.Pages.EmployerSearch.IsAtUrl();
        }

        public bool WaitUntilSecurityBlockIsLoaded()
        {
            Browser.WaitUntilElementIsVisible(By.ClassName("secure-hint-module__root___4OSbU"), 15);
            return securityBlock.Displayed;
        }

        public bool WaitUntilSearchResultsAppear()
        {
            Browser.WaitUntilElementIsVisible(By.ClassName("search-results-module__title___qbzsB"), 5);
            return searchResultsTitle.Displayed;
        }

        public bool WaitUntilPhoneNumberFieldAppears()
        {
            Browser.WaitUntilElementIsVisible(By.Name("phone"), 5);
            return phoneNoField.Displayed;
        }

        public bool WaitUntilWarningBlockIsShown()
        {
            Browser.WaitUntilElementIsVisible(By.ClassName("hint hint_warning np-i"), 5);
            return warningBlock.Displayed;
        }

        public void CloseConfirmationBanner()
        {
            closeButton.Click();
        }

    }
}