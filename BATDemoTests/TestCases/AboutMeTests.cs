using BATDemoFramework;
using NUnit.Framework;
using OpenQA.Selenium;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework.Generators;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class AboutMeTests : TestBase
    {
        private IWebElement element;

        [Test]
        public void CanGoBackFromAboutMePagetoJoin()
        {
            Pages.Join.GoTo();
            Pages.Join.GotoAboutMePage();
            Pages.AboutMe.ClickOnBackLink();

            Assert.IsTrue(Pages.Join.IsAtUrl(), "User is not on Join page");
        }

        [Test]
        public void CanGoFromAboutMePageToLogin()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.ClickOnLoginLink();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User is not on Login page");
        }

        [Test]
        public void CanSeeSomeLegalBitsMenu()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.OpenSomeLegalBitsMenu();

            Assert.IsTrue(Pages.AboutMe.SomeLegalBitsMenuIsDisplayed(), "Some Legal bits menu is not opened");
        }

        [Test]
        public void CanGoToLoginPageOnLogoClick()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.ClickOnNeyberLogo();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User is not on Login page");
        }

        [Test]
        public void CanNotEnterDifferentPrimaryEmails()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserWithNotEqualEmails();

            Assert.AreEqual(Pages.AboutMe.GetEmailsMismatchError(), "The email addresses do not match");
        }

        [Test]
        public void CanNotSubmitAboutMeForm()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.FillInAboutMeForm();

            Assert.IsFalse(Pages.AboutMe.SubmitBtnIsDisabled(), "'Submit' button is not enabled");
        }


        [Test]
        [Ignore("Fix submint reread submit button")]
        public void CanSubmitAboutMeForm()
       {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserButDontTickCheckbox();

            Assert.IsFalse(Pages.AboutMe.SubmitBtnIsEnabled(), "Submit button is enabled despite two of the checkboxes are not checked");
        }

        [Test]
        [Ignore("Fix submint reread submit button")]
        public void CanNotSubmitAboutMeFormWithoutTitle()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserWithNonSelectedTitle();

            Assert.IsTrue(Pages.AboutMe.SubmitBtnIsDisabled(), "Submit button is enabled despite 'Titile' DD hasn't been selected");
        }

        [Test]
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

            Assert.AreEqual(Pages.AboutMe.GetErrorText(), b);
        }

        [Test]
        public void CanSelectRightDay()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectDayOfBirth(13);

            Assert.AreEqual(13.ToString(), Pages.AboutMe.GetDayOfBirth());
        }

        [Test]
        public void CanSelectRightMonth()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectMonthOfBirth(7);

            Assert.AreEqual(6.ToString(), Pages.AboutMe.GetMonthOfBirth()); //exp result == 6, because css value of 7th month (July) is 6
        }

        [Test]
        public void CanSelectRightYear()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectYearOfBirth(1997);

            Assert.AreEqual(1997.ToString(), Pages.AboutMe.GetYearOfBirth());
        }

        [Test]
        public void CanSeeRightAmountOfFeedbackOptions()
        {
            Pages.AboutMe.GoTo();

            Assert.AreEqual(17, Pages.AboutMe.GetFeedbackOptionsNumber());
        }

        [Test]
        public void CanNotRegisterWithAlreadyRegisteredEmail()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserFromCsv("CanLogin");
            Pages.Login.WaitUntilLoginUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User has not been redirected to Login page");    
        }


        [Test]
        public void CanSeeRedErrorBanner()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserFromCsv("CanLogin");
            Pages.Login.WaitUntilLoginUrlIsLoaded(Browser.webDriver);

            Assert.AreEqual(Pages.Login.GetErrorBannerText(), "Close");
        }

    }
}
