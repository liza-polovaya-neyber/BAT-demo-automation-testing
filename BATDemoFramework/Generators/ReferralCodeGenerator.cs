using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Generators
{
    class ReferralCodeGenerator
    {
        public static string GenerateReferralCode(int length)
        {
            Random random = new Random();
            string s = string.Empty;
            for(int i = 1; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;

        }
      

        
    }
}
