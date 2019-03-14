﻿using System;
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
        private static readonly string apolloUrl = ConfigurationManager.AppSettings["ApolloUrl"];
        public static readonly string OurTerms = corporateUrl + "/terms-conditions";
        public static readonly string PrivacyPolicy = corporateUrl + "/privacy-policy";
        public static readonly string CookiePolicy = corporateUrl + "/cookie-policy";
        public static readonly string ComplaintsPolicy = corporateUrl + "/complaints-policy";
        public static readonly string JoinPage = profileUrl + "/join";
        public static readonly string LoginPage = profileUrl + "/login";
        public static readonly string HomePage = profileUrl + "/home";
        public static readonly string ConsentPage = profileUrl + "/fmr/consent";
        public static readonly string ResetPassword = profileUrl + "/reset-password";
        public static readonly string AboutMePage = profileUrl + "/join/about-me";
        public static readonly string VerificationEmail = profileUrl + "/mail/sent";
        public static readonly string NotVerifiedEmail = profileUrl + "/mail/not-verified";
        public static readonly string ResendEmail = profileUrl + "/mail/resend";
        public static readonly string ExpiredLink = profileUrl + "/verification-expired";
        public static readonly string EmployerSearch = profileUrl + "/join/search";
        public static readonly string AlternativeEmail = profileUrl + "/join/alternative-email";
        public static readonly string AdditionalDetails = profileUrl + "/join/additional-details";
        public static readonly string WorkEmail = profileUrl + "/join/work-email";
        public static readonly string EmployerVerification = profileUrl + "/join/employer-verification";
        public static readonly string Marketing = profileUrl + "/join/marketing";
        public static readonly string ApolloPMASPage = apolloUrl + "/app/policemutualloans";
        public static readonly string AboutMePMASPage = profileUrl + "/join/about-me?tenantName=Police%20Mutual";
        public static readonly string EligibilityCriteriaPage = apolloUrl + "/app/eligibility-criteria";
        public static readonly string GetInTouchPage = apolloUrl + "/app/contact-us";
        public static readonly string FAQPage = apolloUrl + "/app/faq";
        public static readonly string SSOLoginRequired = profileUrl + "/join/sso-login-required";
        public static readonly string SSOAboutMePage = profileUrl + "/join/sso-employee";
        public static readonly string SSOAccountConfirm = profileUrl + "/join/account-confirm";
        public static readonly string SSOIneligibleState = profileUrl + "/validation-issues?issues";

        public static readonly string StudIdpPage = "https://stubidp.sustainsys.com/";    

    }
}
