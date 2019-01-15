using BATDemoFramework.Generators;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace BATDemoFramework
{
    [TestFixture]

    public class TestBase
    {
        [OneTimeSetUp]
        public void SetUpBeforeTestClass() // This method fire at the start of the TestFixture
        {
            Browser.Initialize();
        }

        [SetUp]
        public void SetUpBeforeTestMethod() // This method fire at the start of EACH Test
        {
            Browser.Initialize();
        }

        [TearDown]
        public void TearDownAfterTestMethod() // This method will fire at the end of EACH Test
        {
            Browser.DeleteCookies("NEYBER_authData_test1");
            Browser.Quit();
        }

        [OneTimeTearDown]
        public void TearDownAfterTestClass() // This method will fire at the end of the TestFixture
        {
            Browser.Quit();
        }
    }
}
