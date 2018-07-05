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
    class SSOProfileJourneyTests : TestBase
    {
        [Test][Retry(3)]
        public async Task CanNotContinueProfileJourneyWithSSOEmployer()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.SelectEnteredEmployer("Aliaxis");
            Pages.SSOLoginRequired.WaitUntilUrlIsLoaded();

            Assert.IsTrue(Pages.SSOLoginRequired.IsAtUrl(), "User is not on a page that requires login from benefit platform");
        }
    }
}
