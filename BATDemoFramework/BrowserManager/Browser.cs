using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;

namespace BATDemoFramework
{
   public static class Browser
    {
        public static IWebDriver webDriver = new ChromeDriver();
        private static string baseUrl = "https://hellotest1.neyber.co.uk";
        private static IWebDriver driver;
        private static By locator;

        //private static IWebDriver webDriver;
        //private static string baseUrl = ConfigurationManager.AppSettings["url"];
        //private static string browser = ConfigurationManager.AppSettings["browser"];


        public static void Initialize()
        {
            //switch (browser)
            //{
            //    case "Chrome":
            //        webDriver = new ChromeDriver();
            //        break;
            //    case "IE":
            //        webDriver = new InternetExplorerDriver();
            //        break;
            //    case "Firefox":
            //        webDriver = new FirefoxDriver();
            //        break;
            //}
            //webDriver.Manage().Window.Maximize();
            GoTo(baseUrl);
        }

        public static void SwitchTabs(int tabIndex)
        {
            var windows = webDriver.WindowHandles;
            webDriver.SwitchTo().Window(windows[tabIndex]);
        }

        public static void SwitchBackToTab(int tabIndex)
        {
            webDriver.Close();
            var windows = webDriver.WindowHandles;
            webDriver.SwitchTo().Window(windows[tabIndex]);

        }
      
        public static IWebElement WaitUntilElementIsPresent(IWebDriver driver, By locator, int timeoutInSeconds)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(ExpectedConditions.ElementIsVisible((locator)));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static IWebElement WaitUntilElementIsClickable(IWebDriver driver, IWebElement element, int timeoutInSeconds)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static IWebElement WaitUntilElementIsVisible(IWebDriver driver, By locator, int timeoutInSeconds)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool WaitUntilPageTitleIsShown(IWebDriver driver, string title, int timeoutInSeconds)
        {
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(ExpectedConditions.TitleIs(title));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Close()
        {
        }

        public static void Quit()
        {
            webDriver.Quit();
        }

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static string Url
        {
            get
            {
                return webDriver.Url;
            }
        }

        public static ISearchContext Driver
        {
            get { return webDriver; }
        }

        public static void GoTo(string relativeUrl)
        {
            webDriver.Navigate().GoToUrl(string.Format("{0}/{1}", baseUrl, relativeUrl));
        }

    }
   
}