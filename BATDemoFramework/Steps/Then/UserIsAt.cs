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

        public void IsAtAdditionalDetailsPage()
        {
            Assert.IsTrue(NeyberPages.Pages.AdditionalDetails.WaitUntilUrlIsLoaded(), "Valid user is not on additional details page");
        }
    }
}
