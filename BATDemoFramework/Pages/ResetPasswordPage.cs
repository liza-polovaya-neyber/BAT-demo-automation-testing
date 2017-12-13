using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class ResetPasswordPage
    {

        [FindsBy(How = How.TagName, Using = "email")]
        private IWebElement emailTextField;

        [FindsBy(How = How.ClassName, Using = "button button auth__button password-reset-form-module__button___Q5_jB button-module__button___p4iTs")]
        private IWebElement sendMyResetLinkButton;

        [FindsBy(How = How.ClassName, Using = "auth__reg-link")]
        private IWebElement registerLink;

        [FindsBy(How = How.LinkText, Using = "Return to login")]
        private IWebElement returnToLoginLink;


        public void GotoJoinPage()
        {
            registerLink.Click();
        }

        public void GotoLoginPage()
        {
            returnToLoginLink.Click();
        }

        public bool IsAt()
        {
            return Browser.Title.Contains("/reset-password");
        }
    }
}