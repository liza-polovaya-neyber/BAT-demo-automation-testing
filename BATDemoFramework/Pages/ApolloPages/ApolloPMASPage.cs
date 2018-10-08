using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class ApolloPMASPage
    {
        [FindsBy(How = How.LinkText, Using = "Join now")]
        private IWebElement topJoinNowBtn;

        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-success.ng-isolate-scope")]
        private IWebElement middleJoinNowBtn;

        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-info.cta.ng-binding")]
        private IWebElement bottomJoinNowBtn;

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

        [FindsBy(How = How.Id, Using = "adroll_allow")]
        private IWebElement acceptCookiesBtn;

        private IWebDriver driver;

        public void ClickOnTopJoinNowBtn()
        {
            topJoinNowBtn.Click();
        }

        public void ClickToApplyMiddleBtn()
        {
            middleJoinNowBtn.Click();
        }

        public void ClickOnJoinNowBtn()
        {
            bottomJoinNowBtn.Click();
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

        public void AcceptCookiesOnBanner()
        {
            acceptCookiesBtn.Click();
        }

        public bool WaitUntilBottomApplyNowBtnIsClickable()
        {
            var applyNowBtn = Browser.WaitUntilElementIsClickable(bottomJoinNowBtn, 10);
            return applyNowBtn.Displayed;
        }

        public bool WaitUntilCookiesBannerIsVisible()
        {
            var cookiesBanner = Browser.WaitUntilElementIsClickable(acceptCookiesBtn, 10);
            return cookiesBanner.Displayed;
        }
    }
}