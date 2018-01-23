using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Configuration;

namespace BATDemoFramework
{
   public static class Browser
    {
        //private static IWebDriver webDriver = new ChromeDriver();
        //private static string baseUrl = "https://hellotest1.neyber.co.uk";

        private static IWebDriver webDriver;
        private static string baseUrl = ConfigurationManager.AppSettings["url"];
        private static string browser = ConfigurationManager.AppSettings["browser"];
        public static void Init()
        {
            switch (browser)
            {
                case "Chrome":
                    webDriver = new ChromeDriver();
                    break;
                case "IE":
                    webDriver = new InternetExplorerDriver();
                    break;
                case "Firefox":
                    webDriver = new FirefoxDriver();
                    break;
            }
            webDriver.Manage().Window.Maximize();
            GoTo(baseUrl);
        }


        public static void Initialize()
        {
            GoTo("");
        }

        public static void Close()
        {
            webDriver.Close();
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