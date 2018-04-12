using BATDemoFramework.Generators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using BATDemoFramework;
using BATDemoFramework.TestDataAccess;
using System;
using BATDemoFramework.WebDriverManager.enums;
using System.Collections.Generic;

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

        [FindsBy(How = How.Name, Using = "mobile_number")]
        private IWebElement mobileNumberField;

        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailAddressField;

        [FindsBy(How = How.Name, Using = "verify_email")]
        private IWebElement confirmEmailAddressField;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement passwordField;

        [FindsBy(How = How.Name, Using = "customerFeedback")]
        private IWebElement howYouHeardAboutUsDD;

        [FindsBy(How = How.XPath, Using = "//button/span")]
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

        [FindsBy(How = How.XPath, Using = "//p[2]")]
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

        public void SelectTitle(TitleType type)
        {
            var selectElement = new SelectElement(titleDD);
            try
            {
                //selectElement.SelectByText(type.ToString());
                selectElement.SelectByValue(type.ToString());
            }
            catch (Exception ex)
            {
                var ee = 55;
            }
            
            //selectElement.SelectByIndex(type.GetTypeCode());
        } 
        

        public void SelectDayOfBirth(int index)
        {
            var selectElement = new SelectElement(dayOfBirthDD);
            selectElement.SelectByIndex(index);
        }

        public void SelectMonthOfBirth(int monthCount)
        {
            var selectElement = new SelectElement(monthOfBirthDD);
            selectElement.SelectByIndex(monthCount);
        }

        public void SelectYearOfBirth(int year)
        {
            var selectElement = new SelectElement(yearOfBirthDD);
            selectElement.SelectByValue(year.ToString());
        }

        public void SelectHowYouHeardAboutUs(string option)
        {
            var selectElement = new SelectElement(howYouHeardAboutUsDD);
            selectElement.SelectByValue(option);
        }

        public int GetFeedbackOptionsNumber()
        {
            var selectElement = new SelectElement(howYouHeardAboutUsDD);
            return selectElement.Options.Count;
        }

        public void CheckboxTermsAcceptedChecked()
        {
            checkboxTermsAccepted.Click();
        }

        public void CheckboxOptOutEmailsChecked() //checkbox has been disabled for non-pmas users
        {
            checkboxOptOutEmail.Click();
        }


        public bool SubmitBtnIsDisabled()
        {
            return submitBtn.IsNotEnabled();
            //bool result;
            //string A = submitBtn.GetAttribute("disabled");
            //if (A == "disabled") { 
            //    result = true;
            //}
            //else
            //    result = false;
            //return result;       
        }

        public bool SubmitBtnIsEnabled()
        {
            return submitBtn.IsEnabled();
        }

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
            RegisterBase(null, null, false, true, false,true, true);
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
            bool shouldGenerateNewUser = true, 
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
                SelectTitle(TitleType.Mr);
            }

            firstNameTextField.SendKeys(newUser.FirstName);
            lastNameTextField.SendKeys(newUser.LastName);
            SelectDayOfBirth(12);
            SelectMonthOfBirth(7);
            SelectYearOfBirth(1990);
            EnterMobileNumber("07523698547");

            if (!areEmailsEqual)
            {
                emailAddressField.SendKeys(newUser.EmailAddress);
                confirmEmailAddressField.SendKeys("hello@co.uk");
            }

            emailAddressField.SendKeys(newUser.EmailAddress);
            confirmEmailAddressField.SendKeys(newUser.EmailAddress);
            passwordField.SendKeys(newUser.Password);
            SelectHowYouHeardAboutUs("Google search");

   
            if (shouldTickCheckbox)
            {
                CheckboxTermsAcceptedChecked();
            }

            if (shouldClickSubmitButton)
            {
                submitBtn.Click();
            }

        }
        public void EnterMobileNumber(string number)
        {
            mobileNumberField.Click();
            mobileNumberField.SendKeys(number);
        }

        public TitleType GetTitleText()
        {
            return EnumHelper.GetTitleType(titleDD.GetAttribute("value")); 
        }

        public string GetDayOfBirth()
        {
            return dayOfBirthDD.GetAttribute("value");
        }

        public string GetMonthOfBirth()
        {
            return monthOfBirthDD.GetAttribute("value");
        }
        //public MonthType GetMonthText()
        //{
        //    return EnumHelper.GetMonthType(monthOfBirthDD.GetAttribute("value"));
        //}

        public string GetYearOfBirth()
        {
            return yearOfBirthDD.GetAttribute("value");
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

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.AboutMePage);
        }
    }

    public enum TitleType
    {
        Title,
        Mr,
        Ms,
        Mrs,
        Miss,
        Dr,
        Prof
    }

    //public enum MonthType
    //{
    //    Month,
    //    January,
    //    February,
    //    March,
    //    April,
    //    May,
    //    June,
    //    July, 
    //    August, 
    //    September,
    //    October,
    //    November,
    //    December
    //}
}
