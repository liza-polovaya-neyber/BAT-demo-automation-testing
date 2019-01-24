using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Services.Configuration;

namespace BATDemoSalesForce.Services
{
    public interface IConfigurationService
    {
        SalesForceSection GetSection();
    }
}
