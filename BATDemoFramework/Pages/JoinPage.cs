using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class JoinPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.ClassName, Using = "button button button_white welcome-page-module__button___1xdDp button-module__button___p4iTs")]
        private IWebElement letsRegisterLink;

        [FindsBy(How = How.ClassName, Using = "already-customer-login-module__login-link___2DwCr")]
        private IWebElement loginLink;

        [FindsBy(How = How.PartialLinkText, Using = "Terms")]
        private IWebElement ourTermsLink;

        [FindsBy(How = How.PartialLinkText, Using = "Privacy")]
        private IWebElement privacyPolicyLink;

        [FindsBy(How = How.PartialLinkText, Using = "Cookie")]
        private IWebElement cookiePolicyLink;

        [FindsBy(How = How.PartialLinkText, Using = "Complaints")]
        private IWebElement complaintsPolicyLink;

        [FindsBy(How = How.LinkText, Using = "Some legal bits we need to tell you")]
        private IWebElement someLegalBitsMenu;

        [FindsBy(How = How.XPath, Using = "//p[5]")]
        private IWebElement someLegalBitsMenuContent;
        

        public void GoTo()
        {
            Browser.GoTo("join");
        }

        public void GotoAboutMePage()
        {
            letsRegisterLink.Click();
        }
          
         public void GotoLoginPage()
         {
               loginLink.Click();
         }
        
        public void GoToOurTermsPage()
        {
            ourTermsLink.Click();
        }

        public void GoToPrivacyPolicyPage()
        {
            privacyPolicyLink.Click();
        }

        public void GoToCookiePolicyPage()
        {
            cookiePolicyLink.Click();
        }

        public void GoToComplaintsPolicyPage()
        {
            complaintsPolicyLink.Click();
        }

        public void OpenSomeLegalBitsMenu()
        {
            someLegalBitsMenu.Click();
        }
        public bool SomeLegalBitsMenuIsDisplayed()
        {
            //bool result;
            //result = someLegalBitsMenuContent.IsDisplayed();
            //return result;
           return someLegalBitsMenu.IsDisplayed();
        }

        public bool IsAt()
        {
            return Browser.Title.Contains("/join");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.JoinPage);
        }

    }
}

