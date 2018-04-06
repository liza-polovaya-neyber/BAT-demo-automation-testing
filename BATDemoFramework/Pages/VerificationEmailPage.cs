using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using BATDemoFramework.EmailService;

namespace BATDemoFramework
{
    public class VerificationEmailPage
    {
        //private EmailService service;

        [FindsBy(How = How.CssSelector, Using = "button.button.button_white.mail-message-module__button___NhurA.button-module__button___2VX0t")]
        private IWebElement continueBtn;

        [FindsBy(How = How.LinkText, Using = "I haven’t received an email, please send another one.")]
        private IWebElement resendEmailLink;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.LinkText, Using = "Some legal bits we need to tell you")]
        private IWebElement someLegalBitsMenu;

        [FindsBy(How = How.XPath, Using = "//p[3]")]
        private IWebElement someLegalBitsMenuContent;

        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        public void GoTo()
        {
            Browser.GoTo("mail/sent");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.VerificationEmail);
        }

        public bool WaitTillContinueBtnIsVisible(IWebDriver driver)
        {
            var continueButton = Browser.WaitUntilElementIsVisible(driver, By.ClassName("button button_white mail-message-module__button___NhurA button-module__button___2VX0t"), 25);
            return continueButton.Displayed;
        }

        public void ClickOnContinueBtn()
        {
            continueBtn.Click();
        }

        public void ClickOnResendEmailLink()
        {
            resendEmailLink.Click();
        }

        public void ClickOnLogoutLink()
        {
            logoutLink.Click();
        }

        //public bool Check_VerificationEmail_IsSent()
        //{
        //    bool result;
        //    var messages = service.GetMessagesByQuery(EmailTypes.ConfirmYourEmail);
        //    var subject = messages[0].Payload.Headers.FirstOrDefault(x => x.Name == "Subject").Value;
        //    int [] numberOfEmails = 
            
        //    int sizeOfArray = numberOfEmails.Length;

        //    if (sizeOfArray = 1)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        //public bool Check_VerificationEmails_AreSent()
        //{
        //    bool result;
        //    var messages = service.GetMessagesByQuery(EmailTypes.ConfirmYourEmail);
        //    var subject = messages[0].Payload.Headers.FirstOrDefault(x => x.Name == "Subject").Value;
        //    int [] numberOfEmails =

        //    int sizeOfArray = numberOfEmails.Length;

        //    if (sizeOfArray = 2)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }

        //    return result;
        //}
    }
}