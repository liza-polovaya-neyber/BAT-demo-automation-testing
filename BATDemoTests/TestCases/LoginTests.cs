﻿using BATDemoFramework;
using NUnit.Framework;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace BATDemoTests
{
    [TestFixture]
    class LoginTests : TestBase
    {

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

            Assert.IsTrue(Pages.Home.IsAtUrl(), "A valid user was not able to successfully login.");
        }

        [Test]
        public void LoginWithInvalidEmailShouldNotWork()
        {
            Pages.Login.GoTo();
            Pages.Login.LogIn("LoginWithInvalidEmailShouldNotWork");

            Assert.IsTrue(Pages.Login.GetText().Contains("The email address or password you entered is incorrect. Please check and try again."));
        }


        [Test]
        public void LoginWithInvalidPasswordShouldNotWork()
        {
            Pages.Login.GoTo();
            Pages.Login.LogIn("LoginWithInvalidPasswordShouldNotWork");

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User with invalid password is not on Login page");
        }


    }


}
