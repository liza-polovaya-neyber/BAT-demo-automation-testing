using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Steps.Then
{
    public class UserIsAtHomePage
    {
        public static void IsAt()
        {
            Assert.IsTrue(Pages.Home.IsAtUrl(), "Valid user is not on Hpage");
        }
    }
}
