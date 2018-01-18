using BATDemoFramework;
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
            Pages.ResetPassword.GoTo();

            Assert.IsTrue(Pages.ResetPassword.IsAtUrl());
        }

        [Test]
        public void CanGoFromResetPasswordPageToLoginPage()
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.ReturntoLoginPage();

            Assert.IsTrue(Pages.Login.IsAtUrl());
        }

        [Test]
        public void CanGoFromResetPasswordPageToJoinPage()
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.GotoJoinPage();

            Assert.IsTrue(Pages.Join.IsAtUrl());
        }

        [Test]
        public void ResetPasswordLinkIsSent()
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkIsSent");

            Assert.IsTrue(Pages.ResetPassword.ResendMyResetLinkButtonIsDisplayed());
        }

        [Test]
        public void ResetPasswordLinkisResent()
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkisResent");
            Pages.ResetPassword.ClickOnResendMyResetLinkButton();

            Assert.IsTrue(Pages.ResetPassword.ResendMyResetLinkButtonIsDisplayed());
        }

        [Test]
        public void ResetPasswordLinkIsSentToDifferentEmail()
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkIsSentToDifferentEmail");
            Pages.ResetPassword.ClickOnTryDifferentEmailLink();
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkIsSentToDifferentEmail");

            Assert.IsTrue(Pages.ResetPassword.ResendMyResetLinkButtonIsDisplayed());
        }



    }

}
