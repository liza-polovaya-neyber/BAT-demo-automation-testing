using BATDemoFramework;
using NUnit.Framework;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BATDemoTests
{
    [TestFixture]
   public class LoginTests : TestBase
    {
        private IWebDriver driver;
        private IWebElement element;

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
        public void ValidUserLogsinSuccessfully()
        {
            Pages.Login.GoTo();
            Pages.Login.LogIn("ValidUserLogsinSuccessfully");

            Assert.IsTrue(Pages.Home.IsAt(Browser.webDriver), "Valid user is not on Home page");
            //Assert.IsTrue(Pages.Home.UserAvatarIsDisplayed(), "User avatar is not found");
            //Assert.IsTrue(Pages.Home.IsAtUrl(), "A valid user was not able to successfully login.");
        }

        [Test]
        public void LoginWithInvalidEmailShouldNotWork()
        {
            Pages.Login.GoTo();  
            Pages.Login.LogIn("LoginWithInvalidEmailShouldNotWork");

            //Assert.IsTrue(Pages.Login.GetErrorText().Contains("The email address or password you entered is incorrect. Please check and try again."));
            Assert.IsTrue(Pages.Login.ErrorBlockIsShown(Browser.webDriver), "Error block is not shown");
        }


        [Test]
        public void LoginWithInvalidPasswordShouldNotWork()
        {
            Pages.Login.GoTo();
            Pages.Login.LogIn("LoginWithInvalidPasswordShouldNotWork");

            Assert.IsTrue(Pages.Login.ErrorBlockIsShown(Browser.webDriver), "Error block is not shown");
            Assert.IsTrue(Pages.Login.IsAtUrl(), "User with invalid password is not on Login page");
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
