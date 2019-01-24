using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoSalesForce.Helpers
{
    public static class ModelHelper
    {
        public static IEnumerable<string> GetApiPropertyNames(Type type, string[] ignore)
        {
            return GetApiPropertyNames(type).Where(x => !ignore.Contains(x)).ToList();
        }

        public static List<string> GetApiPropertyNames(Type type)
        {
            var result = new List<string>();

            var properties = type.GetProperties();

            foreach (var property in properties)
            {

                if (property.CustomAttributes.Count() > 0 &&
                    property.CustomAttributes.First().AttributeType.Equals(typeof(JsonIgnoreAttribute)))
                {
                    continue;
                }

                if (property.CustomAttributes.Count() > 0 &&
                    property.CustomAttributes.First().AttributeType.Equals(typeof(JsonPropertyAttribute)))
                {
                    var customAttribute = property.CustomAttributes.First();
                    if (customAttribute.ConstructorArguments.Any())
                    {
                        result.Add(customAttribute.ConstructorArguments.First().Value.ToString());
                    }
                    else
                    {
                        throw new Exception(String.Format("Can't get a json name for property: {0}", property.Name));
                    }
                }
                else
                {
                    result.Add(property.Name);
                }
            }

            return result;
        }
    }
}
