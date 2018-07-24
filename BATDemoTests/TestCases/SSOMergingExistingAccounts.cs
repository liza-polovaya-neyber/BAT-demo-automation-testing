using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class SSOMergingExistingAccounts : TestBase
    {
        [Test][Retry(3)]
        public async Task ProfileUserWithoutEmployerCanLoginViaSSO()
        {
            //1. create new user on Profile and verify their email
            var newUser = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(newUser);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, newUser.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.Logout();

            //2. create a new SSO user with the email from step 1.
            var user = UserGenerator.ConvertUserToSSOUser(newUser);

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();

            //3. login by new SSO user to existing account
            Pages.SSOAccountConfirm.LogIn(user, newUser);
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User wasn't able to login via SSO platform");
        }

        [Test][Retry(3)]
        public async Task ProfileUserWithEmployerCanLoginViaSSO()
        {
            //1. create new user on Profile and verify their email
            var newUser = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(newUser);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, newUser.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.SelectEnteredEmployer("Travis Perkins");
            Pages.SSOLoginRequired.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.Logout();

            //2. create a new SSO user with the email from step 1.
            var user = UserGenerator.ConvertUserToSSOUser(newUser);

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();

            //3. login by new SSO user to existing account
            Pages.SSOAccountConfirm.LogIn(user, newUser);
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User wasn't able to login via SSO platform");
        }

        [Test][Retry(3)]
        public async Task ProfileUserCantRegisterAsNewSSOUser()
        {
            //1. create new user on Profile and verify their email
            var newUser = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(newUser);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, newUser.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.Logout();

            //2. create a new SSO user with the email from step 1.
            var user = UserGenerator.ConvertUserToSSOUser(newUser);

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();

            //3. create a new SSO account with existing email (from step 1)
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);

            Assert.AreEqual(Pages.SSOAboutMe.GetErrorPasswordText(), "This email has already been given by your provider. Please use an alternative.");
        }
    }
}

