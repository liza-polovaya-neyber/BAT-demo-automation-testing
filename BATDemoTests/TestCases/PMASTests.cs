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
        [Test]
        public void CanGoFromApolloPMASToAboutMePageTopBtn()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickToApplyTopBtn();

            Assert.True(Pages.AboutMe.IsAtUrl(), "User was not redirected to About Me page from Apollo landing page");
        }

        [Test]
        public void CanGoFromApolloPMASToAboutMePageMiddleBtn()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickToApplyMiddleBtn();

            Assert.True(Pages.AboutMe.IsAtUrl(), "User was not redirected to About Me page from Apollo landing page");
        }

        [Test]
        public void CanGoFromApolloPMASToAboutMePageBottomBtn()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ClickToApplyBottomBtn();
 
            Assert.True(Pages.AboutMe.IsAtUrl(), "User was not redirected to About Me page from Apollo landing page");
        }

        [Test]
        public void CanGoToEligibilityCriteriaPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ShowEligibilityCriteria();

            Assert.True(Pages.EligibilityCriteria.IsAtUrl(), "Userwas not redirected to eligibility criteria page");
        }

        [Test]
        public void CanApplyThroughEligibilityCriteriaPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ShowEligibilityCriteria();
            Pages.EligibilityCriteria.ClickToApplyNow();

            Assert.True(Pages.AboutMe.IsAtUrl(), "User was not redirected to About Me page");
        }

        [Test]
        public void CanGoToLogin()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickOnLogin();

            Assert.True(Pages.Login.IsAtUrl(), "User was not redirected to Login page");
        }


        [Test]
        public void CanGoToGetInTouchPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickOnGetInTouch();

            Assert.True(Pages.GetInTouch.IsAtUrl(), "User was not redirected to the page with contact details");
        }

        [Test]
        public void CanGoToFAQPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickOnFAQ();
            Pages.FAQ.WaitUntilUrlIsLoaded(Browser.webDriver);

            Assert.True(Pages.FAQ.IsAtUrl(), "User was not redirected to the page with contact details");
        }

        [Test]
        public void CanComeBackToApolloPMASPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.ClickOnFAQ();
            Pages.FAQ.ClickOnPersonalLoans();

            Assert.True(Pages.ApolloPMAS.IsAtUrl(), "User was not redirected back to Apollo PMAS landing page");
        }

    }
}
