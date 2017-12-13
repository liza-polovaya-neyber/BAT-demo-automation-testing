using BATDemoFramework.WebDriverManager.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using BATDemoFramework.config;
using BATDemoFramework.WebDriverManager.enums;
using BATDemoFramework.infrastructure.api;
using BATDemoFramework.infrastructure.factory;

namespace BATDemoFramework.WebDriverManager
{
    class DefaultWebDriverManager : ITestBrowserManager
    {

        public IWebDriver GetTestBrowser()
        {
            RunEnv env = EnumHelper.GetRunEnv(TestConfigurationManager.GetInstance().GetRunOn());

            ITestBrowserFactory browserFactory;
            
            switch (env)
            {
                case RunEnv.LOCAL:
                    browserFactory = new LocalTestBrowser();
                    break;
                case RunEnv.BUILDSERVER:
                    browserFactory = new BuildServerTestBrowser();
                    break;
                case RunEnv.CLOUD:
                    browserFactory = new CloudTestBrowser();
                    break;
                default:
                    return null;
            }

            return browserFactory.Create();
        }



        public void DestroyTestBrowser(IWebDriver browser)
        {
            if (browser != null)
            {
                browser.Quit();
            }
        }

       
    }


}
