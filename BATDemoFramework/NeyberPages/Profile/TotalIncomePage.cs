using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Pages.ProfilePages
{
    class TotalIncomePage
    {
        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.ClassName, Using = "form-layout__back")]
        private IWebElement backLink;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.Name, Using = "fmrTotalGrossIncome")]
        private IWebElement incomeField;

        [FindsBy(How = How.CssSelector, Using = "button.button.button-module__button___2VX0t > span")]
        private IWebElement continueBtn;

        [FindsBy(How = How.ClassName, Using = "control__error")]
        private IWebElement errorMessage;

        public void GoTo()
        {
            Browser.GoTo("fmr/total-income");
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public void ClickOnBackLink()
        {
            backLink.Click();
        }

        public void ClickOnIncomeField()
        {
            incomeField.Click();
        }

        public void EnterIncome(string income)
        {
            incomeField.Click();
            incomeField.SendKeys(income);
        }

        public void ClickOnContinueBtn()
        {
            continueBtn.Click();
        }

        public string ShowErrorMessage(string income)
        {
            incomeField.Click();
            incomeField.SendKeys(income);
            continueBtn.Click();
            return errorMessage.Text;   //Can be both "Please tell us your total income" and "Must be more than £1000"
        }
    }
}
