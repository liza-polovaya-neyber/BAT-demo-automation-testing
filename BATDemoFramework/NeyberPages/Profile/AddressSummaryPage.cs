using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Pages.ProfilePages
{
    class AddressSummaryPage
    {
        [FindsBy(How = How.ClassName, Using = "logo")]
        private IWebElement logoNeyber;

        [FindsBy(How = How.ClassName, Using = "form-layout__back")]
        private IWebElement backLink;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        private IWebElement logoutLink;

        [FindsBy(How = How.Name, Using = "query")]
        private IWebElement addressField;

        [FindsBy(How = How.ClassName, Using = "np-m-glass search-box__button-icon")]
        private IWebElement searchButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='root']/div/div/div/div[2]/div/ul/li[2]/button")]
        private IWebElement firstAddress;

        [FindsBy(How = How.Name, Using = "month")]
        private IWebElement monthField;

        [FindsBy(How = How.Name, Using = "year")]
        private IWebElement yearField;

        [FindsBy(How = How.CssSelector, Using = "button.button.button-module__button___2VX0t > span")]
        private IWebElement confirmAndSelectBtn;

        [FindsBy(How = How.XPath, Using = "//*[@id='root']/div/div/div/div[2]/div/form/div[2]/button[2]")]
        private IWebElement cancelButton;

        [FindsBy(How = How.ClassName, Using = "address-box__edit")]
        private IWebElement editButton;

        public void GoTo()
        {
            Browser.GoTo("fmr/address-summary");
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public void ClickOnBackLink()
        {
            backLink.Click();
        }

        public void ClickOnAddressField()
        {
            addressField.Click();
        }

        public void EnterAddress(string address)
        {
            addressField.Click();
            addressField.SendKeys(address);
        }

        public void ClickOnSearchButton()
        {
            searchButton.Click();
        }

        public void ClickOnFirstAddress()
        {
            firstAddress.Click();
        }

        public void ClickOnEditButton()
        {
            editButton.Click();
        }

        public void ClickOnConfirmAndSelectBtn()
        {
            confirmAndSelectBtn.Click();
        }

        public void SetMonthAndYear(string month, string year)
        {
            var selectMonth = new SelectElement(monthField);
            selectMonth.SelectByText(month);
            var selectYear = new SelectElement(yearField);
            selectYear.SelectByText(year);
        }
    }
}
