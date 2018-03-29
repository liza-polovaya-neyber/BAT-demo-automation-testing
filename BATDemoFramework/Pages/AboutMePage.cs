using BATDemoFramework.Generators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using BATDemoFramework;
using BATDemoFramework.TestDataAccess;
using System;

namespace BATDemoFramework
{
    public class AboutMePage
    {
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement titleDD;

        [FindsBy(How = How.Name, Using = "first_name")]
        private IWebElement firstNameTextField;

        [FindsBy(How = How.Name, Using = "last_name")]
        private IWebElement lastNameTextField;

        [FindsBy(How = How.Name, Using = "day")]
        private IWebElement dayOfBirthDD;

        [FindsBy(How = How.Name, Using = "month")]
        private IWebElement monthOfBirthDD;

        [FindsBy(How = How.Name, Using = "year")]
        private IWebElement yearOfBirthDD;

        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailAddressField;

        [FindsBy(How = How.Name, Using = "verify_email")]
        private IWebElement confirmEmailAddressField;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement passwordField;

        [FindsBy(How = How.Name, Using = "customerFeedback")]
        private IWebElement howYouHeardAboutUsDD;

        [FindsBy(How = How.CssSelector, Using = "button.button.button-module__button___2VX0t > span")]
        private IWebElement submitBtn;

        [FindsBy(How = How.Id, Using = "terms_accepted")]
        private IWebElement checkboxTermsAccepted;

        [FindsBy(How = How.Id, Using = "opt_out_email")]
        private IWebElement checkboxOptOutEmail;

        [FindsBy(How = How.ClassName, Using = "form-layout__back")]
        private IWebElement backLink;

        [FindsBy(How = How.LinkText, Using = "Login")]
        private IWebElement loginLink;

        [FindsBy(How = How.LinkText, Using = "Some legal bits we need to tell you")]
        private IWebElement someLegalBitsMenu;

        [FindsBy(How = How.CssSelector, Using = "css=div.bottom-menu-module__bits___3WlQ1")]
        private IWebElement someLegalBitsMenuContent;

        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorEmailsDontMatch;

        public void GoTo()
        {
            Browser.GoTo("join/about-me");
        }

        public void ClickOnBackLink()
        {
            backLink.Click();
        }

        public void ClickOnLoginLink()
        {
            loginLink.Click();
        }

        public void SelectTitle()
        {
            var selectElement = new SelectElement(titleDD);
            selectElement.SelectByValue("Miss");
        } 

        public void SelectDayOfBirth()
        {
            var selectElement = new SelectElement(dayOfBirthDD);
            selectElement.SelectByValue("12");
        }

        public void SelectMonthOfBirth()
        {
            var selectElement = new SelectElement(monthOfBirthDD);
            selectElement.SelectByValue("7");
        }

        public void SelectYearOfBirth()
        {
            var selectElement = new SelectElement(yearOfBirthDD);
            selectElement.SelectByValue("1989");
        }

        public void SelectHowYouHeardAboutUs()
        {
            var selectElement = new SelectElement(howYouHeardAboutUsDD);
            selectElement.SelectByValue("Email from Neyber");
        }

        public void CheckboxTermsAcceptedChecked()
        {
            checkboxTermsAccepted.Click();
        }

        public void CheckboxOptOutEmailsChecked() //checkbox has been disabled for non-pmas users
        {
            checkboxOptOutEmail.Click();
        }

        public bool SubmitBtnIsEnabled()
        {
            return submitBtn.IsEnabled();          
        }

        //public bool SubmitBtnIsNotEnabled()
        //{
        //    return submitBtn.IsNotEnabled();
        //}

        public void RegisterUserFromCsv(string key)
        {
            RegisterBase(csvKey: key);
        }

        public void RegisterNewRandomUser()
        {
            RegisterBase(shouldGenerateNewUser: true);
        }

        public void RegisterNewUser(User user)
        {

            RegisterBase(user);
        }

        public void FillInAboutMeForm()
        {
            RegisterBase(shouldClickSubmitButton: false);

        }

        public void RegisterUserButDontTickCheckbox()
        {
            RegisterBase(shouldTickCheckbox: false);
        }


        public void RegisterUserWithNotEqualEmails()
        {
            RegisterBase(areEmailsEqual: false);
        }

        public void RegisterUserWithNonSelectedTitle()
        {
            RegisterBase(shouldSelectTitle: false);
        }

        private void RegisterBase(User user = null, 
            string csvKey = "", 
            bool shouldClickSubmitButton = true,        
            bool shouldGenerateNewUser = false, 
            bool shouldTickCheckbox = true, 
            bool shouldSelectTitle = true, 
            bool areEmailsEqual = true)
        {
            var newUser = new User();

            var userGenerator = new UserGenerator();

            if (user != null)
            {
                newUser = user;
                shouldGenerateNewUser = false;
            }

            if (!string.IsNullOrEmpty(csvKey))
            {
               var userData = CsvDataAccess.GetTestData(csvKey);
                newUser.FirstName = userData.FirstName;
                newUser.LastName = userData.LastName;
                newUser.EmailAddress = userData.EmailPrimary;
                newUser.Password = userData.Password;

            }
            if (shouldGenerateNewUser)
            {
              
                newUser = userGenerator.GetNewUser();              
            }

            if (shouldSelectTitle)
            {
                SelectTitle();
            }

            firstNameTextField.SendKeys(newUser.FirstName);
            lastNameTextField.SendKeys(newUser.LastName);
            SelectDayOfBirth();
            SelectMonthOfBirth();
            SelectYearOfBirth();

            if (!areEmailsEqual)
            {
                emailAddressField.SendKeys(newUser.EmailAddress);
                confirmEmailAddressField.SendKeys("hello@co.uk");
            }

            emailAddressField.SendKeys(newUser.EmailAddress);
            confirmEmailAddressField.SendKeys(newUser.EmailAddress);
            passwordField.SendKeys(newUser.Password);
            SelectHowYouHeardAboutUs();

   
            if (shouldTickCheckbox)
            {
                CheckboxTermsAcceptedChecked();
            }

            if (shouldClickSubmitButton)
            {
                submitBtn.Click();
            }

        }


        public bool EmailsDontMatchErrorIsDisplayed()
        {
            return errorEmailsDontMatch.IsDisplayed();
        }

        public void OpenSomeLegalBitsMenu()
        {
            someLegalBitsMenu.Click();
        }

        public bool SomeLegalBitsMenuIsDisplayed()
        {
            return someLegalBitsMenuContent.IsDisplayed();
        }

        public void ClickOnNeyberLogo()
        {
            logoNeyber.Click();
        }

        public void IsAtUrl()
        {
            Browser.Url.Contains(Urls.AboutMePage);
        }
    }
}
