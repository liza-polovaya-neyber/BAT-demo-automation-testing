using BATDemoFramework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace BATDemoTests
{
    [TestFixture]
    public class ResetPasswordTests : TestBase
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
            Thread.Sleep(10000);

            Assert.IsTrue(Pages.ResetPassword.ResendMyResetLinkButtonIsDisplayed());
        }

        [Test]
        public void ResetPasswordLinkisResent()
        {   //requires later changes - we have to check whether actual email is sent
            Pages.ResetPassword.GoTo();
            Thread.Sleep(8000);
            Pages.ResetPassword.ClickToSendResetLinkButton("ResetPasswordLinkisResent");
            Thread.Sleep(8000);
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
