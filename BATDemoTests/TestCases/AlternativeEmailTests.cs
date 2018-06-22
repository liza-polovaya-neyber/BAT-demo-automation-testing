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
        [Test]
        public async Task CanSetAnAlternativeEmail()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User hasn't been redirected to Marketing page");
        }

        [Test]
        public async Task CanSkipAlternativeEmailPage()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();
     
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User hasn't been redirected to Marketing preferences page");
        }

        [Test]
        public async Task CanNotSetPrimaryEmailAsAlternative()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.NewUserCreated(user);

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();

            Assert.AreEqual(Pages.AlternativeEmail.GetErrorMessage(), "You've already registered with this email. Please provide an alternative");
        }

        [Test]
        public async Task CanNotSetRegisteredEmailAsAlternative()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.EnterEmailFromCsv("CanLogin");
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.AlternativeEmail.WaitUntilRedBannerIsShown(Browser.webDriver);

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

        [Test]
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
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User was not able to verify their alternative email");
        }

        [Test]
        public async Task CanVerifyAlternativeEmailWhenLoggedOut()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Login.WaitUntilLoginUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Login.GetGreenBannerText(), "Green verification banner doesn't contain needed info");
        }

        [Test]
        public async Task CanVerifyAlternativeEmailAndLogBackIn()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();
      
            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.Logout();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.Login.WaitUntilLoginUrlIsLoaded(Browser.webDriver);
            Pages.Login.LogIn(user);
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User was unable to get back to Marketing page");
        }


        [Test]
        public async Task CanLogout()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Pages.AlternativeEmail.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User hasn't been redirected to login page");
        }


        [Test]
        public async Task CanNotSkipAlternativeEmailPage()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Browser.GoToUrl(Urls.Marketing);

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User was able to skip the alternative email page and go to Marketing preferences page");
        }

        [Test]
        public async Task CanNotGoBackToEmployerSearchPage()
        {
            await Preconditions.HaveNewUserCreatedAndSelectedAnEmployer();

            Browser.GoToUrl(Urls.EmployerSearch);

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User was able to go back from the alternative email page to the Employer search page");
        }

    }
}
