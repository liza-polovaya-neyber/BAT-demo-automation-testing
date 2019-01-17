using System;
using BATDemoFramework;
using BATDemoFramework.Generators;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading.Tasks;
using BATDemoFramework.NeyberPages;
using BATDemoFramework.Steps.Given;
using BATDemoFramework.Steps.When;
using BATDemoFramework.Steps.Then;
using BATDemoFramework.NeyberPages.Profile;

namespace BATDemoTests
{
    [TestFixture /*("single", "ie")*/]
   public class LoginTests : TestBase
    {
        private IWebElement element;
        private UserCreator userCreator = new UserCreator();
        private LoginUser loginUser = new LoginUser();
        private UserIsAt userIsAt = new UserIsAt();
        WebDriverWait wait;

        //public LoginTests(string profile, string environment) : base(profile, environment){}

        [Test][Retry(3)]
        public async Task CanCreateUserAndLogin()
        {
            //      Given
            var user = await userCreator.CreateUserAsync();
            //      When
            loginUser.UserLogin(user);
            //      Then
            userIsAt.IsAtEmployerPage();
        }

        [Test][Retry(3)]
        public async Task CanCreateUserAndPassInitialProfileJourney()
        {
            //      Given
            var user = await userCreator.CreateUserAndSetMarketingPreferencesAsync();
            //      When
            loginUser.UserLogin(user);
            //      Then
            userIsAt.IsAtHomePage();
        }

        [Test][Retry(3)]
        public async Task CanCreateUserPassFmrAndLogin()
        {
            //      Given
            var user = await userCreator.CreateUserAndPassFmrAsync();
            //      When
            loginUser.UserLogin(user);
            //      Then
            userIsAt.IsAtHomePage();
        }

        [Test][Retry(3)]
        public async Task CanCreateUserAndSelectEmployer()
        {
            //      Given
            var user = await userCreator.CreateUserAndSelectEmployerAsync();
            //      When
            loginUser.UserLogin(user);
            //      Then
            userIsAt.IsAtAlternativeEmailPage();
        }

        [Test][Retry(3)]
        public async Task CanCreateUserAndSkipAlternativeEmail()
        {
            //      Given
            var user = await userCreator.CreateUserAndSkipAlternativeEmailAsync();
            //      When
            loginUser.UserLogin(user);
            //      Then
            userIsAt.IsAtMarketingPage();
        }

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