using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.EmailService
{
    public class NeyberUrls
    {
        public static string NeyberUrlMatchingPattern = ConfigurationManager.AppSettings["ProfileUrl"] + "/";
    }
}
