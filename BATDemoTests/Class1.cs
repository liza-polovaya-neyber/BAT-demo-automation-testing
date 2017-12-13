using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests
{
    class Class1
    {
        [TestFixture]
        public class UnitTest1
        {
            [Test]
            public void TestMethod1()
            {
                var driver = GetChromeDriver();
                driver.Navigate().GoToUrl("https://www.google.com.ua/");
                Assert.AreEqual("Google", driver.Title);
                driver.Close();
            }

            private IWebDriver GetChromeDriver()
            {
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return new ChromeDriver(outPutDirectory);
            }
        }
    }
}
