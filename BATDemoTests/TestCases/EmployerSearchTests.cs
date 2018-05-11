using BATDemoFramework;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class EmployerSearchTests : TestBase
    {
        [Test]
        public async Task CanSelectAnEmployer()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectAnEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User is not on an alternative email page");
        }

        [Test]
        public async Task CanGetValidationError()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.EnterTextIntoSearchbox("r");
            Pages.EmployerSearch.ClickOnSearchBtn();

            Assert.IsTrue(Pages.EmployerSearch.ErrorIsShown(), "There is no validation error message shown");

        }

        [Test]
        public async Task EmployerNotFound()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.EnterTextIntoSearchbox("qwerty");
            Pages.EmployerSearch.ClickOnSearchBtn();
            Pages.EmployerSearch.WaitUntilPhoneNumberFieldAppears(Browser.webDriver);

            Assert.IsTrue(Pages.EmployerSearch.EmployerNoutFoundBlockIsShown(), "Employer not found block doesn't show up");
        }

        [Test]
        public async Task CanEnterPhoneNumberWhenEmployerNotFound()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.EnterTextIntoSearchbox("qwerty");
            Pages.EmployerSearch.ClickOnSearchBtn();
            Pages.EmployerSearch.WaitUntilPhoneNumberFieldAppears(Browser.webDriver);
            Pages.EmployerSearch.EnterPhoneNumber("07523698747");
            Pages.EmployerSearch.ClickToSubmitPhoneNo();

            Assert.IsTrue(Pages.EmployerSearch.ThankYouBlockIsShown(), "Thank you block is not shown");
        }

        [Test]
        public async Task CanRefineSearch()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.EnterTextIntoSearchbox("Bupa");
            Pages.EmployerSearch.ClickOnSearchBtn();
            Pages.EmployerSearch.WaitUntilSearchResultsAppear(Browser.webDriver);
            Pages.EmployerSearch.SelectBupa();
            Pages.EmployerSearch.ClickOnSelectResultBtn();
            Pages.EmployerSearch.ClickOnRefineSearchLink();

            Assert.IsTrue(Pages.EmployerSearch.InputFieldIsShown(), "User is not able to search for an employer again");
        }



       [Test]
        public async Task CanLogout()
            {
                await Preconditions.HaveNewUserCreated();

                Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
                Pages.EmployerSearch.Logout();

                Assert.IsTrue(Pages.Login.IsAtUrl(), "User wan't able to log out");
            }

        [Test]
        public async Task CanNotSkipEmployerPage()
        {
            await Preconditions.HaveNewUserCreated();

            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Browser.GoToUrl(Urls.AlternativeEmail);

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User was able to skip the employer search page and go to Alternative email page");
        }
 
    }
}
