using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BATDemoSalesForce.Helpers
{
    public static class SOQLQueryBuilder
    {
        public static string SELECT(List<string> fields, string collection, NameValueCollection conditions, int limit = 1)
        {
            string built_fields = String.Join(", ", fields.Where(s => !string.IsNullOrEmpty(s)));

            string built_conditions = "";
            foreach (string key in conditions.Keys)
            {
                built_conditions += $"{key} '{Escape(conditions[key])}'";
            }

            return $"SELECT {built_fields} FROM {collection} WHERE {built_conditions} LIMIT {limit}";
        }

        private static string Escape(string value)
        {
            Regex re = new Regex(@"(['\\])");
            return re.Replace(value, @"\$1");
        }
    }
}
