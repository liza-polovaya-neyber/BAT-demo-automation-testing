using BATDemoFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework.NeyberPages;
using BATDemoFramework.Pages;

namespace BATDemoTests.TestCases
{
    [TestFixture]
    class MarketingPreferencesTests : TestBase
    {
        [Test][Retry(3)]
        public async Task CanGoToDashboard()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();

            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test][Retry(3)]
        public async Task CanSelectSMSOption()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();

            Pages.Marketing.ChooseSMSOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test][Retry(3)]
        public async Task CanSelectEmailOption()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();

            Pages.Marketing.ChooseEmailOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test][Retry(3)]
        public async Task CanSelectPostOption()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();

            Pages.Marketing.ChoosePostOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test][Retry(3)]
        public async Task CanSelectPhoneOption()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();

            Pages.Marketing.ChoosePhoneOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test][Retry(3)]
        public async Task CanSelectAllOptions()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();

            Pages.Marketing.ChooseEmailOption();
            Pages.Marketing.ChooseSMSOption();
            Pages.Marketing.ChoosePostOption();
            Pages.Marketing.ChoosePhoneOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test][Retry(3)]
        public async Task CanLogout()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();

            Pages.Marketing.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User hasn't been redirected to login page");
        }

        [Test][Retry(3)]
        public async Task CanNotSkipMarketingPage()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();

            Browser.GoToUrl(Urls.HomePage);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User was able to skip the marketing preferences page and go to Profile dashboard page");
        }

        [Test][Retry(3)]
        public async Task CanNotGoBackFromMarketingPage()
        {
            await Preconditions.HaveNewUserCreatedAndSkippedAlternativeEmail();
  
            Browser.GoToUrl(Urls.AlternativeEmail);

            Assert.IsTrue(Pages.Marketing.IsAtUrl(), "User was able to skip the marketing preferences page and go back to Alternative email page");
        }

    }
}
