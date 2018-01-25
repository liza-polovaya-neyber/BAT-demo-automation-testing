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
    public class HelpLinksTests : TestBase
    {
        private IWebDriver driver;

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

            // Store the parent window of the driver
            String parentWindowHandle = driver.CurrentWindowHandle;
            Console.WriteLine("Parent window's handle -> " + parentWindowHandle);

            Pages.Join.GoToCookiePolicyPage();
            String lastWindowHandle = driver.CurrentWindowHandle;
            //driver.SwitchTo().Window(driver.WindowHandles.Last());

            Assert.IsTrue(Pages.CookiePolicy.IsAtUrl());

            driver.SwitchTo().Window(parentWindowHandle);
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
