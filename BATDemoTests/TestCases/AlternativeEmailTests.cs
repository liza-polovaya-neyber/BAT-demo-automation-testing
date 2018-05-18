using BATDemoFramework;
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
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User hasn't been redirected to Marketing page");
        }

        [Test]
        public async Task CanSkipAlternativeEmailPage()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
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
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.EnterEmail(user.EmailAddress);
            Pages.AlternativeEmail.ClickOnSubmitBtn();

            Assert.AreEqual(Pages.AlternativeEmail.GetErrorMessage(), "You've already registered with this email. Please provide an alternative");
        }

        [Test]
        public async Task CanNotSetRegisteredEmailAsAlternative()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilPageIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.EnterEmailFromCsv("CanLogin");
            Pages.AlternativeEmail.ClickOnSubmitBtn();
            Pages.AlternativeEmail.WaitUntilRedBannerIsShown(Browser.webDriver);

            Assert.IsTrue(Pages.AlternativeEmail.GetErrorBannerMessage(), "dgfg");
        }

        [TestCase("email", "Please enter a valid email address")]
        [TestCase("", "Please enter your email address")]
        public async Task CanNotEnterInvalidEmail(string a, string b)
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilPageIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.EnterEmail(a);
            Pages.AlternativeEmail.ClickOnSubmitBtn();

            Assert.AreEqual(Pages.AlternativeEmail.GetErrorMessage(), b);
        }


        [Test]
        public async Task CanLogout()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User hasn't been redirected to login page");
        }


        [Test]
        public async Task CanNotSkipAlternativeEmailPage()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Browser.GoToUrl(Urls.Marketing);

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User was able to skip the alternative email page and go to Marketing preferences page");
        }

        [Test]
        public async Task CanNotGoBackToEmployerSearchPage()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Browser.GoToUrl(Urls.EmployerSearch);

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User was able to go back from the alternative email page to the Employer search page");
        }

    }
}
