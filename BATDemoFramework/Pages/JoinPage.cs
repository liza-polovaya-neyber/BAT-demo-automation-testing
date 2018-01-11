using BATDemoFramework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class JoinPage
    {

        [FindsBy(How = How.ClassName, Using = "button button button_white welcome-page-module__button___1xdDp button-module__button___p4iTs")]
        private IWebElement registerLink;

        [FindsBy(How = How.ClassName, Using = "already-customer-login-module__login-link___2DwCr")]
        private IWebElement loginLink;

        [FindsBy(How = How.LinkText, Using = Urls.TermsAndConditions)]
        private IWebElement ourTermsLink;

        [FindsBy(How = How.LinkText, Using = "https://app.neyber.co.uk/privacy-policy")]
        private IWebElement privacyPolicyLink;

        [FindsBy(How = How.LinkText, Using = "https://testenv1.neyber.co.uk/app/policies#cookie")]
        private IWebElement cookiePolicyLink;

        [FindsBy(How = How.LinkText, Using = "https://testenv1.neyber.co.uk/app/complaints")]
        private IWebElement complaintsPolicyLink;

        [FindsBy(How = How.ClassName, Using = "np-chevron-down bottom-menu-module__bits-link___2YBLe")]
        private IWebElement someLegalBitsMenu;

        //[FindsBy(How = How.ClassName, Using = "bottom-menu-module__bits___1kG15")]
        //private IWebElement someLegalBitsMenuContent;


        public void GotoJoinPage()
        {
            registerLink.Click();
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

        public void someLegalBitsMenuOpen()
        {
            someLegalBitsMenu.Click();
        }

        public bool IsAt()
        {
            return ChromeBrowser.Title.Contains("/join");
        }
    }
}

