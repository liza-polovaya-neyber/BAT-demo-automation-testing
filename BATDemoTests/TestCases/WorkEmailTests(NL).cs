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
    class WorkEmailTests_NL_ : TestBase
    {
        [Test]
        public async Task CanNotVerifyNonExistingWorkEmail()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.SelectEnteredEmployer("Foster Denovo");
            Pages.WorkEmail.WaitUntilWorkEmailUrlIsLoaded(Browser.webDriver);
            Pages.WorkEmail.EnterEmail(user.EmailAddress);
            Pages.WorkEmail.Submit();

            Pages.EmployerVerification.WaitUntilEmployerVerificationUrlIsLoaded(Browser.webDriver);

            Assert.IsTrue(Pages.EmployerVerification.IsAtUrl(), "User was not able to proceed to Employer Verification page");
        }


        [Test]
        public async Task CanTickConsentOnWorkEmailPage()
        {
            var user = new UserGenerator().GetNewUser();
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.SelectEnteredEmployer("Foster Denovo");
            Pages.WorkEmail.WaitUntilWorkEmailUrlIsLoaded(Browser.webDriver);
            Pages.WorkEmail.EnterEmail(user.EmailAddress);
            Pages.WorkEmail.Submit();

            Pages.EmployerVerification.WaitUntilEmployerVerificationUrlIsLoaded(Browser.webDriver);
            Pages.EmployerVerification.CheckConsentCheckbox();
            Pages.EmployerVerification.Submit();
            Pages.EmployerVerification.WaitUntilThankYouBlockIsVisible(Browser.webDriver);

            Assert.IsTrue(Pages.EmployerVerification.ThankYouBlockIsShown(), "Thank you block is not shown");
        }

        [TestCase("", "Please enter your email address")]
        [TestCase("testemail", "Please enter a valid email address")]
        //[TestCase("testemail@hello", "Please enter a valid email address")]
        public async Task CanNotEnterInvalidEmail(string a, string b)
        {
            await Preconditions.HaveNewUserCreatedAndVerifiedEmail();

            Pages.EmployerSearch.SelectEnteredEmployer("Foster Denovo");
            Pages.WorkEmail.WaitUntilWorkEmailUrlIsLoaded(Browser.webDriver);
            Pages.WorkEmail.EnterEmail(a);
            Pages.WorkEmail.Submit();

            Assert.AreEqual(Pages.AboutMe.GetErrorText(), b);
        }

    }
}
