using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BATDemoSalesForce.Models
{
    public class BorrowerClient
    {
        [JsonProperty("Client__c")]
        public string Name { get; set; }
    }
}
