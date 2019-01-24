using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Services.Configuration;

namespace BATDemoSalesForce.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly string sectionName;

        public ConfigurationService(string sectionName)
        {
            this.sectionName = sectionName;
        }

        public SalesForceSection GetSection()
        {
            return (SalesForceSection)ConfigurationManager.GetSection(sectionName);
        }
    }
}
