using System;
using BATDemoFramework;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;


namespace BATDemoTests.TestCases
{
    class NewTabLinks : TestBase
    {
        static IWebDriver driver;

        [Test]
        public void OpenLinks()
        {
            //navigate to join page and store it as a parent window
            Pages.Join.GoTo();
            //Thread.Sleep(3000);
            //String parentWindowHandle = driver.CurrentWindowHandle;
            //Console.WriteLine("Parent window's handle -> " + parentWindowHandle);

            //Open all links in new tabs
            Pages.Join.GoToComplaintsPolicyPage();
            Pages.Join.GoToCookiePolicyPage();
            //Pages.Join.GoToPrivacyPolicyPage();
            //Pages.Join.GoToOurTermsPage();

            //store all opened new tabs in collection
            List<string> lstWindow = driver.WindowHandles.ToList();
            String lastWindowHandle = "";
            foreach (var handle in lstWindow)
            {
                //if (driver.Title.
               Assert.IsTrue(Pages.ComplaintsPolicy.IsAtUrl());
               Assert.IsTrue(Pages.CookiePolicy.IsAtUrl());
            }

            //Switch to the parent window
            //driver.SwitchTo().Window(parentWindowHandle);

            //close the parent window
            //driver.Close();






        }
    }
}
