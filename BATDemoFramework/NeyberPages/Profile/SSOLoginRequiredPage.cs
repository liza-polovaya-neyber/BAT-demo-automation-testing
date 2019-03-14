namespace BATDemoFramework.NeyberPages.Profile
{
    public class SSOLoginRequiredPage
    {
        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.SSOLoginRequired);
        }

        public bool WaitUntilUrlIsLoaded()
        {
            Browser.WaitUntilUrlIsLoaded(Urls.SSOLoginRequired, 10);
            return Pages.SSOLoginRequired.IsAtUrl();
        }
    }
}