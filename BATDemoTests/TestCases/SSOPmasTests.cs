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
    class SSOPmasTests : TestBase
    {
        //private IJavaScriptExecutor driver;

        //[Test]
        //public void CanGetToAccountConfirmPage()
        //{
        //    Browser.GoToUrl("https://stubidp.sustainsys.com/");
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    string script = "";
        //    js.ExecuteScript(script);

        //}

        [Test]
        public void CanGetToAccountConfirmPage()
        {
            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit();
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();

            Assert.True(Pages.SSOAccountConfirm.IsAtUrl(), "User can't get to SSO Account confirmation page");
        }
    
    }
}
