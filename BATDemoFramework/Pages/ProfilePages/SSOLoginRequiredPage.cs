﻿namespace BATDemoFramework
{
    public class SSOLoginRequiredPage
    {
        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.SSOLoginRequired);
        }

        public bool WaitUntilUrlIsLoaded()
        {
            var ssoLoginRequiredPage = Browser.WaitUntilUrlIsLoaded(Urls.SSOLoginRequired, 10);
            return Pages.SSOLoginRequired.IsAtUrl();
        }
    }
}