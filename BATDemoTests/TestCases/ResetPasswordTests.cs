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
            Pages.ResetPassword.ClickOnReturntoLoginLink();

            Assert.IsTrue(Pages.Login.IsAtUrl());
        }

        [Test]
        public void CanGoFromResetPasswordPageToJoinPage()
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.ClickOnRegisterLink();

            Assert.IsTrue(Pages.Join.IsAtUrl());
        }

        [Test]
        public void ResetPasswordLinkIsSent()
        {
            //requires later changes - we have to check whether actual email is sent
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickOnResetLinkButton("ResetPasswordLinkIsSent");

            Assert.IsTrue(Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver));
        }

        [Test]
        public void ResetPasswordLinkisResent()
        {   //requires later changes - we have to check whether actual email is sent
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickOnResetLinkButton("ResetPasswordLinkisResent");
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);
            Pages.ResetPassword.ClickOnResendMyResetLinkButton();
           

            Assert.IsTrue(Pages.ResetPassword.WaitForResendMyResetLinkButtonIsDisplayed(Browser.webDriver));
        }

        [Test]
        public void ResetPasswordLinkIsSentToDifferentEmail()
        {   //requires later changes - we have to check whether actual email is sent
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickOnResetLinkButton("ResetPasswordLinkIsSentToDifferentEmail");
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);
            Pages.ResetPassword.ClickOnTryDifferentEmailLink();
            Pages.ResetPassword.EnterEmailAndClickOnResetLinkButton("ResetPasswordLinkIsSentToDifferentEmail");

            Assert.IsTrue(Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver));
        }



    }

}
