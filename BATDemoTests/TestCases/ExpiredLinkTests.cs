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
    class ExpiredLinkTests : TestBase
    {
        [Test]
        [Retry(3)]
        public async Task CanGetToExpiredLinkPageWhenVerifyingPrimaryEmail()
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
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded();

            Assert.IsTrue(Pages.ExpiredLink.IsAtUrl(), "User wasn't able to get to the Expired link page");

        }

        [Test]
        [Retry(3)]
        public async Task CanGetToExpiredLinkPageWhenVerifyingAlternativeEmail()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Login.WaitUntilLoginUrlIsLoaded();
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded();

            Assert.IsTrue(Pages.ExpiredLink.IsAtUrl(), "User wasn't able to get to the Expired link page");
        }

        [Test]
        [Retry(3)]
        public async Task CanHitTheLogo()
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
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded();
            Pages.ExpiredLink.ClickOnLogo();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User wasn't able to get to the Login page");
        }

        [Test]
        [Retry(3)]
        public async Task CanReturnToNeyber()
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
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded();
            Pages.ExpiredLink.ClickOnReturnBtn();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User wasn't able to get to the Login page");
        }

        [Test]
        [Retry(3)]
        public async Task CanReturnToNeyberLogin()
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
            Pages.EmployerSearch.Logout();
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded();
            Pages.ExpiredLink.ClickOnReturnBtn();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User wasn't able to get to the Login page");
        }

        [Test][Retry(3)]
        public async Task CanNotFollowVerificationLinkTwice()
        {
            //1. create new user and send verification link twice
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();
            Pages.VerificationEmail.ClickOnResendEmailLink();

            //2. follow the first link
            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            //3. follow second link
            var urlToken1 = emailService.GetUrlTokenFromMessage(messages[1]);
            Browser.GoToUrl(urlToken1);
            Pages.ExpiredLink.WaitUntilPageIsLoaded();

            Assert.IsTrue(Pages.ExpiredLink.IsAtUrl(), "User isn't being sent to expired link page when trying to verify their email second time in a row");
        }
    }
}
