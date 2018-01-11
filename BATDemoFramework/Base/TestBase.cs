using BATDemoFramework.Generators;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace BATDemoFramework
{
    //[TestSetUp]

    public class TestBase
    {
        //[TestInitialize]

        public static void Initialize()
        {
            Browser.Initialize();
        }



        //[TestCleanup]

        public static void Cleanup()
        {
            Browser.Close();
            Browser.Quit();
        }



        //[TestFixture]
        //public class TestBase
        //{
        //    [SetUp]
        //    public static void Initialize()
        //    {
        //        Browser.Initialize();
        //        UserGenerator.Initialize();
        //    }        

        //    [TearDown]
        //    public static void TestFixtureTearDown()
        //    {
        //        Browser.Close();
        //    }

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
}
