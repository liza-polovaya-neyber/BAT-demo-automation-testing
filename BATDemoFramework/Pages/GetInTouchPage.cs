namespace BATDemoFramework
{
    public class GetInTouchPage
    {

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.GetInTouchPage);
        }
    }
}