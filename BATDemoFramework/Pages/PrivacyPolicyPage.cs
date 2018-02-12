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
                 Browser.SwitchTabs(1);
                 return Browser.Url.Contains(Urls.PrivacyPolicy);
            }


        }
}