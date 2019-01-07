using BATDemoFramework.NeyberPages.ProfilePages;
using BATDemoFramework.NeyberPages.SSOPages;
using BATDemoFramework.Pages.ApolloPages;
using BATDemoFramework.Pages.ProfilePages;
using OpenQA.Selenium.Support.PageObjects;

namespace BATDemoFramework.NeyberPages
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

        public static ChangePasswordPage ChangePassword
        {
            get { return GetPage<ChangePasswordPage>(); }
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

        public static ExpiredLinkPage ExpiredLink
        {
            get { return GetPage<ExpiredLinkPage>(); }
        }
        public static EmployerSearchPage EmployerSearch
        {
            get { return GetPage<EmployerSearchPage>(); }
        }

        public static AlternativeEmailPage AlternativeEmail
        {
            get { return GetPage<AlternativeEmailPage>(); }
        }

        public static WorkEmailPage WorkEmail
        {
            get { return GetPage<WorkEmailPage>(); }
        }

        public static EmployerVerificationPage EmployerVerification
        {
            get { return GetPage<EmployerVerificationPage>(); }
        }

        public static ConsentPage Consent
        {
            get { return GetPage<ConsentPage>(); }
        }

        public static MarketingPage Marketing
        {
            get { return GetPage<MarketingPage>(); }
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

        public static ApolloPMASPage ApolloPMAS
        {
            get { return GetPage<ApolloPMASPage>(); }
        }

        public static EligibilityCriteriaPage EligibilityCriteria
        {
            get { return GetPage<EligibilityCriteriaPage>(); }
        }

        public static GetInTouchPage GetInTouch
        {
            get { return GetPage<GetInTouchPage>(); }
        }

        public static FAQPage FAQ
        {
            get { return GetPage<FAQPage>(); }
        }

        public static SSOLoginRequiredPage SSOLoginRequired
        {
            get { return GetPage<SSOLoginRequiredPage>(); }
        }

        public static SSOAboutMePage SSOAboutMe
        {
            get { return GetPage<SSOAboutMePage>(); }
        }

        public static SSOAccountConfirmPage SSOAccountConfirm
        {
            get { return GetPage<SSOAccountConfirmPage>(); }
        }

        public static SSOIneligibleStatePage SSOIneligibleState
        {
            get { return GetPage<SSOIneligibleStatePage>(); }
        }

        public static StubIDPPage StubIDP
        {
            get { return GetPage<StubIDPPage>(); }
        }
    }
}
