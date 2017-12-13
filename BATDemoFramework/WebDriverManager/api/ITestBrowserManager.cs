using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.WebDriverManager.api
{
    interface ITestBrowserManager
    {
        IWebDriver GetTestBrowser();

        void DestroyTestBrowser(IWebDriver browser);

    }
}
