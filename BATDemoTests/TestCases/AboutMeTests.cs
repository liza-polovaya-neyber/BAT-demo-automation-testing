using BATDemoFramework;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class AboutMeTests : TestBase
    {
        private IWebDriver driver;
        private IWebElement element;

        [Test]
        public void AboutMeFormIsFilledIn()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser("AboutMeFormIsFilledIn");

            Assert.IsTrue(Pages.AboutMe.SubmitBtnIsEnabled(), "'Submit' button is not enabled");
        }

        [Test]
        public void AboutMeFormIsSubmited()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser("AboutMeFormIsSubmited");

            Assert.IsTrue()
        }

    


    }
}
