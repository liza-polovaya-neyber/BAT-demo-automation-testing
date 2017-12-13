using BATDemoFramework.infrastructure.api;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using BATDemoFramework.config;

namespace BATDemoFramework.infrastructure.factory
{
    public class CloudTestBrowser : ITestBrowserFactory
    {
        public IWebDriver Create()
        {
            string type = TestConfigurationManager.GetInstance().GetTestBrowser();
            if(type == "chrome")
            {
                return new ChromeDriver();
            }
            return null;
        }

            //switch (type)
            //{
            //    case CHROME:
            //        return new ChromeDriver();
            //    case FIREFOX:
            //        return new FirefoxDriver();
            //    case IEXPLORER:
            //        return new InternetExplorerDriver();
            //    case SAFARI:
            //        return new SafariDriver();
            //    default:
            //        return null;

    }
}

