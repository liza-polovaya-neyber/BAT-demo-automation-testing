using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace BATDemoFramework
{
    public class EligibilityCriteriaPage
    {
        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-success")]
        private IWebElement applyNowBtn;

        public void ClickToApplyNow()
        {
            applyNowBtn.Click();
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.EligibilityCriteriaPage);
        }
    }
}