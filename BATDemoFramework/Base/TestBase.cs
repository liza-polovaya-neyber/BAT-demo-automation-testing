using BATDemoFramework.Generators;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace BATDemoFramework
{
    [TestFixture]

    public class TestBase
    {
        //private string profile;
        //private string environment;

        //public TestBase(string profile, string environment)
        //{
        //    this.profile = profile;
        //    this.environment = environment;
        //}

        //Core test base class

        [OneTimeSetUp]
        public void SetUpBeforeTestClass()// This method fire at the start of the TestFixture
        {
            //Browser.Initialize();
            //UserGenerator.Initialize();
        }

        [SetUp]
        public void SetUpBeforeTestMethod()// This method fire at the start of EACH Test
        {
            Browser.Initialize();
            //UserGenerator.Initialize();
        }

        [TearDown]
        public void TearDownAfterTestMethod()// This method will fire at the end of EACH Test
        {
            Browser.DeleteCookies("NEYBER_authData_test1");
            //Browser.Close();
            //Browser.Quit();
        }

        [OneTimeTearDown]
        public void TearDownAfterTestClass()// This method will fire at the end of the TestFixture
        {
            Browser.Close();
            //Browser.Quit();
        }
    }


        //    [TearDown]
        //    public static void TearDown()
        //    {
        //        if(Pages.TopNavigation.IsLoggedIn())
        //            Pages.TopNavigation.LogOut();

        //        if(UserGenerator.LastGeneratedUser != null)
        //            Browser.Goto("Account/DeleteUsers.cshtml");
        //    }
        //}
    }
