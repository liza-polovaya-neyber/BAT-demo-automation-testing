using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Models;

namespace BATDemoSalesForce.Services.SalesForceAuthentication
{
    public interface ISalesForceAuthenticationService
    {
        AuthenticationResult Authenticate(bool forceNewAccessToken = false);
    }
}
