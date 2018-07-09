using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]
    class AlternativeEmailTests : TestBase
    {
        [Test][Retry(3)]
        public async Task CanSetAnAlternativeEmail()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User hasn't been redirected to Marketing page");
        }

        [Test][Retry(3)]
        public async Task CanSkipAlternativeEmailPage()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();
     
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User hasn't been redirected to Marketing preferences page");
        }

        [Test][Retry(3)]
        public async Task CanNotSetPrimaryEmailAsAlternative()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreatedAndVerifiedEmail(user);

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded();
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();
            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();

            Assert.AreEqual(Pages.AlternativeEmail.GetErrorMessage(), "You've already registered with this email. Please provide an alternative");
        }

        [Test][Retry(3)]
        public async Task CanNotSetRegisteredEmailAsAlternative()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.EnterEmailFromCsv("CanLogin");
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.AlternativeEmail.WaitUntilRedBannerIsShown();

            Assert.IsTrue(Pages.AlternativeEmail.GetErrorBannerMessage(), "dgfg");
        }

        [TestCase("email", "Please enter a valid email address")]
        [TestCase("", "Please enter your email address")]
        public async Task CanNotEnterInvalidEmail(string a, string b)
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.EnterEmail(a);
            Pages.AlternativeEmail.ClickOnSubmitBtn();

            Assert.AreEqual(Pages.AlternativeEmail.GetErrorMessage(), b);
        }

        [Test][Retry(3)]
        public async Task CanVerifyAlternativeEmail()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();
 
            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User was not able to verify their alternative email");
        }

        [Test][Retry(3)]
        public async Task CanVerifyAlternativeEmailWhenLoggedOut()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Login.WaitUntilLoginUrlIsLoaded();

            Assert.IsTrue(Pages.Login.GetGreenBannerText(), "Green verification banner doesn't contain needed info");
        }

        [Test][Retry(3)]
        public async Task CanVerifyAlternativeEmailAndLogBackIn()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();
      
            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Login.WaitUntilLoginUrlIsLoaded();
            Pages.Login.LogIn(user);
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User was unable to get back to Marketing page");
        }


        [Test][Retry(3)]
        public async Task CanLogout()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User hasn't been redirected to login page");
        }


        [Test][Retry(3)]
        public async Task CanNotSkipAlternativeEmailPage()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Browser.GoToUrl(Urls.Marketing);

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User was able to skip the alternative email page and go to Marketing preferences page");
        }

        [Test][Retry(3)]
        public async Task CanNotGoBackToEmployerSearchPage()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Browser.GoToUrl(Urls.EmployerSearch);

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User was able to go back from the alternative email page to the Employer search page");
        }

    }
}
