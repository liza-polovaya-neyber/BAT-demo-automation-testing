using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.WebDriverManager.enums
{
    public static class EnumHelper
    {
        public static RunEnv GetRunEnv(string value)
        {
            return (RunEnv)Enum.Parse(typeof(RunEnv), value.ToUpper(), ignoreCase: true);
        }
    }
}
