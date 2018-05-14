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
    class ProfileDashboardTests : TestBase
    {
      [Test]
      public async Task CanLogout()
        {
            await Preconditions.HaveNewUserPassedProfileJourney();

            Pages.Home.Logout();

            Assert.IsTrue(Pages.Login.IsAtUrl(), "User has not been redirected to login page");
        }

        [Test]
        public async Task CanNotGoBackToMarketingPage()
        {
            await Preconditions.HaveNewUserPassedProfileJourney();

            Browser.GoToUrl(Urls.Marketing);
            Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.Home.IsAtUrl(), "User can go back to Marketing page");
        }

    }
}
