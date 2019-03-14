using BATDemoFramework.TestDataAccess;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace BATDemoFramework.NeyberPages
{
    public class AdditionalDetailsPage
    {
        [FindsBy(How = How.XPath, Using = "//label/span")]
        private IWebElement smsOption;

        [FindsBy(How = How.XPath, Using = "//div/div[2]/label")]
        private IWebElement emailOption;

        [FindsBy(How = How.XPath, Using = "//div[3]/label")]
        private IWebElement postOption;

        [FindsBy(How = How.XPath, Using = "//div[4]/label")]
        private IWebElement phoneOption;

        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement alternateEmailField;

        [FindsBy(How = How.Name, Using = "customerFeedback")]
        private IWebElement howYouHeardAboutUsDD;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement submitBtn;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorMessage;

        [FindsBy(How = How.XPath, Using = "//form/div/p")]
        private IWebElement errorValidation;

        [FindsBy(How = How.XPath, Using = "//button")]
        private IWebElement logoutLink;

        [FindsBy(How = How.ClassName, Using = "secure-hint-module__root___4OSbU")]
        private IWebElement securityBlock;

        [FindsBy(How = How.CssSelector, Using = "div.global-message-module__container___1hY2-")]
        private IWebElement errorRedBanner;

        public void GoTo()
        {
            Browser.GoTo("join/additional-details");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.AdditionalDetails);
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

        public void EnterEmail(string text)
        {
            alternateEmailField.SendKeys(text);
        }

        public int GetFeedbackOptionsNumber()
        {
            var selectElement = new SelectElement(howYouHeardAboutUsDD);
            return selectElement.Options.Count;
        }

        public void EnterEmailFromCsv(string Key)
        {
            var userData = CsvDataAccess.GetTestData(Key);

            alternateEmailField.SendKeys(userData.EmailPrimary);
        }

        public void ClickOnSubmitBtn()
        {
            submitBtn.Click();
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public string GetErrorMessage()
        {
            return errorValidation.Text;
        }

        public bool GetErrorBannerMessage()
        {
            return errorRedBanner.Text.Contains("This email is already registered. Please provide an alternative.");
        }

        public void SelectHowYouHeardAboutUs(string option)
        {
            var selectElement = new SelectElement(howYouHeardAboutUsDD);
            selectElement.SelectByValue(option);
        }

        public bool WaitUntilSecurityBlockIsLoaded()
        {
           Browser.WaitUntilElementIsVisible(By.ClassName("secure-hint-module__root___4OSbU"), 13);
            return securityBlock.IsDisplayed();
        }

        public bool WaitUntilUrlIsLoaded()
        {
           Browser.WaitUntilUrlIsLoaded(Urls.AdditionalDetails, 45);
            return Pages.AdditionalDetails.IsAtUrl();
        }

        public bool WaitUntilRedBannerIsShown()
        {
            Browser.WaitUntilElementIsVisible(By.CssSelector("div.global-message-module__container___1hY2-"), 7);
            return errorRedBanner.IsDisplayed();
        }
    }
}