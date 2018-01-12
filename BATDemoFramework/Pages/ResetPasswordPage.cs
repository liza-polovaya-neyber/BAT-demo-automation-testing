using System;
using BATDemoFramework.TestDataAccess;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class ResetPasswordPage
    {

        [FindsBy(How = How.ClassName, Using = "control__input")]
        private IWebElement emailTextField;

        [FindsBy(How = How.ClassName, Using = "button button auth__button password-reset-form-module__button___Q5_jB button-module__button___p4iTs")]
        private IWebElement sendMyResetLinkButton;

        [FindsBy(How = How.ClassName, Using = "auth__reg-link")]
        private IWebElement registerLink;

        [FindsBy(How = How.LinkText, Using = "Return to login")]
        private IWebElement returnToLoginLink;

        [FindsBy(How = How.ClassName, Using = "button auth__button password-reset-sent-module__button___KWkni button-module__button___2VX0t")]
        private IWebElement resendMyResetLinkButton;


        [FindsBy(How = How.ClassName, Using = "password-reset-sent-module__different-eamil___3smZ4")]
        private IWebElement tryDifferentEmailLink;


        //Verifies if "Resend my reset link" button is displayed
        public bool ResendMyResetLinkButtonIsDisplayed()
        {
            bool result;
            result = resendMyResetLinkButton.IsDisplayed();
            return result;
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

        //Fills email box with the email from CSV file and submits sending a link
        public void ClickToSendResetLinkButton(string testName)
        {
            var userData = CsvDataAccess.GetTestData(testName);
            emailTextField.Click();
            emailTextField.SendKeys(userData.Email);

            sendMyResetLinkButton.Click();
        }

        //Browser navigates to Reset Password page
        public void GoToResetPasswordPage()
        {
            Browser.GoTo("/reset-password");
        }

        //Go from Reset Password page => Join page
        public void GotoJoinPage()
        {
            registerLink.Click();
        }

        //Go from Reset Password page => Login page
        public void ReturntoLoginPage()
        {
            returnToLoginLink.Click();
        }

        //Verifies the browser page title
        public bool IsAt()
        {
            return Browser.Title.Contains("/reset-password");
        }
    }
}