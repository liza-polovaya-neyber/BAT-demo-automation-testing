using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class VeirificationEmailTests : TestBase
    {
        [Test][Retry(3)]
        public void CanSubmitAnProfileOnAboutMePage()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            Assert.IsTrue(Pages.VerificationEmail.IsAtUrl(), "User is not on Verification email page");
        }


        [Test][Retry(3)]
        public void CanLogoutFromVerificationEmailPage()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();
            Pages.VerificationEmail.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User has not been redirected to login page");
        }


        [Test][Retry(3)]
        public void CanLogoutAndLogBackInToVerificationEmailPage()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();
            Pages.VerificationEmail.Logout();
            Pages.Login.LogIn(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            Assert.IsTrue(Pages.VerificationEmail.IsAtUrl(), "User is not on the verification email page");
        }

        [Test][Retry(3)]
        public async Task VerificationEmailIsReceived()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);

            Assert.IsNotEmpty(messages, "No verification email is found");
        }


        [Test][Retry(3)]
        public void CanNotContinueIfNotVerified()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();
            Pages.VerificationEmail.ClickOnContinueBtn();
            Pages.NotVerifiedEmail.WaitUntilNotVerifiedEmailPageTitleIsShown();

            Assert.IsTrue(Pages.NotVerifiedEmail.IsAtUrl(), "User is not a /mail/not-verified page");
        }

        [Test][Retry(3)]
        public void CanRequestNewVerificationEmail()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();
            Pages.VerificationEmail.ClickOnResendEmailLink();
            Pages.ResendEmail.WaitUntilResendEmailPageTitleIsShown();

            Assert.IsTrue(Pages.ResendEmail.IsAtUrl(), "User is not on/mail/resend page");
        }

        [Test][Retry(3)]
        public void CanStartAgainWhenNotVerified()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();
            Pages.VerificationEmail.ClickOnContinueBtn();
            Pages.NotVerifiedEmail.WaitTillStartAgainLinkIsVisible();
            Pages.NotVerifiedEmail.ClickOnStartAgainLink();

            Assert.IsTrue(Pages.AboutMe.IsAtUrl(), "User hasn't been redirected to About Me page");
        }

        [Test][Retry(3)]
        public async Task CanRequestNewResetLinkWhenNotVerified()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();
            Pages.VerificationEmail.ClickOnResendEmailLink();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);

            //check that 2 verification emails are received
            Assert.AreEqual(2, messages.Count, "Can't find 2 verification emails"); 
        }

        [Test][Retry(3)]
        public async Task CanVerifyPrimaryEmail()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User wasn't able to pass email verification step");

        }


        [Test][Retry(3)]
        public async Task CanCloseGreenVerificationBanner()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.VerificationEmail.WaitUntilGreenBannerIsShown();
            Pages.VerificationEmail.CloseGreenBanner();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsFalse(Pages.VerificationEmail.GreenBannerIsShown(), "Green banner is still shown");
        }
    }
}
