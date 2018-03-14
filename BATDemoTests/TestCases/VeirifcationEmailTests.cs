using BATDemoFramework;
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

    class VeirifcationEmailTests : TestBase
    {
        [Test]
        public void GoesFromAboutMePageToVerificationEmailPage() //a user for this test has to be generated automatically + check email is sent
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserFromCsv("GoesFromAboutMePageToVerificationEmailPage");
            Pages.VerificationEmail.WaitTillContinueBtnIsVisible(Browser.webDriver);

            Assert.IsTrue(Pages.VerificationEmail.IsAtUrl(), "User is not on Verification email page");
        }

        [Test]
        public void CanLogoutFromVerificationEmailPage() //a user for this test has to be generated automatically
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserFromCsv("CanLogoutFromVerificationEmailPage");
            Pages.VerificationEmail.WaitTillContinueBtnIsVisible(Browser.webDriver);
            Pages.VerificationEmail.ClickOnLogoutLink();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User has not redirected to page");
        }

        [Test]
        public void CanLogoutAndLogBackInToVerificationEmailPage() //a user for this test has to be generated automatically 
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitTillContinueBtnIsVisible(Browser.webDriver);
            Pages.VerificationEmail.ClickOnLogoutLink();
            Pages.Login.LogIn(user);

            Assert.IsTrue(Pages.VerificationEmail.IsAtUrl(), "User is not on the verification email page");
        }

        [Test]
        public void NotVerifiedUserWantsToContinue() //a user for this test has to be generated automatically + check email is sent
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserFromCsv("NotVerifiedUserWantsToContinue");
            Pages.VerificationEmail.WaitTillContinueBtnIsVisible(Browser.webDriver);
            Pages.VerificationEmail.ClickOnContinueBtn();
            Pages.NotVerifiedEmail.WaitTillStartAgainLinkIsVisible(Browser.webDriver);

            Assert.IsTrue(Pages.NotVerifiedEmail.IsAtUrl(), "User is not a /mail/not-verified page");
        }

        [Test]
        public void NotVerifiedUserRequestsNewResetLink() //a user for this test has to be generated automatically + check email is sent
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserFromCsv("NotVerifiedUserRequestsNewResetLink");
            Pages.VerificationEmail.WaitTillContinueBtnIsVisible(Browser.webDriver);
            Thread.Sleep(3000);
            Pages.VerificationEmail.ClickOnResendEmailLink();
            Thread.Sleep(3000);

            
            Assert.IsTrue(Pages.ResendEmail.IsAtUrl(), "User is not a /mail/resend page");
        }
    }
}
