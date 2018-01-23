﻿using OpenQA.Selenium;
using BATDemoFramework;

namespace BATDemoFramework
{
    public class OurTermsPage
    {
       

        public bool IsAt()
        {
            return Browser.Title.Contains("/terms-conditions");
        }

        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.OurTerms);
        }


    }
}