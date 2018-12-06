using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using BATDemoFramework.EmailService;
using BATDemoFramework.Utils;
using System;

namespace BATDemoFramework
{
    public class VerificationEmailPage
    {
        //private EmailService service;

        [FindsBy(How = How.CssSelector, Using = "button.button.button_white.mail-message-module__button___NhurA.button-module__button___2VX0t")]
        private IWebElement continueBtn;

        [FindsBy(How = How.CssSelector, Using = "button.mail-message-module__note___1wK3m")]
        private IWebElement resendEmailLink;

        [FindsBy(How = How.XPath, Using = "//button")]
        private IWebElement logoutLink;

        [FindsBy(How = How.LinkText, Using = "Some legal bits we need to tell you")]
        private IWebElement someLegalBitsMenu;

        [FindsBy(How = How.XPath, Using = "//p[3]")]
        private IWebElement someLegalBitsMenuContent;

        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.XPath, Using = "//button")]
        private IWebElement closeBtn;

        [FindsBy(How = How.XPath, Using = "//section/div")]
        private IWebElement greenVerificationBanner;

        public void GoTo()
        {
            Browser.GoTo("mail/sent");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.VerificationEmail);
        }

        public bool WaitTillContinueBtnIsVisible()
        {
           Browser.WaitUntilElementIsVisible(By.ClassName("button button_white mail-message-module__button___NhurA button-module__button___2VX0t"), 25);
            return continueBtn.Displayed;
        }

        public bool WaitUntilVerificationEmailPageTitleIsShown()
        {
            Browser.WaitUntilPageTitleIsShown(PageTitles.VerificationEmailSent, 120);
            return Pages.VerificationEmail.IsAtUrl();
        }

        public bool WaitUntilGreenBannerIsShown()
        {
           Browser.WaitUntilElementIsClickable(greenVerificationBanner, 10);
            return greenVerificationBanner.Displayed;
        }

        public void CloseGreenBanner()
        {
            closeBtn.Click();
        }

        public void ClickOnContinueBtn()
        {
            continueBtn.Click();
        }

        public void ClickOnResendEmailLink()
        {
            resendEmailLink.Click();
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public bool GreenBannerIsShown()
        {
            return greenVerificationBanner.Exists();
        }
    }
}