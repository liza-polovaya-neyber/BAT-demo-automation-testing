using BATDemoFramework;
using BATDemoFramework.EmailServices;
using BATDemoFramework.Generators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework.NeyberPages;
using BATDemoFramework.Pages;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class SSOMergingExistingAccounts : TestBase
    {
        [Test][Retry(3)]
        public void ProfileUserNotVerifiedCanRegisterAsSSO()
        {
            //1. create new user on Profile and DON'T verify their email
            var newUser = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(newUser);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();
            Pages.VerificationEmail.Logout();

            //2. create a new SSO user with the email from step 1.
            var user = UserGenerator.ConvertUserToSSOUser(newUser);

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();

            //3. try to set up a new SSO user
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);
            Pages.SSOAboutMe.PressSubmitButton();
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();

            //4. login by profile user with non-verified email
            Pages.SSOAccountConfirm.LogInBySSOUser(newUser);
            Pages.SSOAdditionalDetails.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.SSOAdditionalDetails.IsAtUrl(), "Profile user with non-verified email wasn't able to login as a new SSO user");
        }

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
            Pages.SSOAccountConfirm.LogInBySSOUser(newUser);
            Pages.SSOAdditionalDetails.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.SSOAdditionalDetails.IsAtUrl(), "User wasn't able to login via SSO platform");
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
            Pages.SSOAccountConfirm.LogInBySSOUser(newUser);
            Pages.SSOAdditionalDetails.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.SSOAdditionalDetails.IsAtUrl(), "User wasn't able to login via SSO platform");
        }

        [Test][Retry(3)]
        public async Task SSOUserSetsAlternativeEmailRegisteredOnProfile()
        {
            //1. create a new user on Profile and verify their email
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

            //2. create a new SSO user with a different primary email.
            var user = UserGenerator.GetNewSSOUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();

            //3. create a new SSO account with alternative email from step 1
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);
            Pages.SSOAboutMe.PressSubmitButton();

            //4. login by email from step 2
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.LogInByProfileUser(newUser);

            Assert.IsTrue(Pages.SSOAdditionalDetails.IsAtUrl(), "User wasn't able to login by the user that was created on /join");
            //to specify whether this is a bug (now user's not able to login with SSO email, but OK with /join email)

        }


    }
}

