using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework

{
    public class ComplaintsPolicyPage
    {
        [FindsBy(How = How.CssSelector, Using = "h1")]
        private IWebElement ourComplaintsPolicyHeader;

        public bool ComplaintsPolicyHeaderDisplayed()
        {
            bool header = ourComplaintsPolicyHeader.Displayed;
            return header;
        }

        //public bool IsAt()
        //{
        //    Browser.SwitchTabs(1);
        //    return Pages.ComplaintsPolicy.ComplaintsPolicyHeaderDisplayed();
        //}

        public bool IsAtUrl()
        {
            Browser.SwitchTabs(1);
            return Browser.Url.Contains(Urls.ComplaintsPolicy);
            
            //Browser.SwitchBackToTab(0);
        }
    }
}