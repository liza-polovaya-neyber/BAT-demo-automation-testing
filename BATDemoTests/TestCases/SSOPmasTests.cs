using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
using NUnit.Framework;
using OpenQA.Selenium;
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
    class SSOPmasTests : TestBase
    {

        [Test][Retry(3)]
        public void CanGetToAccountConfirmPage()
        {
            Preconditions.NewSSOUserCreated();

            Assert.True(Pages.SSOAccountConfirm.IsAtUrl(), "User can't get to SSO Account confirmation page");
        }

        [Test][Retry(3)]
        public void CanGoBackToAccountConfirmPage()
        {
            Preconditions.NewSSOUserCreated();

            Pages.SSOAccountConfirm.ClickToContinue();
            Browser.NavigateBack();

            Assert.True(Pages.SSOAccountConfirm.IsAtUrl(), "User can't get to SSO Account confirmation page");
        }

        [Test][Retry(3)]
        public void CanFillInSSOAboutMeForm()
        {
            Preconditions.NewSSOUserCreated();
            var newUser = new UserGenerator().GetNewUser();

            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);
            Pages.SSOAboutMe.PressSubmitButton();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();

            Assert.True(Pages.Marketing.IsAtUrl(), "User wasn't able to get to Marketing page");
        }

        [Test][Retry(3)]
        public void CanLogoutAndLogBackInOnProfileJourney()
        {
            var user = UserGenerator.GetNewSSOUser();
            var newUser = new UserGenerator().GetNewUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);
            Pages.SSOAboutMe.PressSubmitButton();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.LogoutOption();
            Pages.Login.LogInBySSOUserPrimaryEmail(user, newUser);
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.ChooseEmailOption();
            Pages.Marketing.ChoosePhoneOption();
            Pages.Marketing.ChoosePostOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();

            Assert.True(Pages.Home.IsAtUrl(), "User wasn't able to get to Home page after logging back in");
        }

        [Test][Retry(3)]
        public void CanNotEnterDifferentAlternativeEmails()
        {
            Preconditions.NewSSOUserCreated();
            var newUser = new UserGenerator().GetNewUser();

            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterWithDifferentAlternativeEmails(newUser);

            Assert.AreEqual(Pages.SSOAboutMe.GetErrorPasswordText(), "The email addresses do not match");
        }

        [Test][Retry(3)]
        public void CanSkipAlternativeEmailFields()
        {
            Preconditions.NewSSOUserCreated();
            var newUser = new UserGenerator().GetNewUser();

            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserButDontEnterAlternativeEmail(newUser);
            Pages.SSOAboutMe.PressSubmitButton();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();

            Assert.True(Pages.Marketing.IsAtUrl(), "User wasn't able to get to Marketing page without filling in the alternative email");
        }

        [Test][Retry(3)]
        public void CanSkipMobileNumberField()
        {
            Preconditions.NewSSOUserCreated();
            var newUser = new UserGenerator().GetNewUser();

            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserButDontEnterMobileNumber(newUser);
            Pages.SSOAboutMe.PressSubmitButton();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();

            Assert.True(Pages.Marketing.IsAtUrl(), "User wasn't able to get to Marketing page without filling in the alternative email");
        }

        [Test][Retry(3)]
        public void CantRegisterIfJoinedLessThan6MosAgo()
        {
            var user = UserGenerator.GetNewSSOUser(false);

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOIneligibleState.WaitUntilUrlIsLoaded();

            Assert.True(Pages.SSOIneligibleState.IsAtUrl(), "User wasn't redirected to validation screen");
        }


        [TestCase("09523697411", "Must start with 07")]
        [TestCase("0752369741101", "Must be 11 digits")]
        [TestCase("0752369741", "Must be 11 digits")]
        public void CanNotEnterWrongPhoneNoFormat(string a, string b)
        {
            Preconditions.NewSSOUserCreated();

            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.EnterMobileNumber(a);
            Pages.SSOAboutMe.PutCursorOnEmailInput();

            Assert.AreEqual(Pages.SSOAboutMe.GetErrorPasswordText(), b);
        }

        [TestCase("Password", "See hint indicators")]
        [TestCase("password1", "See hint indicators")]
        [TestCase("passwor", "See hint indicators")]
        public void CanNotEnterWrongPassword(string a, string b)
        {
            Preconditions.NewSSOUserCreated();

            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.EnterPassword(a);
            Pages.SSOAboutMe.PutCursorOnEmailInput();

            Assert.AreEqual(Pages.SSOAboutMe.GetErrorPasswordText(), b);
        }

        [Test][Retry(3)]
        public void CanNotLoginByNotVerifiedAlternativeEmail()
        {
            Preconditions.NewSSOUserCreated();
            var newUser = new UserGenerator().GetNewUser();

            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);
            Pages.SSOAboutMe.PressSubmitButton();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.LogoutOption();
            Pages.Login.LogInBySSOUserAlternativeEmail(newUser);
            Pages.Login.WaitUntilErrorBlockIsShown();

            Assert.AreEqual(Pages.Login.GetErrorText(), "The email address or password you entered is incorrect. Please check and try again.");
        }


        [Test][Retry(3)]
        public async Task CanLoginByVerifiedAlternativeEmail()
        {
            Preconditions.NewSSOUserCreated();
            var newUser = new UserGenerator().GetNewUser();

            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);
            Pages.SSOAboutMe.PressSubmitButton();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.LogoutOption();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, newUser.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);
            Browser.GoToUrl(urlToken);

            Pages.Login.WaitUntilLoginUrlIsLoaded();
            Pages.Login.LogInBySSOUserAlternativeEmail(newUser);
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User wasn't able to login by verified alternative email address");
        }

        [Test][Retry(3)]
        public void CanNotEnterSSOPrimaryEmailAsAlternative()
        {
            var user = UserGenerator.GetNewSSOUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();

            Pages.SSOAboutMe.EnterAlternativeEmail(user.Email);

            Assert.AreEqual(Pages.SSOAboutMe.GetErrorPasswordText(), "This email has already been given by your provider. Please use an alternative.");

        }
    }
}
