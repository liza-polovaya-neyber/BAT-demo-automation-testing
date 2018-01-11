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
    [TestFixture]
    public class WebDriverTests
    {
        [Test]
        public void TestMethod1()
        {
            //Arrange
            using (var driver = GetChromeDriver())
            {
                //Act
                driver.Navigate().GoToUrl("https://www.google.com.ua/");

                //Assert
                Assert.AreEqual("Google", driver.Title);
            }
        }

        private IWebDriver GetChromeDriver()
        {
            var outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(outputDirectory);
        }
    }
}
