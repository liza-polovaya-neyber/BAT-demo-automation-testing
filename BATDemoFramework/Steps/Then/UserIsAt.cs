using NUnit.Framework;

namespace BATDemoFramework.Steps.Then
{
    public class UserIsAt
    {
        public void IsAtHomePage()
        {
            Assert.IsTrue(NeyberPages.Pages.Home.WaitUntilHomeUrlIsLoaded(), "Valid user is not on Hpage");
        }

        public void IsAtEmployerPage()
        {
            Assert.IsTrue(NeyberPages.Pages.EmployerSearch.WaitUntilUrlIsLoaded(), "Valid user is not on employer page");
        }

        public void IsAtAlternativeEmailPage()
        {
            Assert.IsTrue(NeyberPages.Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(), "Valid user is not on alternative email page");
        }

        public void IsAtMarketingPage()
        {
            Assert.IsTrue(NeyberPages.Pages.Marketing.WaitUntilMarketingUrlIsLoaded(), "Valid user is not on marketing page");
        }
    }
}
