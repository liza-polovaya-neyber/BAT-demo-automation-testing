using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework
{
    public class Urls
    {
        private static readonly string profileUrl = ConfigurationManager.AppSettings["ProfileUrl"];
        private static readonly string corporateUrl = ConfigurationManager.AppSettings["CorporateUrl"];
        public static readonly string OurTerms = corporateUrl + "/terms-conditions";
        public static readonly string PrivacyPolicy = corporateUrl + "/privacy-policy";
        public static readonly string CookiePolicy = corporateUrl + "/cookie-policy";
        public static readonly string ComplaintsPolicy = corporateUrl + "/complaints-policy";
        public static readonly string JoinPage = profileUrl + "/join";
        public static readonly string LoginPage = profileUrl +  "/login";
        public static readonly string HomePage = profileUrl +  "/home";
        public static readonly string ResetPassword = profileUrl + "/reset-password";
        public static readonly string AboutMePage = profileUrl + "/join/about-me";
        public static readonly string VerificationEmail = profileUrl + "/mail/sent";
        public static readonly string NotVerifiedEmail = profileUrl + "/mail/not-verified";
        public static readonly string ResendEmail = profileUrl + "/mail/resend";
        public static readonly string ExpiredLink = profileUrl + "/verification-expired";
        public static readonly string EmployerSearch = profileUrl + "/join/search";
        public static readonly string AlternativeEmail = profileUrl + "/join/alternative-email";
        public static readonly string WorkEmail = profileUrl + "/join/work-email";
        public static readonly string EmployerVerification = profileUrl + "/join/employer-verification";
        public static readonly string Marketing = profileUrl + "/join/marketing";

    }
}
