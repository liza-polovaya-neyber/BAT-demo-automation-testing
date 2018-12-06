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
using BATDemoFramework.Generators;

namespace BATDemoTests
{
   [TestFixture /*("single", "ie")*/]
   public class LoginTests : TestBase
    {
        private IWebElement element;
        WebDriverWait wait;

        //public LoginTests(string profile, string environment) : base(profile, environment){}

        [Test][Retry(3)]
        public void CanGoToLoginPage()
        {
            Pages.Login.GoTo();

            Assert.IsTrue(Pages.Login.IsAtUrl());
        }

        [Test][Retry(3)]
        public void CanGoFromLoginPageToResetPasswordPage()
        {
            Pages.Login.GoTo();
            Pages.Login.GoToResetPasswordPage();

            Assert.IsTrue(Pages.ResetPassword.IsAtUrl());
        }

        [Test][Retry(3)]
        public void CanGoFromLoginPageToJoinPage()
        {
            Pages.Login.GoTo();
            Pages.Login.GoToJoinPage();

            Assert.IsTrue(Pages.Join.IsAtUrl());
        }

        [Test][Retry(3)]
        public async Task CanLogin()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndPassedProfileJourney(user);
            Pages.Home.Logout();

            Pages.Login.GoTo();
            Pages.Login.LogIn(user);
            Pages.Home.WaitUntilHomeUrlIsLoaded();


            Assert.IsTrue(Pages.Home.IsAtUrl(), "Valid user is not on Hpage");     
        }

        [Test][Retry(3)]
        public void CanNotLoginWithNonRegisteredEmail()
        {
            Pages.Login.GoTo();  
            Pages.Login.LoginByRandomUser();
            Pages.Login.WaitUntilErrorBlockIsShown();
            
            Assert.AreEqual(Pages.Login.GetErrorText(), "The email address or password you entered is incorrect. Please check and try again.");
        }


        [Test][Retry(3)]
        public void CanNotLoginWithWrongPassword()
        {
            Pages.Login.GoTo();
            Pages.Login.LoginByUserWithWrongPassword();
            Pages.Login.WaitUntilErrorBlockIsShown();

            Assert.AreEqual(Pages.Login.GetErrorText(), "The email address or password you entered is incorrect. Please check and try again.");
        }

        [Test][Retry(3)]
        public void CheckSomeLegalBitsMenuIsDisplayed()
        {
            Pages.Join.GoTo();
            Pages.Join.OpenSomeLegalBitsMenu();

            Assert.IsTrue(Pages.Join.SomeLegalBitsMenuIsDisplayed());
        }
    }
}
