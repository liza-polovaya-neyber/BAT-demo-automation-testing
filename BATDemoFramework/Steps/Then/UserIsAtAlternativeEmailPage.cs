using NUnit.Framework;

namespace BATDemoFramework.Steps.Then
{
    public class UserIsAtAlternativeEmailPage
    {
        public static void IsAt()
        {
            Assert.IsTrue(NeyberPages.Pages.AlternativeEmail.WaitUntilAlternativeUrlIsLoaded(), "Valid user is not on alternative email page");
        }
    }
}
