
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

        public static ResetPasswordPage ResetPassword
        {
            get { return GetPage<ResetPasswordPage>(); }
        }

        public static VerificationEmailPage VerificationEmail
        {
            get { return GetPage<VerificationEmailPage>(); }
        }

        public static NotVerifiedEmailPage NotVerifiedEmail
        {
            get { return GetPage<NotVerifiedEmailPage>(); }
        }

        public static ResendEmailPage ResendEmail
        {
            get { return GetPage<ResendEmailPage>(); }
        }

        public static EmployerSearchPage EmployerSearch
        {
            get { return GetPage<EmployerSearchPage>(); }
        }

        public static HomePage Home
        {
            get { return GetPage<HomePage>(); }
        }

        public static PrivacyPolicyPage PrivacyPolicy
        {
            get { return GetPage<PrivacyPolicyPage>(); }
        }

        public static CookiePolicyPage CookiePolicy
        {
            get { return GetPage<CookiePolicyPage>(); }
        }

        public static ComplaintsPolicyPage ComplaintsPolicy
        {
            get { return GetPage<ComplaintsPolicyPage>(); }
        }

        public static OurTermsPage OurTerms
        {
            get { return GetPage<OurTermsPage>(); }
        }
    }
}
