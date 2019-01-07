using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Models
{
    public class AddressModel
    {
        public string FlatNumber { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public bool AddedManually { get; set; }
        public int MovedInMonth { get; set; }
        public int MovedInYear { get; set; }
    }
}
