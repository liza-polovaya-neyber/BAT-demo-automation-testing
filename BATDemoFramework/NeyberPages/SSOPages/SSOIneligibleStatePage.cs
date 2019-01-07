namespace BATDemoFramework.NeyberPages.SSOPages
{
    public class SSOIneligibleStatePage
    {
        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.SSOIneligibleState);
        }

        public bool WaitUntilUrlIsLoaded()
        {
            Browser.WaitUntilUrlIsLoaded(Urls.SSOIneligibleState, 60);
            return Pages.SSOIneligibleState.IsAtUrl();
        }
    }
}