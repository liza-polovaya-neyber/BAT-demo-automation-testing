using System;
using BATDemoFramework;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BATDemoTests.TestCases
{
    [TestFixture]
    public class HelpLinksTests : TestBaseForNewTabWindow
    {
        private IWebDriver driver;

        [Test]
        public void CheckOurTermsLinkOpens()
        {
            Pages.Join.GoTo();
            Pages.Join.GoToOurTermsPage();

            Assert.IsTrue(Pages.OurTerms.IsAtUrl());
        }

        [Test]
        public void CheckPrivacyPolicyLinkOpens()
        {
            Pages.Join.GoTo();
            Pages.Join.GoToPrivacyPolicyPage();

            Assert.IsTrue(Pages.PrivacyPolicy.IsAtUrl());
        }

        [Test]
        public void CheckCookiePolicyLinkOpens()
        {
            Pages.Join.GoTo();
            Pages.Join.GoToCookiePolicyPage();

            Assert.IsTrue(Pages.CookiePolicy.IsAtUrl());  
        }

        [Test]
        public void CheckComplaintsPolicyLinkOpens()
        {
            Pages.Join.GoTo();
            Pages.Join.GoToComplaintsPolicyPage();

            Assert.IsTrue(Pages.ComplaintsPolicy.IsAtUrl());
        }


    }
}
