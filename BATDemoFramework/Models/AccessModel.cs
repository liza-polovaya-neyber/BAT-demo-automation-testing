using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Models
{
    class AccessModel
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string Roles { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string ProfileId { get; set; }
        public bool HermesUser { get; set; }
    }
}
