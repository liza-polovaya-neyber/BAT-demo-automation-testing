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
            
            Assert.IsTrue(Pages.ResetPassword.ResendMyResetLinkButtonIsDisplayed());
        }

        [Test]
        public void ResetPasswordLinkisResent()
        {
            Pages.ResetPassword.GoToResetPasswordPage();
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkisResent");
            Pages.ResetPassword.ClickOnResendMyResetLinkButton();

            Assert.IsTrue(Pages.ResetPassword.ResendMyResetLinkButtonIsDisplayed());
        }

        [Test]
        public void ResetPasswordLinkIsSentToDifferentEmail()
        {
            Pages.ResetPassword.GoToResetPasswordPage();
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkIsSentToDifferentEmail");
            Pages.ResetPassword.ClickOnTryDifferentEmailLink();
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkIsSentToDifferentEmail");

            Assert.IsTrue(Pages.ResetPassword.ResendMyResetLinkButtonIsDisplayed());
        }



    }

}
