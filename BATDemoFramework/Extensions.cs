using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BATDemoFramework
{
    public static class Extensions
    {
        public static bool Exists(this IWebElement element)
        {
            try
            {
                var text = element.Text;
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }


        //method clears text field and then fills it in
        public static void EnterText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }


        //method replaces the method Displayed(), allows not to stop the programm running is case element is not found

        public static bool IsDisplayed(this IWebElement element)
        {
            bool result;
            try
            {
                result = element.Displayed;
            }
            catch (Exception)
            {
                result = false;
            }
            // Log the Action
            return result;
        }

        public static bool IsEnabled(this IWebElement element)
        {
            bool result;
            try
            {
                result = element.Enabled;
            }
            catch (Exception)
            {
                result = false;
            }
            // Log the Action
            return result;
        }

        public static bool IsNotEnabled(this IWebElement element)
        {
            bool result;
            try
            {
                result = !element.Enabled;
            }
            catch (Exception)
            {
                result = false;
            }
            // Log the Action
            return result;
        }

        //Click on the element
        public static void ClickOnIt(this IWebElement element, string elementName)
        {
            element.Click();
            Console.WriteLine("Clicked on " + elementName);
        }

        //select the option from dropdown
        public static void SelectDropDown(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByText(value);
        }

        //Get text from Textbox
        public static string GetText(IWebElement element)
        {
            return element.GetAttribute("value");
        }

        //Get text from  DropDownList box
        public static string GetTextFromDDL(IWebElement element)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;
        }


        //method which allows you to wait for an element in your code
        //public static IWebElement WaitUntilElementIsPresent(this IWebDriver driver, By by, int timeoutInSeconds)
        //{
        //    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        //    return wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("")));
        //}

    }
}

