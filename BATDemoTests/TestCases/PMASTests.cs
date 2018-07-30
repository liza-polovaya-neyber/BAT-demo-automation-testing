using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
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
            Pages.ApolloPMAS.AcceptCookiesOnBanner();
            Pages.ApolloPMAS.ClickToFMRTopBtn();

            Assert.AreEqual(Urls.AboutMePMASPage, Browser.Url, "User was not redirected back to Apollo PMAS landing page");
        }

        [Test][Retry(3)]
        public void CanGoFromApolloPMASToAboutMePageMiddleBtn()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesOnBanner();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ClickToApplyMiddleBtn();

            Assert.AreEqual(Urls.AboutMePMASPage, Browser.Url, "User was not redirected back to Apollo PMAS landing page");
        }

        [Test][Retry(3)]
        public void CanGoFromApolloPMASToAboutMePageBottomBtn()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesOnBanner();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ClickToApplyBottomBtn();

            Assert.AreEqual(Urls.AboutMePMASPage, Browser.Url, "User was not redirected back to Apollo PMAS landing page");
        }

        [Test][Retry(3)]
        public void CanGoToEligibilityCriteriaPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesOnBanner();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ShowEligibilityCriteria();

            Assert.True(Pages.EligibilityCriteria.IsAtUrl(), "Userwas not redirected to eligibility criteria page");
        }

        [Test][Retry(3)]
        public void CanApplyThroughEligibilityCriteriaPage()
        {
            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesOnBanner();
            Pages.ApolloPMAS.AcceptCookiesPolicy();
            Pages.ApolloPMAS.ShowEligibilityCriteria();
            Pages.EligibilityCriteria.ClickToApplyNow();

            Assert.AreEqual(Urls.AboutMePMASPage, Browser.Url, "User was not redirected back to Apollo PMAS landing page");
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
         
        [Test]
        [Retry(3)]
        public async Task CanGetToProfileDashboardPage()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesOnBanner();
            Pages.ApolloPMAS.ClickToFMRTopBtn();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.ChoosePostOption();
            Pages.Marketing.ChoosePhoneOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();

            Assert.True(Pages.Home.IsAtUrl(), "User was not able to get to Profile dashboard page");
        }

        [Test]
        [Retry(3)]
        public async Task CanSkipEmployerSearchPage()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.ApolloPMAS.GoToUrl();
            Pages.ApolloPMAS.AcceptCookiesOnBanner();
            Pages.ApolloPMAS.ClickToFMRTopBtn();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();


            Assert.True(Pages.AlternativeEmail.IsAtUrl(), "User was not able to get to Alternative email page");
        }

    }
}
