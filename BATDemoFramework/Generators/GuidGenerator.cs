using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.Generators
{
    public static class GuidGenerator
    {
        public static string GenerateGuid()
        {
            Guid id = Guid.NewGuid();
            return id.ToString();
        }
    }
}
