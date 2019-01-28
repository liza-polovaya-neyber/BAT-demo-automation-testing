using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoSalesForce.Helpers
{
    public class RestRequestCounter
    {
        public int MaxAttempts { get; set; }
        public int CurrentAttempt { get; set; }
        public bool IsSuccessful { get; set; }
        public bool ForceNewAccessToken { get; set; }

        public RestRequestCounter(int maxAttempts)
        {
            MaxAttempts = maxAttempts;
        }

        public bool ShouldKeepTrying()
        {
            return !IsSuccessful && CurrentAttempt < MaxAttempts;
        }

        public void MarkAttempt(bool isSuccess)
        {
            if (!isSuccess)
            {
                ForceNewAccessToken = true;
            }

            IsSuccessful = isSuccess;
            CurrentAttempt++;
        }
    }
}
