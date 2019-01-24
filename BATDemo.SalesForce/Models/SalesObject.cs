using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Services;
using Newtonsoft.Json;

namespace BATDemoSalesForce.Models
{
    public abstract class SalesObject : Entity
    {
        [JsonIgnore]
        public abstract string ObjectTypeName { get; }
    }
}
