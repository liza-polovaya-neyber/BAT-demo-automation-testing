using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public class StubIDPPage
    {

        [FindsBy(How = How.Id, Using = "submit")]
        private IWebElement submitBtn;
       
        public void GoTo()
        {
            Browser.GoToUrl(Urls.StudIdpPage);
        }


        public string GetSSOUserData()
        {
            var user = UserGenerator.GetNewSSOUser();

            return @"(() => {
                $('#AssertionModel_AssertionConsumerServiceUrl').val('https://testenv0.neyber.co.uk/heracles/saml/login');
                $('#AssertionModel_Audience').val('https://testenv0.neyber.co.uk/heracles/identity/policemutual');" +
                $"const obj = {JsonConvert.SerializeObject(user)};" +
                @"const $addAttribute = $('#add-attribute');
                const $attributesPlaceholder = $('#attributes-placeholder');
                Object.keys(obj).forEach(function (key) {
                    $addAttribute.click();
                    const $controls = $attributesPlaceholder.find('.form-group:last .form-control');
                    const $name = $controls.eq(0);
                    const $value = $controls.eq(1);
                    $name.val(key);
                    $value.val(obj[key]);
                });
                })();";
        }

        public void EnterSSOUserDetailsAndSubmit()
        {
            Browser.ExecuteScript(GetSSOUserData());
            submitBtn.Click();
        }

    }
}