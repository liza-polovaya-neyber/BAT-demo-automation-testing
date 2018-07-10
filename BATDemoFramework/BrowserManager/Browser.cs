using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Threading;
using System.Web.UI.WebControls;
using NUnit.Framework;

namespace BATDemoFramework
{
   public static class Browser
   {
        public static IWebDriver webDriver;
        private static readonly string baseUrl = ConfigurationManager.AppSettings["ProfileUrl"];
        private static readonly double implicitWaitTimeOut = Double.Parse(ConfigurationManager.AppSettings["ImplicitWaitTimeOut"]);
        private static readonly double pageLoadTimeOut = Double.Parse(ConfigurationManager.AppSettings["PageLoadTimeOut"]);

        //private static IWebDriver webDriver;
        //private static string baseUrl = ConfigurationManager.AppSettings["url"];
        //private static string browser = ConfigurationManager.AppSettings["browser"];

        private static TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, TimeSpan timeout)
        {
            // workaround on Retry attribute.
            TResult result = default(TResult);
            Assert.That(() => result =
                new WebDriverWait(webDriver, timeout).Until(condition), Throws.Nothing);
            return result;
        }

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
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTimeOut);
            //webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeOut);
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
      
        public static IWebElement WaitUntilElementIsPresent(By locator, int timeoutInSeconds)
        {
            return WaitUntil(ExpectedConditions.ElementIsVisible(locator), TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public static IWebElement WaitUntilElementIsClickable(IWebElement element, int timeoutInSeconds)
        {
            return WaitUntil(ExpectedConditions.ElementToBeClickable(element), TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public static IWebElement WaitUntilElementIsVisible(By locator, int timeoutInSeconds)
        {
            return WaitUntil(ExpectedConditions.ElementIsVisible(locator), TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public static bool WaitUntilPageTitleIsShown(string title, int timeoutInSeconds)
        {
            return WaitUntil(ExpectedConditions.TitleIs(title), TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public static bool WaitUntilUrlIsLoaded(string url, int timeoutInSeconds)
        {
            return WaitUntil(ExpectedConditions.UrlMatches(url), TimeSpan.FromSeconds(timeoutInSeconds));
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

        public static object WebDriver { get; private set; }

        public static void GoTo(string relativeUrl)
        {
            webDriver.Navigate().GoToUrl(string.Format("{0}/{1}", baseUrl, relativeUrl));
        }

        public static void GoToUrl(string url)
        {
            webDriver.Navigate().GoToUrl(url);
        }

        public static void RefreshThePage()
        {
            webDriver.Navigate().Refresh();
        }

        public static void DeleteCookies(string cookieName)
        {
            webDriver.Manage().Cookies.DeleteCookieNamed(cookieName);

        }

        public static void DragAndDrop(IWebElement source, IWebElement destination)
        {
            (new Actions(webDriver)).DragAndDrop(source, destination).Build().Perform();
        }

        public static void NavigateBack()
        {
            webDriver.Navigate().Back();
        }

        public static void ClickTheElement(IWebDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform(); // move to the button
            element.Click();
        }

        public static void AlertAccept()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Alert().Accept();
            webDriver.SwitchTo().DefaultContent();
        }

        public static object ExecuteScript(string script)
        {
            return ((IJavaScriptExecutor)webDriver).ExecuteScript(script);
        }

    }
   
}