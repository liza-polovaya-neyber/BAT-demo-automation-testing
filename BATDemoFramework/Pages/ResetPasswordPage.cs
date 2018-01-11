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
        private IWebElement resendMyResetLink;

        [FindsBy(How = How.ClassName, Using = "password-reset-sent-module__different-eamil___3smZ4")]
        private IWebElement tryDifferentEmailLink;

        public void ClickToSendResetLinkButton(string testName)
        {
            var userData = CsvDataAccess.GetTestData(testName);
            emailTextField.Click();
            emailTextField.SendKeys(userData.Email);

            sendMyResetLinkButton.Click();
        }


        public void GoToResetPasswordPage()
        {
            Browser.GoTo("/reset-password");
        }

        public void GotoJoinPage()
        {
            registerLink.Click();
        }

        public void ReturntoLoginPage()
        {
            returnToLoginLink.Click();
        }

        public bool IsAt()
        {
            return Browser.Title.Contains("/reset-password");
        }
    }
}