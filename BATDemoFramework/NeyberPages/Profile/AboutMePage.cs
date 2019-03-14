using System;
using BATDemoFramework.Generators;
using BATDemoFramework.TestDataAccess;
using BATDemoFramework.WebDriverManager.enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace BATDemoFramework.NeyberPages.Profile
{
    public class AboutMePage
    {
        IWebDriver webDriver;


        [FindsBy(How = How.Name, Using = "first_name")]
        private IWebElement firstNameTextField;

        [FindsBy(How = How.Name, Using = "last_name")]
        private IWebElement lastNameTextField;

        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement emailAddressField;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement passwordField;

        [FindsBy(How = How.Name, Using = "referral_code']")]
        private IWebElement referralCodeField;

        [FindsBy(How = How.XPath, Using = "//div[7]/button/span")]
        private IWebElement submitBtn;

        [FindsBy(How = How.Id, Using = "terms_accepted")]
        private IWebElement checkboxTermsAccepted;

        [FindsBy(How = How.LinkText, Using = "Login")]
        private IWebElement loginLink;

        [FindsBy(How = How.XPath, Using = "//p/button")]
        private IWebElement someLegalBitsMenu;

        [FindsBy(How = How.XPath, Using = "//p[2]")]
        private IWebElement someLegalBitsMenuContent;

        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.XPath, Using = "//div[4]/div/div/p")]
        private IWebElement errorWrongPassword;

        public void GoTo()
        {
            Browser.GoTo("join/about-me");
        }

        public void ClickOnLoginLink()
        {
            loginLink.Click();
        }


        public void CheckboxTermsAcceptedChecked()
        {
            checkboxTermsAccepted.Click();
        }

        public bool SubmitBtnIsDisabled()
        {
            return Browser.WaitUntilAttributeIsPresent(submitBtn, "disabled", 10);           
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
            RegisterBase(null, null, true, false, false);
        }


        private void RegisterBase(User user = null, 
            string csvKey = "", 
            bool shouldClickSubmitButton = true,        
            bool shouldGenerateNewUser = true, 
            bool shouldTickCheckbox = true)
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
                shouldGenerateNewUser = false;
            }
            if (shouldGenerateNewUser)
            {
              
                newUser = userGenerator.GetNewUser();              
            }

            firstNameTextField.SendKeys(newUser.FirstName);
            lastNameTextField.SendKeys(newUser.LastName);
            emailAddressField.SendKeys(newUser.EmailAddress);
            passwordField.SendKeys(newUser.Password);
   
            if (shouldTickCheckbox)
            {
                CheckboxTermsAcceptedChecked();
            }

            if (shouldClickSubmitButton)
            {
                submitBtn.Click();
            }

        }
        
        public void EnterPassword(string password)
        {
            passwordField.Click();
            passwordField.SendKeys(password);
        }

        public string GetErrorPasswordText()
        {
            return errorWrongPassword.Text;
        }

        public void PutCursorOnEmailInput()
        {
            emailAddressField.Click();
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

        public bool WaitUntilUrlIsLoaded()
        {
            var aboutMePage = Browser.WaitUntilUrlIsLoaded(Urls.AboutMePage, 20);
            return Pages.AboutMe.IsAtUrl();
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
    }

}
