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
        [Test]
        public async Task CanGetToNewPasswordPage()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown(Browser.webDriver);

            Assert.IsTrue(Pages.ChangePassword.IsAtTitle(), "User couldn't land on Change password page");
        }

        [Test]
        public async Task CanGoToJoinPage()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown(Browser.webDriver);
            Pages.ChangePassword.GoToJoinPage();

            Assert.IsTrue(Pages.Join.IsAtUrl(), "User wasn't redirected to Join page");
        }

        [TestCase("", "Please create a password")]
        [TestCase("password1", "See hint indicators")]
        [TestCase("Password", "See hint indicators")]
        public async Task CanGetValidationMessageWhenEntersNewPassword(string a, string b)
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown(Browser.webDriver);
            Pages.ChangePassword.EntersNewPassword(a);
            Pages.ChangePassword.ClickToShowHidePassword();

            Assert.AreEqual(Pages.ChangePassword.GetErrorMessage(), b);
        }

        [Test]
        public async Task CanSetNewPassword()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown(Browser.webDriver);

            Pages.ChangePassword.SetNewPassword();
            Pages.Login.WaitUntilLoginUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User was not able to set new password");
        }

        [Test]
        public async Task CanLoginWithNewPassword()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown(Browser.webDriver);

            Pages.ChangePassword.SetNewPassword();
            Pages.Login.WaitUntilLoginUrlIsLoaded(Browser.webDriver);
            Pages.Login.LogInAsLastRegisteredUser(LoginPage.LoginOptions.UseLastGeneratedPassword);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User was not able to set new password");
        }

        [Test]
        public async Task CanNotLoginWithOldPassword()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.Logout();
            Pages.ResetPassword.GoTo();
            Pages.ResetPassword.EnterEmailAndClickToResetPassword(user);

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ResetPassword, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.ChangePassword.WaitUntilTitleIsShown(Browser.webDriver);

            Pages.ChangePassword.SetNewPassword();
            Pages.Login.WaitUntilLoginUrlIsLoaded(Browser.webDriver);
            Pages.Login.LogInAsLastRegisteredUser();
            Pages.Login.WaitUntilErrorBlockIsShown(Browser.webDriver);

            Assert.AreEqual(Pages.Login.GetErrorText(), "The email address or password you entered is incorrect. Please check and try again.");
        }
    }
}
