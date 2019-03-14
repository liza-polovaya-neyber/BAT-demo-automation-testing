using System;
using BATDemoFramework.Generators;
using BATDemoFramework.NeyberPages.Profile;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using static BATDemoFramework.NeyberPages.Profile.AboutMePage;

namespace BATDemoFramework.NeyberPages.SSOPages
{
    public class SSOAboutMePage
    {
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement titleDD;

        [FindsBy(How = How.Name, Using = "mobile_number")]
        private IWebElement mobileNumberField;

        [FindsBy(How = How.Name, Using = "mobile_number")]
        private IWebElement referralCodeField;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement passwordField;

        [FindsBy(How = How.Id, Using = "terms_accepted")]
        private IWebElement policyTickbox;

        [FindsBy(How = How.XPath, Using = "//div[6]/button/span")]
        private IWebElement submitBtn;

        [FindsBy(How = How.CssSelector, Using = "p.control__error")]
        private IWebElement errorMessage;


        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.SSOAboutMePage);
        }

        public bool WaitUntilUrlIsLoaded()
        {
            Browser.WaitUntilUrlIsLoaded(Urls.SSOAboutMePage, 60);
            return Pages.SSOAboutMe.IsAtUrl();
        }

        public void PressSubmitButton()
        {
            submitBtn.Click();
        }

        public string GetErrorPasswordText()
        {
            return errorMessage.Text;
        }

        public void RegisterUserButDontSelectTitle(User newUser)
        {
            RegisterBase(newUser, shouldSelectTitle: false);
        }

        public void RegisterUserButDontEnterMobileNumber(User newUser)
        {
            RegisterBase(newUser, shouldEnterMobileNumber: false);
        }

        public void RegisterUserWithEnteredReferralCode(User newUser)
        {
            RegisterBase(newUser, shouldEnterReferralCode: true);
        }

        public void RegisterUserWithAllFieldsFilledIn(User newUser)
        {
            RegisterBase(newUser);
        }

        private void RegisterBase(User newUser,
            bool shouldSelectTitle = true,
            bool shouldEnterReferralCode = false,
            bool shouldEnterMobileNumber = true)
        {

            if (shouldSelectTitle)
            {
                SelectTitle(TitleType.Mr);
            }

            mobileNumberField.EnterText("07523698547");

            if (!shouldEnterMobileNumber)
            {
                mobileNumberField.Clear();
            }

            if (shouldEnterReferralCode)
            {
                referralCodeField.SendKeys(ReferralCodeGenerator.GenerateReferralCode(15));
            }

            passwordField.SendKeys(newUser.Password);

            policyTickbox.Click();
        }

        public void SelectTitle(TitleType type)
        {
            var selectElement = new SelectElement(titleDD);
            try
            {
                selectElement.SelectByValue(type.ToString());
            }
            catch (Exception ex)
            {
                var ee = 55;
            }
        }

        public void EnterMobileNumber(string number)
        {
            mobileNumberField.Click();
            mobileNumberField.SendKeys(number);
        }

        public void EnterPassword(string password)
        {
            passwordField.SendKeys(password);
        }

        public void EnterReferralCode(string referralCode)
        {

        }

        public void PutCursorOnReferralCodeField()
        {
            referralCodeField.Click();
        }
    }
}