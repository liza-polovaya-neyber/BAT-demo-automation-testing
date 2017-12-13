using BATDemoFramework.infrastructure.api;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using BATDemoFramework.config;
using BATDemoFramework.WebDriverManager.enums;

namespace BATDemoFramework.infrastructure.factory
{
    class BuildServerTestBrowser : ITestBrowserFactory
    { 
        public IWebDriver Create()
        {
            //BrowserType type = BrowserType.GetBrowserType(TestConfigurationManager.GetInstance().GetTestBrowser());
            string type = TestConfigurationManager.GetInstance().GetTestBrowser();


            if (type == "chrome")
            {
                return new ChromeDriver();
            }
            return null;

            //switch (type)
            //{
            //    case CHROME:
            //         return new ChromeDriver();
            //    case FIREFOX:
            //        return new FirefoxDriver();
            //    case IEXPLORER:
            //        return new InternetExplorerDriver();
            //    case SAFARI:
            //        return new SafariDriver();
            //    default:
            //        return null;

            //}
        }
    }
}
