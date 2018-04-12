using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.Threading.Tasks;

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
        public void TryDifferentEmailLinkIsShown()
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickOnResetLinkButton();

            Assert.IsTrue(Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver));
        }

        [Test]
        public async Task ResetPasswordLinkIsSent()  //there should be a user created prior to running this test
        {
            var user = new UserGenerator().GetNewUser();

            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            Thread.Sleep(7000);
            var emailService = new EmailService();
            var messages = await emailService.GetMessagesByQuery(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.IsNotEmpty(messages, "No reset password emails found");
        }

        [Test]
        public async Task ResetPasswordLinkisResent()   //there should be a user created prior to running this test
        {  
            var user = new UserGenerator().GetNewUser();

            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);
            Pages.ResetPassword.ClickOnResendMyResetLinkButton();
            Pages.ResetPassword.WaitForResendMyResetLinkButtonIsDisplayed(Browser.webDriver); //can be replaced by Thread sleep


            var emailService = new EmailService();
            var messages = await emailService.GetMessagesByQuery(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.AreEqual(2, messages.Count, "Can't find 2 reset password emails");
        }

        [Test]
        public async Task ResetPasswordLinkIsSentToDifferentEmail()   //there should be a user created prior to running this test
        {   
            var user = new UserGenerator().GetNewUser();

            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);
            Pages.ResetPassword.ClickOnTryDifferentEmailLink();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);  //can be replaced by Thread sleep

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesByQuery(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.AreEqual(2, messages.Count, "Can't find 2 reset password emails");
        }



    }

}
