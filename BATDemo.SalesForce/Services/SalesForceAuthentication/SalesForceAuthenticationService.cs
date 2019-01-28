using Salesforce.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Models;
using BATDemoSalesForce.Services.Configuration;
using BATDemoSalesForce.Services.SalesForceAuthentication;

namespace BATDemoSalesForce.Services.SalesForceAuthentication
{
    public class SalesForceAuthenticationService : ISalesForceAuthenticationService
    { 
        private readonly IConfigurationService configurationService;
        private string instanceUrl;
        private string apiVersion;
        private string accessToken;
        private DateTime lastTokenRefresh;
        private Object lockThis = new Object();

        public SalesForceAuthenticationService(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public AuthenticationResult Authenticate(bool forceNewAccessToken = false)
        {
            lock (lockThis)
            {
                var dateTime = DateTime.UtcNow;
                var salesForceSection = configurationService.GetSection();

                if (ShouldGetNewAccessToken(forceNewAccessToken, salesForceSection, dateTime))
                {
                    var authClient = new AuthenticationClient();
                    try
                    {
                        authClient.UsernamePasswordAsync(
                                salesForceSection.ConsumerKey,
                                salesForceSection.ConsumerSecret,
                                salesForceSection.Username,
                                salesForceSection.Password + salesForceSection.SecurityToken,
                                salesForceSection.LoginUrl)
                            .Wait();
                    }
                    catch (Exception ex)
                    {
                        throw new AuthenticationException("Failed to authenticate with SalesForce", ex.InnerException);
                    }
                    finally
                    {
                        authClient.Dispose();
                    }

                    instanceUrl = authClient.InstanceUrl;
                    apiVersion = authClient.ApiVersion;
                    accessToken = authClient.AccessToken;
                    lastTokenRefresh = dateTime;
                }

                return new AuthenticationResult { AccessToken = accessToken, ApiVersion = apiVersion, InstanceUrl = instanceUrl };
            }
        }

        private bool ShouldGetNewAccessToken(bool forceNewAccessToken, SalesForceSection config, DateTime dateTime)
        {
            return string.IsNullOrEmpty(instanceUrl) ||
                            (dateTime - lastTokenRefresh).TotalHours > config.RefreshTokenIntevalHours ||
                            forceNewAccessToken;
        }
    }
}
