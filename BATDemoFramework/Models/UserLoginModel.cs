using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Models
{
    public class UserLoginModel
    {
        public string TenantName { get; set; }
        public string Title { get; set; }
        public string Surname { get; internal set; }
        public string FirstName { get; internal set; }
        public DateTime DoB { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Password { get; set; }
        public string Feedback { get; set; }
        public bool AffinityTermsAndPrivacyPolicyAccepted { get; set; }
        public bool AgreedPoliceMutualMarketing { get; set; }
        public bool OptOutEmailPolicyAccepted { get; set; }
        public bool TermsAndPrivacyPolicyAccepted { get; set; }
    }
}
