using BATDemoFramework;
using NUnit.Framework;
using OpenQA.Selenium;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework.Generators;
using BATDemoFramework.NeyberPages;
using BATDemoFramework.NeyberPages.Profile;
using BATDemoFramework.Pages;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class AboutMeTests : TestBase
    {
        private IWebElement element;

        [Test][Retry(3)]
        public void CanGoBackFromAboutMePagetoJoin()
        {
            Pages.Join.GoTo();
            Pages.Join.GotoAboutMePage();
            Pages.AboutMe.ClickOnBackLink();

            Assert.IsTrue(Pages.Join.IsAtUrl(), "User is not on Join page");
        }

        [Test][Retry(3)]
        public void CanGoFromAboutMePageToLogin()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.ClickOnLoginLink();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User is not on Login page");
        }

        [Test][Retry(3)]
        public void CanSeeSomeLegalBitsMenu()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.OpenSomeLegalBitsMenu();

            Assert.IsTrue(Pages.AboutMe.SomeLegalBitsMenuIsDisplayed(), "Some Legal bits menu is not opened");
        }

        [Test][Retry(3)]
        public void CanGoToLoginPageOnLogoClick()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.ClickOnNeyberLogo();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User is not on Login page");
        }

        [Test][Retry(3)]
        public void CanNotEnterDifferentPrimaryEmails()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserWithNotEqualEmails();

            Assert.AreEqual(Pages.AboutMe.GetEmailsMismatchError(), "The email addresses do not match");
        }

        [Test][Retry(3)]
        public void CanNotSubmitAboutMeForm()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.FillInAboutMeForm();

            //Assert.IsFalse(Pages.AboutMe.SubmitBtnIsDisabled(), "'Submit' button is not enabled");
        }


        [Test][Retry(3)]
        [Ignore("Fix submint reread submit button")]
        public void CanSubmitAboutMeForm()
       {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserButDontTickCheckbox();

            Assert.IsTrue(Pages.AboutMe.SubmitBtnIsDisabled(), "Submit button is enabled despite T&C checkboxes aren't being checked");
        }

        [Test][Retry(3)]
        [Ignore("Fix submint reread submit button")]
        public void CanNotSubmitAboutMeFormWithoutTitle()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserWithNonSelectedTitle();

            Assert.IsTrue(Pages.AboutMe.SubmitBtnIsDisabled(), "Submit button is enabled despite 'Titile' DD hasn't been selected");
        }

        [Test][Retry(3)]
        public void CanSelectRightTitle()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectTitle(TitleType.Ms);

            Assert.AreEqual(TitleType.Ms, Pages.AboutMe.GetTitleText());
        }
        
        [TestCase("09523697411", "Must start with 07")]
        [TestCase("0752369741101", "Must be 11 digits")]
        [TestCase("0752369741", "Must be 11 digits")]
        public void CanNotEnterWrongPhoneNoFormat(string a, string b)
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.EnterMobileNumber(a);
            Pages.AboutMe.PutCursorOnEmailInput();

            Assert.AreEqual(Pages.AboutMe.GetErrorPhoneNoText(), b);
        }

        [TestCase("Password", "See hint indicators")]
        [TestCase("password1", "See hint indicators")]
        [TestCase("passwor", "See hint indicators")]
        public void CanNotEnterWrongPassword(string a, string b)
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.EnterPassword(a);
            Pages.AboutMe.PutCursorOnEmailInput();

            Assert.AreEqual(Pages.AboutMe.GetErrorPasswordText(), b);
        }

        [Test][Retry(3)]
        public void CanSelectRightDay()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectDayOfBirth(13);

            Assert.AreEqual(13.ToString(), Pages.AboutMe.GetDayOfBirth());
        }

        [Test][Retry(3)]
        public void CanSelectRightMonth()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectMonthOfBirth(7);

            Assert.AreEqual(6.ToString(), Pages.AboutMe.GetMonthOfBirth()); //exp result == 6, because css value of 7th month (July) is 6
        }

        [Test][Retry(3)]
        public void CanSelectRightYear()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectYearOfBirth(1997);

            Assert.AreEqual(1997.ToString(), Pages.AboutMe.GetYearOfBirth());
        }

        [Test][Retry(3)]
        public void CanSeeRightAmountOfFeedbackOptions()
        {
            Pages.AboutMe.GoTo();

            Assert.AreEqual(18, Pages.AboutMe.GetFeedbackOptionsNumber());
        }

        [Test][Retry(3)]
        public async Task CanNotRegisterWithAlreadyRegisteredEmail()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndPassedProfileJourney(user);
            Pages.Home.Logout();


            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.Login.WaitUntilLoginUrlIsLoaded();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User has not been redirected to Login page");    
        }


        [Test][Retry(3)]
        public async Task CanSeeRedErrorBanner()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndPassedProfileJourney(user);
            Pages.Home.Logout();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.Login.WaitUntilLoginUrlIsLoaded();

            Assert.AreEqual(Pages.Login.GetErrorBannerText(), "A Neyber account already exists for the email address you entered. Please log in.");
        }

    }
}
