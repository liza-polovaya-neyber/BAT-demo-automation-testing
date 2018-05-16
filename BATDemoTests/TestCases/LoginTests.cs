using BATDemoFramework;
using NUnit.Framework;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BATDemoFramework.BrowserStackTest;
using System.Threading.Tasks;

namespace BATDemoTests
{
   [TestFixture /*("single", "ie")*/]
   public class LoginTests : TestBase
    {
        private IWebElement element;

        //public LoginTests(string profile, string environment) : base(profile, environment){}

        [Test]
        public void CanGoToLoginPage()
        {
            Pages.Login.GoTo();

            Assert.IsTrue(Pages.Login.IsAtUrl());
        }

        [Test]
        public void CanGoFromLoginPageToResetPasswordPage()
        {
            Pages.Login.GoTo();
            Pages.Login.GoToResetPasswordPage();

            Assert.IsTrue(Pages.ResetPassword.IsAtUrl());
        }

        [Test]
        public void CanGoFromLoginPageToJoinPage()
        {
            Pages.Login.GoTo();
            Pages.Login.GoToJoinPage();

            Assert.IsTrue(Pages.Join.IsAtUrl());
        }

        [Test]
        public void CanLogin()
        {
            Pages.Login.GoTo();
            Pages.Login.LogInFromCsv("CanLogin");

            Assert.IsTrue(Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver), "Valid user is not on Hpage");     
        }

        [Test]
        public void CanNotLoginWithNonRegisteredEmail()
        {
            Pages.Login.GoTo();  
            Pages.Login.LoginByRandomUser();
            Pages.Login.WaitUntilErrorBlockIsShown(Browser.webDriver);
            
            Assert.AreEqual(Pages.Login.GetErrorText(), "The email address or password you entered is incorrect. Please check and try again.");
        }


        [Test]
        public void CanNotLoginWithWrongPassword()
        {
            Pages.Login.GoTo();
            Pages.Login.LoginByUserWithWrongPassword();
            Pages.Login.WaitUntilErrorBlockIsShown(Browser.webDriver);

            Assert.AreEqual(Pages.Login.GetErrorText(), "The email address or password you entered is incorrect. Please check and try again.");
        }

        [TestCase("ukr", "Must be more than 8 characters")]
        [TestCase("lowercase", "Must contain at least one upper letter, one lower letter and one number")]
        [TestCase("lowecaseP", "Must contain at least one upper letter, one lower letter and one number")]
        [TestCase("lowecase10", "Must contain at least one upper letter, one lower letter and one number")]
        public void GetsValidationMessageWhenInvalidPassword(string a, string b)
        {
            Pages.Login.GoTo();
            Pages.Login.EnterPassword(a);
            Pages.Login.ClickToShowHidePassword();

            Assert.AreEqual(Pages.AboutMe.GetErrorMessage(), b);
        }

        [Test]
        public void CheckSomeLegalBitsMenuIsDisplayed()
        {
            Pages.Join.GoTo();
            Pages.Join.OpenSomeLegalBitsMenu();

            Assert.IsTrue(Pages.Join.SomeLegalBitsMenuIsDisplayed());
        }
    }
}
