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
    class ChangePassword : TestBase
    {
        [Test][Retry(3)]
        public async Task CanGetToNewPasswordPage()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);

            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown();

            Assert.IsTrue(Pages.ChangePassword.IsAtTitle(), "User couldn't land on Change password page");
        }

        [Test][Retry(3)]
        public async Task CanGoToJoinPage()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);

            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown();
            Pages.ChangePassword.GoToJoinPage();

            Assert.IsTrue(Pages.Join.IsAtUrl(), "User wasn't redirected to Join page");
        }


        [TestCase("", "Please create a password")]
        [TestCase("password1", "See hint indicators")]
        [TestCase("Password", "See hint indicators")]
        public async Task CanGetValidationMessageWhenEntersNewPassword(string a, string b)
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);
            
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown();
            Pages.ChangePassword.EntersNewPassword(a);
            Pages.ChangePassword.ClickToShowHidePassword();

            Assert.AreEqual(Pages.ChangePassword.GetErrorMessage(), b);
        }

        [Test][Retry(3)]
        public async Task CanSetNewPassword()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);

            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown();

            Pages.ChangePassword.SetNewPassword();
            Pages.Login.WaitUntilLoginUrlIsLoaded();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User was not able to set new password");
        }

        [Test][Retry(3)]
        public async Task CanLoginWithNewPassword()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);

            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown();

            Pages.ChangePassword.SetNewPassword();
            Pages.Login.WaitUntilLoginUrlIsLoaded();
            Pages.Login.LogInAsLastRegisteredUser(LoginPage.LoginOptions.UseLastGeneratedPassword);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User was not able to set new password");
        }

        [Test][Retry(3)]
        public async Task CanNotLoginWithOldPassword()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);

            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown();

            Pages.ChangePassword.SetNewPassword();
            Pages.Login.WaitUntilLoginUrlIsLoaded();
            Pages.Login.LogInAsLastRegisteredUser();
            Pages.Login.WaitUntilErrorBlockIsShown();

            Assert.AreEqual(Pages.Login.GetErrorText(), "The email address or password you entered is incorrect. Please check and try again.");
        }

        [Test][Retry(3)]
        public async Task CanNotUseResetPasswordLinkTwice() //bug ACCO-265!!!
        {
            //1. create new user and verify their email
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);
            Pages.EmployerSearch.Logout();

            //2. request reset password link
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            //3. follow reset password link and reset password
            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown();
            Pages.ChangePassword.SetNewPassword();
            Pages.Login.WaitUntilLoginUrlIsLoaded();

            //4. follow same reset password link
            // TODO WORK WRONG WE CAN'T verify resent link in advice only after a new password was entered and submitted.
            Browser.GoToUrl(urlToken);

            Assert.IsTrue(Pages.ExpiredLink.IsAtUrl(), "User was not able to set new password");
        }
    }
}
