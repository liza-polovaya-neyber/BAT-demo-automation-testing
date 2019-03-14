using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework;
using BATDemoFramework.Generators;
using BATDemoFramework.NeyberPages;
using BATDemoFramework.Steps.Given;
using BATDemoTests.Validators;
using NUnit.Framework;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class SalesforceTests : TestBase
    {
        private UserCreator userCreator = new UserCreator();

        [Test][Retry(3)]
        public async Task CreateUserAndCompareWithSalesForce()
        {
            var user = await userCreator.CreateUserWithTenantAsync();
            var profileValidator = this.GetService<ProfileValidator>();
            await profileValidator.AssertCreatedProfileState(user);
        }

        [Test][Retry(3)]
        public async Task CreateSSOUserAndCompareWithSalesForce()
        {
            var user = UserGenerator.GetNewSSOUser();
            var newUser = new UserGenerator().GetNewUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
            Pages.SSOAccountConfirm.ClickToContinue();
            Pages.SSOAboutMe.RegisterUserWithAllFieldsFilledIn(newUser);
            Pages.SSOAboutMe.PressSubmitButton();
            Pages.SSOAdditionalDetails.WaitUntilUrlIsLoaded();
            Pages.SSOAdditionalDetails.Logout();
            var profileValidator = this.GetService<ProfileValidator>();
            await profileValidator.AssertCreatedProfileStateSSO(user, newUser);
        }
    }
}
