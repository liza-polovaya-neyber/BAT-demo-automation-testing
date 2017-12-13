using BATDemoFramework.Generators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

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
        private IWebElement emailAddressTextField;

        [FindsBy(How = How.Name, Using = "verify_email")]
        private IWebElement confirmEmailAddressTextField;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement passwordTextField;

        [FindsBy(How = How.ClassName, Using = "button button-module__button___p4iTs")]
        private IWebElement submitButton;

        [FindsBy(How = How.Id, Using = "terms_accepted")]
        private IWebElement checkboxTermsAccepted;

        [FindsBy(How = How.Id, Using = "opt_out_email")]
        private IWebElement checkboxOptOutEmail;

        [FindsBy(How = How.ClassName, Using = "form-layout__back")]
        private IWebElement backLink;

        [FindsBy(How = How.ClassName, Using = "already-customer-login-module__login-link___2DwCr")]
        private IWebElement loginLink;



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
            selectElement.SelectByValue("August");
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

        public void RegisterNewUser()
        {
            var userGenerator = new UserGenerator();
            var user = userGenerator.Generate();

            SelectTitle();
            firstNameTextField.SendKeys("First Name");
            lastNameTextField.SendKeys("LastName");
            SelectDayOfBirth();
            SelectMonthOfBirth();
            SelectYearOfBirth();
            emailAddressTextField.SendKeys(user.EmailAddress);
            confirmEmailAddressTextField.SendKeys(user.EmailAddress);
            passwordTextField.SendKeys(user.Password);
            CheckboxTermsAcceptedChecked();
            CheckboxOptOutEmailsChecked();

            submitButton.Click();
        }
    }
}
