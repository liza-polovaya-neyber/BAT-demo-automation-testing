using NUnit.Framework;

namespace BATDemoFramework.Steps.Then
{
    public class UserIsAtMarketingPreferencesPage
    {
        public static void IsAt()
        {
            Assert.IsTrue(NeyberPages.Pages.Marketing.WaitUntilMarketingUrlIsLoaded(), "Valid user is not on marketing page");
        }
    }
}
