using BATDemoFramework.Generators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using BATDemoFramework;
using BATDemoFramework.TestDataAccess;

namespace BATDemoFramework
{
    public class AboutMePage
    {
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement titleDropdown;

        [FindsBy(How = How.Name, Using = "first_name")]
        private IWebElement firstNameTextField;

        [FindsBy(How = How.Name, Using = "last_name")]
        private IWebElement lastNameTextField;

        [FindsBy(How = How.Name, Using = "day")]
        private IWebElement dayOfBirthDropdown;

        [FindsBy(How = How.Name, Using = "month")]
        private IWebElement monthOfBirthDropdown;

        [FindsBy(How = How.Name, Using = "year")]
        private IWebElement yearOfBirthDropdown;

        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailAddressField;

        [FindsBy(How = How.Name, Using = "verify_email")]
        private IWebElement confirmEmailAddressField;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement passwordField;

        [FindsBy(How = How.CssSelector, Using = "button.button.button-module__button___2VX0t > span")]
        private IWebElement submitButton;

        [FindsBy(How = How.Id, Using = "terms_accepted")]
        private IWebElement checkboxTermsAccepted;

        [FindsBy(How = How.Id, Using = "opt_out_email")]
        private IWebElement checkboxOptOutEmail;

        [FindsBy(How = How.ClassName, Using = "form-layout__back")]
        private IWebElement backLink;

        [FindsBy(How = How.ClassName, Using = "already-customer-login-module__login-link___2DwCr")]
        private IWebElement loginLink;

        public void GoTo()
        {
            Browser.GoTo("join/about-me");
        }

        public void GotoJoinPage()
        {
            backLink.Click();
        }

        public void GoToLoginPage()
        {
            loginLink.Click();
        }

        public void SelectTitle()
        {
            var selectElement = new SelectElement(titleDropdown);
            selectElement.SelectByValue("Miss");

        } 

        public void SelectDayOfBirth()
        {
            var selectElement = new SelectElement(dayOfBirthDropdown);
            selectElement.SelectByValue("12");
        }

        public void SelectMonthOfBirth()
        {
            var selectElement = new SelectElement(monthOfBirthDropdown);
            selectElement.SelectByValue("7");
        }

        public void SelectYearOfBirth()
        {
            var selectElement = new SelectElement(yearOfBirthDropdown);
            selectElement.SelectByValue("1989");
        }

        public void CheckboxTermsAcceptedChecked()
        {
            checkboxTermsAccepted.Click();
        }

        public void CheckboxOptOutEmailsChecked()
        {
            checkboxOptOutEmail.Click();
        }

        public bool SubmitBtnIsEnabled()
        {
            return submitButton.IsEnabled();
        }

        public void RegisterNewUser(string Key)
        {
            //var userGenerator = new UserGenerator();
            //var user = userGenerator.GetNewUser();
            var userData = CsvDataAccess.GetTestData(Key);

            SelectTitle();
            firstNameTextField.SendKeys(userData.FirstName);
            lastNameTextField.SendKeys(userData.LastName);
            SelectDayOfBirth();
            SelectMonthOfBirth();
            SelectYearOfBirth();
            emailAddressField.SendKeys(userData.Email);
            confirmEmailAddressField.SendKeys(userData.Email);
            passwordField.SendKeys(userData.Password);
            CheckboxTermsAcceptedChecked();
            CheckboxOptOutEmailsChecked();

            submitButton.Click();
        }

        public void IsAtUrl()
        {
            Browser.Url.Contains(Urls.AboutMePage);
        }
    }
}
