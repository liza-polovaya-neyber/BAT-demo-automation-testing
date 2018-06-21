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
        [Test]
        public void CanSubmitAnProfileOnAboutMePage()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            Assert.IsTrue(Pages.VerificationEmail.IsAtUrl(), "User is not on Verification email page");
        }


        [Test]
        public void CanLogoutFromVerificationEmailPage()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnLogoutLink();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User has not been redirected to login page");
        }


        [Test]
        public void CanLogoutAndLogBackInToVerificationEmailPage()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnLogoutLink();
            Pages.Login.LogIn(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            Assert.IsTrue(Pages.VerificationEmail.IsAtUrl(), "User is not on the verification email page");
        }

        [Test]
        public async Task VerificationEmailIsReceived()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            await Task.Delay(TimeSpan.FromSeconds(10));
            var emailService = new EmailService();
            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);

            Assert.IsNotEmpty(messages, "No verification email is found");
        }


        [Test]
        public void CanNotContinueIfNotVerified()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnContinueBtn();
            Pages.NotVerifiedEmail.WaitUntilNotVerifiedEmailPageTitleIsShown(Browser.webDriver);

            Assert.IsTrue(Pages.NotVerifiedEmail.IsAtUrl(), "User is not a /mail/not-verified page");
        }

        [Test]
        public void CanRequestNewVerificationEmail()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnResendEmailLink();
            Pages.ResendEmail.WaitUntilResendEmailPageTitleIsShown(Browser.webDriver);

            Assert.IsTrue(Pages.ResendEmail.IsAtUrl(), "User is not on/mail/resend page");
        }

        [Test]
        public void CanStartAgainWhenNotVerified()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnContinueBtn();
            Pages.NotVerifiedEmail.WaitTillStartAgainLinkIsVisible(Browser.webDriver);
            Pages.NotVerifiedEmail.ClickOnStartAgainLink();

            Assert.IsTrue(Pages.AboutMe.IsAtUrl(), "User hasn't been redirected to About Me page");
        }

        [Test]
        public async Task CanRequestNewResetLinkWhenNotVerified()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnResendEmailLink();
            await Task.Delay(8000);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);

            //check that 2 verification emails are received
            Assert.AreEqual(2, messages.Count, "Can't find 2 verification emails"); 
        }

        [Test]
        public async Task CanVerifyPrimaryEmail()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            await Task.Delay(TimeSpan.FromSeconds(10));
            var emailService = new EmailService();

            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);

            Assert.IsTrue(Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver), "User wasn't able to pass email verification step");

        }


        [Test]
        public async Task CanCloseGreenVerificationBanner()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            await Task.Delay(TimeSpan.FromSeconds(10));
            var emailService = new EmailService();
            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.VerificationEmail.WaitUntilGreenBannerIsShown(Browser.webDriver);
            Pages.VerificationEmail.CloseGreenBanner();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);

            Assert.IsFalse(Pages.VerificationEmail.GreenBannerIsShown(), "Green banner is still shown");
        }
    }
}
