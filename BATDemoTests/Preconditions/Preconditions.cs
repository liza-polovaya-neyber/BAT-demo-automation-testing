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
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
        }

        public static async Task HaveNewUserCreatedAndSelectedAnEmployer()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();
        }

        public static async Task HaveNewUserCreatedAndSkippedAlternativeEmail()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);

            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
        }

        public static async Task HaveNewUserPassedProfileJourney()
        {
            var user = new UserGenerator().GetNewUser();

            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();
            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.ChooseEmailOption();
            Pages.Marketing.ChooseSMSOption();
            Pages.Marketing.ChoosePostOption();
            Pages.Marketing.ChoosePhoneOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();
        }

        //new user creation => email verification. Method takes a user that already generated prior to method execution
        public static async Task NewUserCreatedAndVerifiedEmail(User user)
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            if (messages.Count == 0)
            {
                throw new Exception($"Can't get confirmation email: {user.EmailAddress}");
            }
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
        }

        public static async Task NewUserCreatedAndPassedProfileJourney(User user)
        {
            Pages.AboutMe.GoTo();
            Pages.AboutMe.RegisterNewUser(user);
            Pages.VerificationEmail.WaitUntilVerificationEmailPageTitleIsShown();

            var emailService = new EmailService();

            var messages = await emailService.GetMessagesBySubject(EmailTypes.ConfirmYourEmail, user.EmailAddress);
            if (messages.Count == 0)
            {
                throw new Exception($"Can't get confirmation email: {user.EmailAddress}");
            }
            var urlToken = emailService.GetUrlTokenFromMessage(messages[0]);

            Browser.GoToUrl(urlToken);
            Pages.EmployerSearch.WaitUntilUrlIsLoaded();
            Pages.EmployerSearch.SelectEnteredEmployer("Bupa");
            Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded();
            Pages.AlternativeEmail.ClickOnSkipLink();
            Pages.Marketing.WaitUntilMarketingUrlIsLoaded();
            Pages.Marketing.ChooseEmailOption();
            Pages.Marketing.ClickOnSubmitBtn();
            Pages.Home.WaitUntilHomeUrlIsLoaded();
        }

        public static void NewSSOUserCreated()
        {
            var user = UserGenerator.GetNewSSOUser();

            Pages.StubIDP.GoTo();
            Pages.StubIDP.EnterSSOUserDetailsAndSubmit(user);
            Pages.SSOAccountConfirm.WaitUntilUrlIsLoaded();
        }
    }
}
