﻿using BATDemoFramework;
using NUnit.Framework;
using OpenQA.Selenium;

namespace BATDemoTests
{
    [TestFixture]
    class ResetPasswordTests : TestBase
    {
        private IWebDriver driver;

        [Test]
        public void CanGoToResetPasswordPage()
        {
            Pages.ResetPassword.GoToResetPasswordPage();
            Assert.IsTrue(Pages.ResetPassword.IsAt());  
        }

        [Test]
        public void CanGoFromResetPasswordPageToLoginPage()
        {
            Pages.ResetPassword.ReturntoLoginPage();
            Assert.IsTrue(Pages.Login.IsAt());
        }

        [Test]
        public void CanGoFromResetPasswordPageToJoinPage()
        {
            Pages.ResetPassword.GotoJoinPage();
            Assert.IsTrue(Pages.Join.IsAt());
        }

        [Test]
        public void ResetPasswordLinkIsSent()
        {
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkIsSent");
            //IWebElement resendMyResetLink = driver.FindElement(By.ClassName("button auth__button password-reset-sent-module__button___KWkni button-module__button___2VX0t")); 
            //Assert.IsTrue(Pages.ResetPassword.IsDisplayed(resendMyResetLink));
        }
    }
}
