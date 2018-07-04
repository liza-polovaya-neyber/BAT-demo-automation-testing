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
    class PoliceMutualTests : TestBase
    {
        [Test][Retry(3)]
        public void CanGoFromApolloPMASToAboutMePageTopBtn()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickToApplyTopBtn();

            Assert.True(Pages.AboutMe.IsAtUrl(), "User was not redirected to About Me page from Apollo landing page");
        }

        [Test][Retry(3)]
        public void CanGoFromApolloPMASToAboutMePageMiddleBtn()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickToApplyMiddleBtn();

            Assert.True(Pages.AboutMe.IsAtUrl(), "User was not redirected to About Me page from Apollo landing page");
        }

        [Test][Retry(3)]
        public void CanGoFromApolloPMASToAboutMePageBottomBtn()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ClickToApplyBottomBtn();
 
            Assert.True(Pages.AboutMe.IsAtUrl(), "User was not redirected to About Me page from Apollo landing page");
        }

        [Test][Retry(3)]
        public void CanGoToEligibilityCriteriaPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ShowEligibilityCriteria();

            Assert.True(Pages.EligibilityCriteria.IsAtUrl(), "Userwas not redirected to eligibility criteria page");
        }

        [Test][Retry(3)]
        public void CanApplyThroughEligibilityCriteriaPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ShowEligibilityCriteria();
            Pages.EligibilityCriteria.ClickToApplyNow();

            Assert.True(Pages.AboutMe.IsAtUrl(), "User was not redirected to About Me page");
        }

        [Test][Retry(3)]
        public void CanGoToLogin()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickOnLogin();

            Assert.True(Pages.Login.IsAtUrl(), "User was not redirected to Login page");
        }


        [Test][Retry(3)]
        public void CanGoToGetInTouchPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickOnGetInTouch();

            Assert.True(Pages.GetInTouch.IsAtUrl(), "User was not redirected to the page with contact details");
        }

        [Test][Retry(3)]
        public void CanGoToFAQPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickOnFAQ();
            Pages.FAQ.WaitUntilUrlIsLoaded();

            Assert.True(Pages.FAQ.IsAtUrl(), "User was not redirected to the page with contact details");
        }

        [Test][Retry(3)]
        public void CanComeBackToApolloPMASPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickOnFAQ();
            Pages.FAQ.ClickOnPersonalLoans();

            Assert.True(Pages.ApolloPMAS.IsAtUrl(), "User was not redirected back to Apollo PMAS landing page");
        }

    }
}
