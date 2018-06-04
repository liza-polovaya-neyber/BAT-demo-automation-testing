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
        public static async Task HaveNewUserCreatedAndVerifiedEmail()
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

        public static async Task HaveNewUserCreatedAndSelectedAnEmployer()
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

            Pages.EmployerSearch.WaitUntilUrlIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
        }

        public static async Task HaveNewUserPassedProfileJourney()
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
            Pages.EmployerSearch.WaitUntilSecurityBlockIsLoaded(Browser.webDriver);
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(Browser.webDriver);
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded(Browser.webDriver);
            Pages.Marketing.ChooseEmailOption();
            Pages.Marketing.ChooseSMSOption();
            Pages.Marketing.ChoosePostOption();
            Pages.Marketing.ChoosePhoneOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded(Browser.webDriver);
        }

        //new user creation => email verification. Method takes a user that already generated prior to method execution
        public static async Task NewUserCreated(User user)
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown(Browser.webDriver);

            Thread.Sleep(TimeSpan.FromSeconds(20));

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesByQuery(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
        }
    }
}
