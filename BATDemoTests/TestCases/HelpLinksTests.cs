using System;
using BATDemoFramework;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]
    class HelpLinksTests : TestBase
    {
        [Test]
        public void CheckOurTermsLinkWorks()
        {
            Pages.Join.GoTo();
            Pages.Join.GoToOurTermsPage();

            Assert.IsTrue(Pages.OurTerms.IsAtUrl());
        }

        [Test]
        public void CheckPrivacyPolicyLinkWorks()
        {
            Pages.Join.GoTo();
            Pages.Join.GoToPrivacyPolicyPage();

            Assert.IsTrue(Pages.PrivacyPolicy.IsAtUrl());
        }

        [Test]
        public void CheckCookiePolicyLinkWorks()
        {
            Pages.Join.GoTo();
            Pages.Join.GoToCookiePolicyPage();

            Assert.IsTrue(Pages.CookiePolicy.IsAtUrl());
        }

        [Test]
        public void CheckComplaintsPolicyLinkWorks()
        {
            Pages.Join.GoTo();
            Pages.Join.GoToComplaintsPolicyPage();

            Assert.IsTrue(Pages.ComplaintsPolicy.IsAtUrl());
        }

        [Test]
        public void CheckSomeLegalBitsMenuIsDisplayed()
        {
            Pages.Join.GoTo();
            Pages.Join.OpenSomeLegalBitsMenu();

            Assert.IsTrue(Pages.Join.SomeLegalBitsMenuIsDisplayed());
        }

    }
}
