namespace BATDemoFramework.Utils;

{
    public class ComplaintsPolicyPage
    {
        public bool IsAt()
        {
            return Browser.Title.Contains("/complaints");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.ComplaintsPolicy);
        }
    }
}