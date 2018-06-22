﻿using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
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

        [TestCase("qwerty", "Please enter a valid email address")]
        [TestCase("", "Please enter your email address")]
        public void CanNotEnterInvalidEmail(string a, string b)
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmail(a);
            Pages.ResetPassword.ClickOnResendMyResetLinkButton();

            Assert.AreEqual(Pages.ResetPassword.GetTextError(), b);
        }

        [Test]
        public void CanSeeTryDifferentEmailLink()
        {
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickOnResetLinkButton();

            Assert.IsTrue(Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver));
        }

        [Test]
        public async Task ResetLinkIsNotSentToNotRegisteredUser()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.IsEmpty(messages, "Reset password email is received");
        }

        [Test]
        public async Task TryDifferentResetLinkIsNotSentToNotRegisteredUser()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToReset();
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);
            Pages.ResetPassword.ClickOnTryDifferentEmailLink();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.IsEmpty(messages, "Reset password email is received");
        }

        [Test]
        public async Task RegisteredUserReceivesResetPasswordLink()  
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.IsNotEmpty(messages, "No reset password emails found");
        }

        [Test]
        public async Task CanResendResetPasswordLink()  
        {  
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);
            Pages.ResetPassword.ClickOnResendMyResetLinkButton();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.AreEqual(2, messages.Count, "Can't find 2 reset password emails");
        }

        [Test]
        public async Task ResetPasswordLinkIsSentToDifferentEmail()   
        {   
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);
            Pages.ResetPassword.ClickOnTryDifferentEmailLink();
            Pages.ResetPassword.EnterEmailAndClickToReset();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.AreEqual(1, messages.Count, "There was not just 1 reset password email in the mailbox");
        }

        [Test]
        public async Task ResetPasswordLinkIsReceivedOnAlternativeEmail()   
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);

            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToReset();
            Pages.ResetPassword.TryDifferentEmailLinkIsVisible(Browser.webDriver);
            Pages.ResetPassword.ClickOnTryDifferentEmailLink();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);
   
            var newMessages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);

            Assert.AreEqual(1, messages.Count, "There was not just 1 reset password email in the mailbox");
        }

    }

}
