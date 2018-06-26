using System;
using BATDemoFramework;
using NUnit.Framework;
namespace BATDemoTests

{
    [TestFixture]

    public class TestBaseForNewTabWindow
    {
        [OneTimeSetUp]
        public void StartUpAfterEachTest()// This method fire at the start of the Fixture
        {
            Browser.Initialize();
            //UserGenerator.Initialize();
        }

        [SetUp]
        public void StartUp()// This method fire at the start of the Test
        {
            //Browser.Initialize();
            //UserGenerator.Initialize();
        }


        [TearDown]
        public void EndTest()// This method will fire at the end of the Test
        {
           
            Browser.SwitchBackToTab(0);
            //Browser.Close();
        }

        [OneTimeTearDown]
        public void EndTestAfterEachTest()// This method will fire at the end of the Fixture
        {
            //Browser.Close();
            Browser.Quit();
        }

    }
}
