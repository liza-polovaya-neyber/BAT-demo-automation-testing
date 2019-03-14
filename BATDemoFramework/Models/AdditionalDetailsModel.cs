using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Models
{
    public class AdditionalDetailsModel
    {
        public MarketingPreferenceModel UpdateMarketingPreferences { get; set; }

        public SecondaryEmailModel UpdateSecondaryEmail { get; set; }

        public CustomerFeedbackModel UpdateCustomerFeedback { get; set; }
    }
}
