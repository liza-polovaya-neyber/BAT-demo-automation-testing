using System;
using BATDemoFramework.TestDataAccess;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using BATDemoFramework.Generators;

namespace BATDemoFramework
{
    public class ResetPasswordPage
    {

        [FindsBy(How = How.ClassName, Using = "control__input")]
        private IWebElement emailTextField;

        [FindsBy(How = How.CssSelector, Using = "button.button.button.auth__button.password-reset-form-module__button___G5XgU.button-module__button___2VX0t > span")]
        private IWebElement sendMyResetLinkButton;

        [FindsBy(How = How.ClassName, Using = "auth__reg-link")]
        private IWebElement registerLink;

        [FindsBy(How = How.LinkText, Using = "Return to login")]
        private IWebElement returnToLoginLink;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement resendMyResetLinkButton;


        [FindsBy(How = How.ClassName, Using = "password-reset-sent-module__different-eamil___3smZ4")]
        private IWebElement tryDifferentEmailLink;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorValidation;

        
        public bool CheckResendMyResetLinkButtonIsDisplayed()
        {
            return resendMyResetLinkButton.IsDisplayed();
        }

        public bool WaitForResendMyResetLinkButtonIsDisplayed(IWebDriver driver)
        {
            var resetLinkBtn = Browser.WaitUntilElementIsClickable(driver, resendMyResetLinkButton, 8);
            return resetLinkBtn.Displayed; 
        }

        public bool TryDifferentEmailLinkIsVisible(IWebDriver driver)
        {     
            var tryDifferentEmailElement = Browser.WaitUntilElementIsVisible(driver, By.ClassName("password-reset-sent-module__different-eamil___3smZ4"), 30);
            return tryDifferentEmailLink.IsDisplayed();
        }

        public void ClickOnResendMyResetLinkButton()
        {
            resendMyResetLinkButton.Click();
        }

        public void ClickOnTryDifferentEmailLink()
        {
            tryDifferentEmailLink.Click();
        }

        public void EnterEmailAndClickOnResetLinkButton()
        {
            var user = new UserGenerator().GetNewUser();
            //var userData = CsvDataAccess.GetTestData(testName);

            emailTextField.Click();
            emailTextField.SendKeys(user.EmailAddress);

            sendMyResetLinkButton.Click();
        }

        public void EnterEmailAndClickToResetPassword(User user)
        {
            emailTextField.Click();
            emailTextField.SendKeys(user.EmailAddress);

            sendMyResetLinkButton.Click();
        }

        public void EnterEmail(string email)
        {
            emailTextField.Click();
            emailTextField.SendKeys(email);
        }

        public string GetTextError()
        {
            return errorValidation.Text;
        }

        public void GoTo()
        {
            Browser.GoTo("reset-password");
        }

        public void ClickOnRegisterLink()
        {
            registerLink.Click();
        }

        public void ClickOnReturntoLoginLink()
        {
            returnToLoginLink.Click();
        }

        public bool IsAt()
        {
            return Browser.Title.Contains("/reset-password");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.ResetPassword);
        }
    }
}