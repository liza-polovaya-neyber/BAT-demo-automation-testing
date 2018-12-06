using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace BATDemoFramework
{
    public class EligibilityCriteriaPage
    {
        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-success.ng-isolate-scope")]
        private IWebElement FMRBtn;

        public void ClickToApplyNow()
        {
            FMRBtn = Browser.WaitUntilElementIsClickable(this.FMRBtn, 30);
            FMRBtn.Click();
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.EligibilityCriteriaPage);
        }
    }
}