﻿using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BATDemoTests.TestCases
{
    class ProfileRegistration : TestBase
    {
        public async Task CanVerifyPrimaryEmail()
        {
            var user = new UserGenerator().GetNewUser();
            var emailService = new EmailService();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);
            Thread.Sleep(TimeSpan.FromSeconds(5));



            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            //var url = await emailService.GetLastConfirmationUrlFromMessage(messages);


        }
    
    }
}
