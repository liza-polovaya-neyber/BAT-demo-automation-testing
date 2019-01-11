using NUnit.Framework;

namespace BATDemoFramework.Steps.Then
{
    public class UserIsAtEmployerPage
    {
        public static void IsAt()
        {
            Assert.IsTrue(NeyberPages.Pages.EmployerSearch.WaitUntilUrlIsLoaded(), "Valid user is not on employer page");
        }
    }
}