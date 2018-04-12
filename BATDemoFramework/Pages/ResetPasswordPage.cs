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


        //Verifies if "Resend my reset link" button is displayed
        public bool ResendMyResetLinkButtonIsDisplayed()
        {
            return resendMyResetLinkButton.IsDisplayed();
        }

        public bool WaitForResendMyResetLinkButtonIsDisplayed(IWebDriver driver)
        {
            bool result;
            var resetLinkBtn = Browser.WaitUntilElementIsClickable(driver, resendMyResetLinkButton, 8);
            result = resetLinkBtn.Displayed;
            return result;
        }

        public bool TryDifferentEmailLinkIsVisible(IWebDriver driver)
        {
            bool result;
            var tryDifferentEmailElement = Browser.WaitUntilElementIsVisible(driver, By.ClassName("password-reset-sent-module__different-eamil___3smZ4"), 16);
            return result = tryDifferentEmailLink.IsDisplayed();
        }

        //Clicks on "Resend my reset link" button
        public void ClickOnResendMyResetLinkButton()
        {
            resendMyResetLinkButton.Click();
        }

        //Clicks on "Try different email" link
        public void ClickOnTryDifferentEmailLink()
        {
            tryDifferentEmailLink.Click();
        }

        //Fills email box with the newly created email and submits sending a link
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

        public void EnterEmail(User user)
        {
            emailTextField.Click();
            emailTextField.SendKeys(user.EmailAddress);
        }

        //Browser navigates to Reset Password page
        public void GoTo()
        {
            Browser.GoTo("reset-password");
        }

        //Go from Reset Password page => Join page
        public void ClickOnRegisterLink()
        {
            registerLink.Click();
        }

        //Go from Reset Password page => Login page
        public void ClickOnReturntoLoginLink()
        {
            returnToLoginLink.Click();
        }

        //Verifies the browser page title
        public bool IsAt()
        {
            return Browser.Title.Contains("/reset-password");
        }

        //verifis page's url
        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.ResetPassword);
        }
    }
}