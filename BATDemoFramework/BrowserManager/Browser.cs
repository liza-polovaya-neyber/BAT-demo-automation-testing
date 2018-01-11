using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BATDemoFramework
{
   public static class Browser
    {
        private static IWebDriver webDriver = new ChromeDriver();
        private static string baseUrl = "https://hellotest1.neyber.co.uk";


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