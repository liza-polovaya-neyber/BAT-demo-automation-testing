using BATDemoFramework;
using BATDemoFramework.EmailService;
using BATDemoFramework.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BATDemoTests
{
    static class Preconditions
    {
        public static async Task HaveNewUserCreated()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            Thread.Sleep(TimeSpan.FromSeconds(10));

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
        }
    }
}
