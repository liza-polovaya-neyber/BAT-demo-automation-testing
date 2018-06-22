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
        private static string profileUrl = ConfigurationManager.AppSettings["ProfileUrl"];
        private static string hostUrlUrl = ConfigurationManager.AppSettings["HostUrl"];
        public const string OurTerms = "https://neyberweb-testenv1.neyber.co.uk/terms-conditions";
        public const string PrivacyPolicy = "https://neyberweb-testenv1.neyber.co.uk/privacy-policy";
        public static string CookiePolicy = hostUrlUrl + "/app/policies#cookie";
        public static string ComplaintsPolicy = hostUrlUrl + "/app/complaints";
        public static string JoinPage = profileUrl + "/join";
        public static string LoginPage = profileUrl +  "/login";
        public static string HomePage = profileUrl +  "/home";
        public static string ResetPassword = profileUrl + "/reset-password";
        public static string AboutMePage = profileUrl + "/join/about-me";
        public static string VerificationEmail = profileUrl + "/mail/sent";
        public static string NotVerifiedEmail = profileUrl + "/mail/not-verified";
        public static string ResendEmail = profileUrl + "/mail/resend";
        public static string ExpiredLink = profileUrl + "/verification-expired";
        public static string EmployerSearch = profileUrl + "/join/search";
        public static string AlternativeEmail = profileUrl + "/join/alternative-email";
        public static string WorkEmail = profileUrl + "/join/work-email";
        public static string EmployerVerification = profileUrl + "/join/employer-verification";
        public static string Marketing = profileUrl + "/join/marketing";

    }
}
