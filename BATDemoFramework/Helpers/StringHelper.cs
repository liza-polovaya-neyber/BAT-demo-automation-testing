using BATDemoFramework.EmailServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BATDemoFramework.Helpers
{
    public static class StringHelper
    {
        public static IEnumerable<string> GetURLsWithMatchingPattern(string rawHtml, string matchingPattern)
        {
            Regex urlRx = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase);

            MatchCollection matches = urlRx.Matches(rawHtml);
            foreach (Match match in matches)
            {
                if (match.Value.ToLower().Contains(matchingPattern.ToLower())) 
                {
                    yield return match.Value;
                }
            }

        }
    }
}