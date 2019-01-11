﻿using NUnit.Framework;

namespace BATDemoFramework.Steps.Then
{
    public class UserIsAtHomePage
    {
        public static void IsAt()
        {
            Assert.IsTrue(NeyberPages.Pages.Home.WaitUntilHomeUrlIsLoaded(), "Valid user is not on Hpage");
        }
    }
}
