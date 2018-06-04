using BATDemoFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]
    class MarketingPreferencesTests : TestBase
    {
        [Test]
        public async Task CanGoToDashboard()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test]
        public async Task CanSelectSMSOption()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.ChooseSMSOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test]
        public async Task CanSelectEmailOption()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.ChooseEmailOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test]
        public async Task CanSelectPostOption()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.ChoosePostOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test]
        public async Task CanSelectPhoneOption()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.ChoosePhoneOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test]
        public async Task CanSelectAllOptions()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.ChooseEmailOption();
            Pages.Marketing.ChooseSMSOption();
            Pages.Marketing.ChoosePostOption();
            Pages.Marketing.ChoosePhoneOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test]
        public async Task CanLogout()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test]
        public async Task CanNotSkipMarketingPage()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Browser.GoToUrl(Urls.HomePage);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User was able to skip the marketing preferences page and go to Profile dashboard page");
        }

        [Test]
        public async Task CanNotGoBackFromMarketingPage()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Browser.GoToUrl(Urls.AlternativeEmail);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User was able to skip the marketing preferences page and go back to Alternative email page");
        }


    }
}
