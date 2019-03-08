using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.NeyberPages.Profile
{
    public class AdditionalDetailsPage
    {
        public bool IsAtUrl()
        {
            return Browser.Url.Contains(Urls.AdditionalDetails);
        }

        public bool WaitUntilUrlIsLoaded()
        {
            Browser.WaitUntilUrlIsLoaded(Urls.AdditionalDetails, 60);
            return Pages.AdditionalDetails.IsAtUrl();
        }
    }
}
