using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class ApolloPMASPage
    {
        [FindsBy(How = How.LinkText, Using = "Apply Now")]
        private IWebElement topApplyNowBtn;

        [FindsBy(How = How.XPath, Using = "//div[4]/button")]
        private IWebElement middleApplyNowBtn;

        [FindsBy(How = How.XPath, Using = "//div[8]/div/section/div/div[2]/button")]
        private IWebElement bottomApplyNowBtn;

        [FindsBy(How = How.PartialLinkText, Using = "Am I eligible to apply?")]
        private IWebElement eligibilityCriteriaBlock;

        [FindsBy(How = How.LinkText, Using = "Log in")]
        private IWebElement loginLink;

        [FindsBy(How = How.LinkText, Using = "Get in touch")]
        private IWebElement getInTouchLink;

        [FindsBy(How = How.LinkText, Using = "FAQ")]
        private IWebElement FAQLink;

        [FindsBy(How = How.LinkText, Using = "Personal loans")]
        private IWebElement personalLoansLink;

        [FindsBy(How = How.LinkText, Using = "I accept")]
        private IWebElement acceptPolicyBtn;

        private IWebDriver driver;

        public void ClickToApplyTopBtn()
        {
            topApplyNowBtn.Click();
        }

        public void ClickToApplyMiddleBtn()
        {
            middleApplyNowBtn.Click();
        }

        public void ClickToApplyBottomBtn()
        {
            bottomApplyNowBtn.Click();
        }

        public void ShowEligibilityCriteria()
        {
            eligibilityCriteriaBlock.Click();
        }

        public void ClickOnLogin()
        {
            loginLink.Click();
        }

        public void ClickOnGetInTouch()
        {
            getInTouchLink.Click();
        }

        public void ClickOnFAQ()
        {
            FAQLink.Click();
        }

        public void ClickOnPersonalLoans()
        {
            personalLoansLink.Click();
        }

        public void AcceptCookiesPolicy()
        {
            acceptPolicyBtn.Click();
        }

        public void GoToUrl()
        {
            Browser.GoToUrl(Urls.ApolloPMASPage);
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.ApolloPMASPage);
        }

        public bool WaitUntilBottomApplyNowBtnIsClickable(IWebDriver driver)
        {
            var applyNowBtn = Browser.WaitUntilElementIsClickable(driver, bottomApplyNowBtn, 10);
            return applyNowBtn.Displayed;

        }
    }
}