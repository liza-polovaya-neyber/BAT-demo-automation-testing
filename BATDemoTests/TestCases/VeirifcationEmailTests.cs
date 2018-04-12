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
        public void UserSubmitsAnAccountOnAboutMePage() 
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

            Thread.Sleep(TimeSpan.FromSeconds(10)); 
            var emailService = new EmailService();
            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);

            Assert.IsNotEmpty(messages, "No verification email is found");
        }


        [Test]
        public void NotVerifiedUserWantsToContinue() 
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnContinueBtn();
            Pages.NotVerifiedEmail.WaitUntilNotVerifiedEmailPageTitleIsShown(Browser.webDriver);
           
            Assert.IsTrue(Pages.NotVerifiedEmail.IsAtUrl(), "User is not a /mail/not-verified page");
        }

        [Test]
        public void UserRequestsNewVerificationEmail()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewRandomUser();
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnResendEmailLink();
            Pages.ResendEmail.WaitUntilResendEmailPageTitleIsShown(Browser.webDriver);

            Assert.IsTrue(Pages.ResendEmail.IsAtUrl(), "User is not on/mail/resend page");
        }

        [Test]
        public void NotVerifiedUserStartsAgain()
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
        public async Task NotVerifiedUserRequestsNewResetLink() 
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Pages.VerificationEmail.ClickOnResendEmailLink();
            Thread.Sleep(5000);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);

            //check that 2 verification emails are received
            Assert.AreEqual(2, messages.Count, "Can't find 2 verification emails");
        }
    }
}
