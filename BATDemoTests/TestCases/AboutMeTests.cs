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
        public void CanNotSubmitAboutMeForm()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.FillInAboutMeForm();

            //Assert.IsFalse(Pages.AboutMe.SubmitBtnIsDisabled(), "'Submit' button is not enabled");
        }


        [Test][Retry(3)]
        [Ignore("Fix submit reread submit button")]
        public void CanSubmitAboutMeForm()
       {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserButDontTickCheckbox();

            Assert.IsTrue(Pages.AboutMe.SubmitBtnIsDisabled(), "Submit button is enabled despite T&C checkboxes aren't being checked");
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
