﻿using BATDemoFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    [TestFixture]

    class VeirifcationEmailTests : TestBase
    {
        [Test]
        public void GoesFromAboutMePageToVerificationEmailPage()
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser("GoesFromAboutMePageToVerificationEmailPage");

            Assert.IsTrue(Pages.VerificationEmail.IsAtUrl(), "User is not on Verification email page");
        }


    }
}
