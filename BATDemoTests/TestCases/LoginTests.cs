using BATDemoFramework;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace BATDemoTests
{
    [TestFixture]
    class LoginTests : TestBase
    {

        [Test]
        public void CanGoToLoginPage()
        {
            Pages.Login.GoToLoginPage();
            Assert.IsTrue(Pages.Login.IsAt());
        }

        [Test]
        public void CanGoFromLoginPageToResetPasswordPage()
        {
            Pages.Login.GoToResetPasswordPage();
            Assert.IsTrue(Pages.ResetPassword.IsAt());
        }

        [Test]
        public void CanGoFromLoginPageToJoinPage()
        {
            Pages.Login.GoToJoinPage();
            Assert.IsTrue(Pages.Join.IsAt());
        }

        [Test]
        public void ValidUserLogsinSuccessfully()
        {
            Pages.Login.GoToLoginPage();
            Pages.Login.LogIn("ValidUserLogsinSuccessfully");
            Assert.IsTrue(Pages.Home.IsAt(), "A valid user was not able to successfully login.");
        }

        [Test]
        public void LoginWithInvalidEmailShouldNotWork()
        {
            Pages.Login.GoToLoginPage();
            Pages.Login.LogIn("LoginWithInvalidEmailShouldNotWork");

            //assert that error is displayed is better rather than user stays on the same page
            //Assert.IsTrue(Pages.Login.IsDisplayed(errorInvalidCredentials));
            Assert.IsTrue(Pages.Login.IsAt(), "User with invalid email is not on Login page");
        }


        [Test]
        public void LoginWithInvalidPasswordShouldNotWork()
        {
            Pages.Login.GoToLoginPage();
            Pages.Login.LogIn("LoginWithInvalidPasswordShouldNotWork");
            Assert.IsTrue(Pages.Login.IsAt(), "User with invalid password is not on Login page");
        }


    }


}
