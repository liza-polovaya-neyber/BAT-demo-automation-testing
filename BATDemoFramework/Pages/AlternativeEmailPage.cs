using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class AlternativeEmailPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement alternativeEmailField;

        [FindsBy(How = How.LinkText, Using = "Skip this step")]
        private IWebElement skipEmailLink;

        [FindsBy(How = How.ClassName, Using = "button button email-form-module__button___3vdUD button-module__button___2VX0t")]
        private IWebElement submitBtn;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorMessage;
        
        public void GoTo()
        {
            Browser.GoTo("join/alternative");
        }

        public void IsAtUrl()
        {
            Browser.Url.Contains(Urls.AlternativeEmail);
        }
    }
}