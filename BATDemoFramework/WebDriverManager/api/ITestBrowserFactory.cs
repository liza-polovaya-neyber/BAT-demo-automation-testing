using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.infrastructure.api
{
    interface ITestBrowserFactory
    {
        IWebDriver Create();
    }
}
