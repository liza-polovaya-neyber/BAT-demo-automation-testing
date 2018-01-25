using BATDemoFramework.Generators;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace BATDemoFramework
{
    [TestFixture]

    public class TestBase
    {
        //Core test base class

        [SetUp]
        public void StartUpTest()// This method fire at the start of the Test
        {
            Browser.Initialize();
            //UserGenerator.Initialize();
        }

        //[TearDown]
        //public void EndTest()// This method will fire at the end of the Test
        //{
        //    Browser.Close();
        //    Browser.Quit();
        //}


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
