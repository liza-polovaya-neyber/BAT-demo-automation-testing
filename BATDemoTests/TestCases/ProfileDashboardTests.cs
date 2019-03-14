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
    class ProfileDashboardTests : TestBase
    {
      [Test][Retry(3)]
      public async Task CanLogout()
        {
            await Preconditions.HaveNewUserPassedProfileJourney();

            Pages.Home.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User has not been redirected to login page");
        }

        [Test][Retry(3)]
        public async Task CanNotGoBackToMarketingPage()
        {
            await Preconditions.HaveNewUserPassedProfileJourney();

            Browser.GoToUrl(Urls.Marketing);
            Pages.Home.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User can go back to Marketing page");
        }

    }
}
