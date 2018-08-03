using BATDemoFramework;
using BATDemoFramework.Generators;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class EmployerSearchTests : TestBase
    {
        [Test][Retry(3)]
        public async Task CanSelectAnEmployer()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();

            Assert.IsTrue(Pages.AlternativeEmail.IsAtUrl(), "User is not on an alternative email page");
        }

        [Test][Retry(3)]
        public async Task CanGetValidationError()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.EnterTextIntoSearchbox("r");
            Pages.EmployerSearch.ClickOnSearchBtn();

            Assert.AreEqual(Pages.EmployerSearch.GetErrorText(), "Field must contain 2 or more characters");

        }

        [Test][Retry(3)]
        public async Task EmployerNotFound()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.EnterTextIntoSearchbox("qwerty");
            Pages.EmployerSearch.ClickOnSearchBtn();
            Pages.EmployerSearch.WaitUntilPhoneNumberFieldAppears();
            
            Assert.IsTrue(Pages.EmployerSearch.EmployerNoutFoundBlockIsShown(), "Employer not found block doesn't show up");
        }

        [Test][Retry(3)]
        public async Task CanBeFoundMoreThan10Employers()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.EnterTextIntoSearchbox("re");
            Pages.EmployerSearch.ClickOnSearchBtn();

            Assert.IsTrue(Pages.EmployerSearch.WarningBlockIsShown(), "Found more than 10 employers block doesn't show up");
        }

        [Test][Retry(3)]
        public async Task CanEnterPhoneNumberWhenEmployerNotFound()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.EnterTextIntoSearchbox("qwerty");
            Pages.EmployerSearch.ClickOnSearchBtn();
            Pages.EmployerSearch.WaitUntilPhoneNumberFieldAppears();
            Pages.EmployerSearch.EnterPhoneNumber("07523698747");
            Pages.EmployerSearch.ClickToSubmitPhoneNo();

            Assert.IsTrue(Pages.EmployerSearch.ThankYouBlockIsShown(), "Thank you block is not shown");
        }

        [Test][Retry(3)]
        public async Task CanRefineSearch()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.EnterTextIntoSearchbox("Bupa");
            Pages.EmployerSearch.ClickOnSearchBtn();
            Pages.EmployerSearch.WaitUntilSearchResultsAppear();
            Pages.EmployerSearch.SelectEmployer();
            Pages.EmployerSearch.ClickOnSelectResultBtn();
            Pages.EmployerSearch.ClickOnRefineSearchLink();

            Assert.IsTrue(Pages.EmployerSearch.InputFieldIsShown(), "User is not able to search for an employer again");
        }



       [Test][Retry(3)]
        public async Task CanLogoutAndLogbackIn()
            {
               var user = new UserGenerator().GetNewUser();
               await Preconditions.NewUserCreatedAndVerifiedEmail(user);

                Pages.EmployerSearch.Logout();
                Pages.Login.LogIn(user);
                Pages.EmployerSearch.WaitUntilUrlIsLoaded();

                Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User wan't able to log out");
            }

        [Test][Retry(3)]
        public async Task CanNotSkipEmployerPage()
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Browser.GoToUrl(Urls.AlternativeEmail);

            Assert.IsTrue(Pages.EmployerSearch.IsAtUrl(), "User was able to skip the employer search page and go to Alternative email page");
        }
 
    }
}
