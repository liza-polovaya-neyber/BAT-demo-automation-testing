using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BATDemoFramework
{
   public static class Browser
    {
        private static string baseUrl = "https://hellotest1.neyber.co.uk/";
        private static IWebDriver webDriver;


        public static void Initialize()
        {
            webDriver = new ChromeDriver();
        }

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static ISearchContext Driver
        {
            get { return webDriver; }
        }

        public static void Goto(string url)
        {
            webDriver.Navigate().GoToUrl(baseUrl + url);
        }

        public static void Close()
        {
            webDriver.Close();
        }
    }
   
}