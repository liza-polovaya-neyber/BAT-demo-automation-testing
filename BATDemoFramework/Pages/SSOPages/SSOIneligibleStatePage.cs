namespace BATDemoFramework
{
    public class SSOIneligibleStatePage
    {
        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.SSOIneligible);
        }

        public bool WaitUntilUrlIsLoaded()
        {
            Browser.WaitUntilUrlIsLoaded(Urls.SSOIneligible, 60);
            return Pages.SSOIneligibleState.IsAtUrl();
        }
    }
}