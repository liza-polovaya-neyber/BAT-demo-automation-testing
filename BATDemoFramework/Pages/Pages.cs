using BATDemoFramework;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework
{
    public static class Pages
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(Browser.Driver, page);
            return page;
        }

        public static JoinPage Join
        {
            get { return GetPage<JoinPage>(); }
        }

        public static LoginPage Login
        {
            get { return GetPage<LoginPage>(); }
        }

        public static AboutMePage AboutMe
        {
            get { return GetPage<AboutMePage>(); }
        }

        public static ResetPasswordPage Contact
        {
            get { return GetPage<ResetPasswordPage>(); }
        }

        public static OurTermsPage OurTerms
        {
            get { return GetPage<OurTermsPage>(); }
        }


    }
}
