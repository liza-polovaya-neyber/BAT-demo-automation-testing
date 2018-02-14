using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class NotVerifiedEmailPage
    {
        [FindsBy(How = How.ClassName, Using = "button button_white mail-message-module__button___NhurA button-module__button___2VX0t")]
        private IWebElement continueBtn;

        [FindsBy(How = How.LinkText, Using = "start again")]
        private IWebElement startAgainLink;

        [FindsBy(How = How.LinkText, Using = "I haven’t received an email, please send another one.")]
        private IWebElement resendEmailLink;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.LinkText, Using = "Some legal bits we need to tell you")]
        private IWebElement someLegalBitsMenu;

        [FindsBy(How = How.XPath, Using = "//p[3]")]
        private IWebElement someLegalBitsMenuContent;


        public void GoTo()
        {
            Browser.GoTo("mail/not-verified");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.NotVerifiedEmail);
        }
    }
}