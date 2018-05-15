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
        public async Task CanGetToExpiredLinkPage()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            Thread.Sleep(TimeSpan.FromSeconds(10));
            var emailService = new EmailService();

            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilPageIsLoaded(Browser.webDriver);
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.ExpiredLink.IsAtUrl(), "User wasn't able to get to the Expired link page");

        }

        [Test]
        public async Task CanHitTheLogo()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            Thread.Sleep(TimeSpan.FromSeconds(10));
            var emailService = new EmailService();

            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilPageIsLoaded(Browser.webDriver);
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded(Browser.webDriver);
            Pages.ExpiredLink.ClickOnLogo();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User wasn't able to get to the Login page");
        }

        [Test]
        public async Task CanReturnToNeyber()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            Thread.Sleep(TimeSpan.FromSeconds(10));
            var emailService = new EmailService();

            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilPageIsLoaded(Browser.webDriver);
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded(Browser.webDriver);
            Pages.ExpiredLink.ClickOnReturnBtn();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User wasn't able to get to the Login page");
        }

        [Test]
        public async Task CanReturnToNeyberLogin()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            Thread.Sleep(TimeSpan.FromSeconds(10));
            var emailService = new EmailService();

            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilPageIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded(Browser.webDriver);
            Pages.ExpiredLink.ClickOnReturnBtn();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User wasn't able to get to the Login page");
        }
    }
}
