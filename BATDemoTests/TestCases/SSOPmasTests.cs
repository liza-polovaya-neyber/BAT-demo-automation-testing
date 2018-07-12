using BATDemoFramework;
using BATDemoFramework.Generators;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]
    class SSOPmasTests : TestBase
    {

        [Test][Retry(3)]
        public void CanGetToAccountConfirmPage()
        {
            var user = UserGenerator.GetNewSSOUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();

            Assert.True(Pages.SSOAccountConfirm.IsAtUrl(), "User can't get to SSO Account confirmation page");
        }

        [Test][Retry(3)]
        public void CanGoBackToAccountConfirmPage()
        {
            var user = UserGenerator.GetNewSSOUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();
            Browser.NavigateBack();

            Assert.True(Pages.SSOAccountConfirm.IsAtUrl(), "User can't get to SSO Account confirmation page");
        }

        [Test][Retry(3)]
        public void CanFillInSSOAboutMeForm()
        {
            var user = UserGenerator.GetNewSSOUser();
            var newUser = new UserGenerator().GetNewUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);
            Pages.SSOAboutMe.PressSubmitButton();

            Assert.True(Pages.Marketing.IsAtUrl(), "User wasn't able to get to Marketing page");
        }

        [Test][Retry(3)]
        public void CanNotEnterDifferentAlternativeEmails()
        {
            var user = UserGenerator.GetNewSSOUser();
            var newUser = new UserGenerator().GetNewUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterWithDifferentAlternativeEmails(newUser);

            Assert.AreEqual(Pages.SSOAboutMe.GetErrorPasswordText(), "The email addresses do not match");
        }

        [Test][Retry(3)]
        public void CanSkipAlternativeEmailFields()
        {
            var user = UserGenerator.GetNewSSOUser();
            var newUser = new UserGenerator().GetNewUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserButDontEnterAlternativeEmail(newUser);
            Pages.SSOAboutMe.PressSubmitButton();

            Assert.True(Pages.Marketing.IsAtUrl(), "User wasn't able to get to Marketing page without filling in the alternative email");
        }

        [Test][Retry(3)]
        public void CanSkipMobileNumberField()
        {
            var user = UserGenerator.GetNewSSOUser();
            var newUser = new UserGenerator().GetNewUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserButDontEnterMobileNumber(newUser);
            Pages.SSOAboutMe.PressSubmitButton();

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
            var user = UserGenerator.GetNewSSOUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
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
            var user = UserGenerator.GetNewSSOUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.EnterPassword(a);
            Pages.SSOAboutMe.PutCursorOnEmailInput();

            Assert.AreEqual(Pages.SSOAboutMe.GetErrorPasswordText(), b);
        }

       
    }
}
