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
        private IWebDriver driver;
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

        public void SomeLegalBitsIsShown()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.OpenSomeLegalBitsMenu();

            Assert.IsTrue(Pages.AboutMe.SomeLegalBitsMenuIsDisplayed(), "Some Legal bits menu is not opened");
        }

        [Test]
        public void ClickOnLogoRedirectsToLoginPage()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.ClickOnNeyberLogo();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User is not on Login page");
        }

        [Test]
        public void CheckPrimaryEmailsAreCheckedToBeEqual()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserWithNotEqualEmails();

            Assert.IsTrue(Pages.AboutMe.EmailsDontMatchErrorIsDisplayed(), "No error shown on different email addresses entered as primary email");
        }

        [Test]
        public void AboutMeFormIsReadyToBeSubmitted()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.FillInAboutMeForm();

            Assert.IsFalse(Pages.AboutMe.SubmitBtnIsDisabled(), "'Submit' button is not enabled");
        }


        [Test]
        public void AboutMeFormCantBeSubmitted()
       {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserButDontTickCheckbox();

            Assert.IsFalse(Pages.AboutMe.SubmitBtnIsEnabled(), "Submit button is enabled despite two of the checkboxes are not checked");
        }

        [Test]
        public void AboutMeFormCantBeSubmittedWithoutTitle()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterUserWithNonSelectedTitle();

            Assert.IsTrue(Pages.AboutMe.SubmitBtnIsDisabled(), "Submit button is enabled despite 'Titile' DD hasn't been selected");
        }

        [Test]
        public void RightTitleIsSelected()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectTitle(TitleType.Ms);

            Assert.AreEqual(TitleType.Ms, Pages.AboutMe.GetTitleText());
        }

        [Test]
        public void RightDayIsSelected()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectDayOfBirth(13);

            Assert.AreEqual(13.ToString(), Pages.AboutMe.GetDayOfBirth());
        }

        [Test]
        public void RightMonthIsSelected()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectMonthOfBirth(7);

            Assert.AreEqual(6.ToString(), Pages.AboutMe.GetMonthOfBirth()); //exp result == 6, because css value of 7th month (July) is 6
        }

        [Test]
        public void RightYearIsSelected()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.SelectYearOfBirth(1997);

            Assert.AreEqual(1997.ToString(), Pages.AboutMe.GetYearOfBirth());
        }

        [Test]
        public void RightAmountOfFeedbackOptions()
        {
            Pages.AboutMe.GoTo();

            Assert.AreEqual(17, Pages.AboutMe.GetFeedbackOptionsNumber());
        }

    }
}
