using BATDemoFramework;
using BATDemoFramework.EmailServices;
using BATDemoFramework.Generators;
using BATDemoFramework.NeyberPages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]
    class AdditionalDetailsTests : TestBase
    {
        [Test][Retry(3)]
        public async Task CanSkipAdditionalDeatilsPageAndGoToEmployerPage()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User hasn't been redirected to the Employer search page");
        }

        [Test]
        [Retry(3)]
        public async Task CanSetAnAlternateEmail()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.EnterEmail(user.EmailAddress);
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User hasn't been redirected to the Employer Search page");
        }

        [Test]
        [Retry(3)]
        public async Task CanNotSetPrimaryEmailAsAlternative()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);

            Pages.AdditionalDetails.WaitUntilUrlIsLoaded();
            Pages.AdditionalDetails.EnterEmail(user.EmailAddress);
            Pages.AdditionalDetails.ClickOnSubmitBtn();

            Assert.AreEqual(Pages.AdditionalDetails.GetErrorMessage(), "You've already registered with this email. Please provide an alternative");
        }

        [TestCase("email", "Please enter a valid email address")]
        [TestCase("email@", "Please enter a valid email address")]
        public async Task CanNotEnterInvalidEmail(string a, string b)
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.EnterEmail(a);
            Pages.AdditionalDetails.ClickOnSubmitBtn();

            Assert.AreEqual(Pages.AdditionalDetails.GetErrorMessage(), b);
        }

        [Test]
        [Retry(3)]
        public async Task CanVerifyAlternateEmail()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.EnterEmail(user.EmailAddress);
            Pages.AdditionalDetails.ClickOnSubmitBtn();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User was not able to verify their alternative email");
        }

        [Test]
        [Retry(3)]
        public async Task CanVerifyAlternateEmailWhenLoggedOut()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.EnterEmail(user.EmailAddress);
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Login.WaitUntilLoginUrlIsLoaded();

            Assert.IsTrue(Pages.Login.GetGreenBannerText(), "Green verification banner doesn't contain needed info");
        }

        [Test]
        [Retry(3)]
        public async Task CanVerifyAlternativeEmailAndLogBackIn()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.EnterEmail(user.EmailAddress);
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Login.WaitUntilLoginUrlIsLoaded();
            Pages.Login.LogIn(user);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User was unable to get back to the Employer Search page");
        }

        [Test]
        [Retry(3)]
        public async Task CanGetToExpiredLinkPage()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.EnterEmail(user.EmailAddress);
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Login.WaitUntilLoginUrlIsLoaded();
            Browser.GoToUrl(urlToken);
            Pages.ExpiredLink.WaitUntilPageIsLoaded();

            Assert.IsTrue(Pages.ExpiredLink.IsAtUrl(), "User wasn't able to get to expired link page after clicking verification link twice in a row");
        }

        [Test]
        [Retry(3)]
        public async Task CanSelectSMSOption()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.ChooseSMSOption();
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User hasn't been redirected to the Employer search page");
        }

        [Test]
        [Retry(3)]
        public async Task CanSelectEmailOption()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.ChooseEmailOption();
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User hasn't been redirected to the Employer search page");
        }

        [Test]
        [Retry(3)]
        public async Task CanSelectPostOption()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.ChoosePostOption();
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User hasn't been redirected to the Employer search page");
        }

        [Test]
        [Retry(3)]
        public async Task CanSelectPhoneOption()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.ChoosePhoneOption();
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User hasn't been redirected to the Employer search page");
        }

        [Test]
        [Retry(3)]
        public async Task CanSetAllFields()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.ChooseEmailOption();
            Pages.AdditionalDetails.ChooseSMSOption();
            Pages.AdditionalDetails.ChoosePostOption();
            Pages.AdditionalDetails.ChoosePhoneOption();
            Pages.AdditionalDetails.EnterEmail(user.EmailAddress);
            Pages.AdditionalDetails.SelectHowYouHeardAboutUs("Colleague");
            Pages.AdditionalDetails.ClickOnSubmitBtn();
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User hasn't been redirected to the Employer search page");
        }

        [Test]
        [Retry(3)]
        public async Task CanLogout()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test]
        [Retry(3)]
        public async Task CanSeeRightAmountOfFeedbackOptions()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.AdditionalDetails.WaitUntilUrlIsLoaded();

            Assert.AreEqual(18, Pages.AdditionalDetails.GetFeedbackOptionsNumber());
        }

        [Test]
        [Retry(3)]
        public async Task CanNotBypassAdditionalDetailsPage()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Browser.GoToUrl(Urls.HomePage);

            Assert.IsTrue(Pages.AdditionalDetails.IsAtUrl(), "User was able to skip the Additional Details page and go to Profile dashboard page");
        }

        [Test]
        [Retry(3)]
        public async Task CanNotGoBackFromAdditionalDeatilsPage()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Browser.GoToUrl(Urls.AboutMePage);

            Assert.IsTrue(Pages.AdditionalDetails.IsAtUrl(), "User was able to skip the Additional Details page and go back to the About Me page");
        }
    }
}
