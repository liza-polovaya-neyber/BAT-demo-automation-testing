namespace BATDemoFramework

{
    public class PrivacyPolicyPage
    {


            public bool IsAt()
            {
                return Browser.Title.Contains("/privacy-policy");
            }

            public bool IsAtUrl()
            {
                return Browser.Url.Contains(Urls.PrivacyPolicy);
            }


        }
}