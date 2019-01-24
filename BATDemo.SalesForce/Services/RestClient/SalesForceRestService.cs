using Newtonsoft.Json;
using RestSharp;
using Salesforce.Common;
using Salesforce.Common.Models;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Helpers;
using BATDemoSalesForce.Services.SalesForceAuthentication;

namespace BATDemoSalesForce.Services.RestClient
{
    public class SalesForceRestService : ISalesForceRestService
    {
        private string apiVersion;
        private string instanceUrl;
        private readonly RestSharp.RestClient client;
        private readonly ISalesForceAuthenticationService authenticationService;

        public SalesForceRestService(ISalesForceAuthenticationService authenticationService)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            this.authenticationService = authenticationService;

            var authResult = authenticationService.Authenticate();

            apiVersion = authResult.ApiVersion;
            instanceUrl = authResult.InstanceUrl;

            client = new RestSharp.RestClient();
        }

        public async Task<T> ExecuteAsync<T>(string resource, Method method)
        {
            return await ExecuteAsync<T>(string.Empty, resource, method, null);
        }

        public async Task<T> ExecuteAsync<T>(string resource, Method method, object data)
        {
            return await ExecuteAsync<T>(string.Empty, resource, method, data);
        }

        public async Task<T> ExecuteApexAsync<T>(string serviceRoute, Method method, object data)
        {
            return await ExecuteAsync<T>(serviceRoute, string.Empty, method, data);
        }

        public async Task<T> ExecuteAsync<T>(string serviceRoute, string resource, Method method, object data)
        {
            var response = await ExecuteAsync(serviceRoute, resource, method, data);

            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IRestResponse> ExecuteAsync(string serviceRoute, string resource, Method method, object data)
        {
            var request = new RestRequest(method);

            if (!string.IsNullOrEmpty(resource))
            {
                request.Resource = resource;
            }

            if (string.IsNullOrEmpty(serviceRoute))
            {
                client.BaseUrl = new Uri(string.Format("{0}/services/data/{1}/", instanceUrl, apiVersion));
            }
            else
            {
                client.BaseUrl = new Uri(string.Format("{0}/services/apexrest/{1}", instanceUrl, serviceRoute));
            }

            if (data != null)
            {
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("application/json;charset=utf-8", JsonConvert.SerializeObject(data), ParameterType.RequestBody);
            }

            IRestResponse response = await ExecuteRequestWithRetry(request, serviceRoute, resource, method);

            if (response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.InternalServerError ||
                response.StatusCode == HttpStatusCode.NotFound)
            {
                string errorMessage, errorDescription;
                try
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponses>(response.Content).First();
                    errorMessage = errorResponse.ErrorCode;
                    errorDescription = errorResponse.Message;
                }
                catch (Exception)
                {
                    errorMessage = "API call to SalesForce returned a Bad Request.";
                    errorDescription = response.Content;
                }

                throw new ForceException(errorMessage, errorDescription);
            }

            return response;
        }

        private async Task<IRestResponse> ExecuteRequestWithRetry(RestRequest request, string serviceRoute, string resource, Method method)
        {
            int maximumAttempts;
            if (!int.TryParse(ConfigurationManager.AppSettings["SalesForce.HttpRequests.MaxAttempts"], out maximumAttempts))
            {
                maximumAttempts = 2;
            }

            RestRequestCounter requestCounter = new RestRequestCounter(maximumAttempts);

            IRestResponse response = null;
            while (requestCounter.ShouldKeepTrying())
            {
                response = await Execute(request, serviceRoute, resource, method, requestCounter.ForceNewAccessToken);

                requestCounter.MarkAttempt(response.StatusCode != HttpStatusCode.Unauthorized);
            }

            if (!requestCounter.IsSuccessful)
            {
                throw new ForceException(Error.AuthenticationFailure,
                    $"{requestCounter.CurrentAttempt} failed attempts to send authorised SF request");
            }

            return response;
        }

        private async Task<IRestResponse> Execute(RestRequest request, string serviceRoute, string resource, Method method, bool forceNewToken)
        {
            RemoveExistingAuthHeaders(request);

            var authResult = authenticationService.Authenticate(forceNewToken);
            request.AddHeader("Authorization", string.Format("Bearer {0}", authResult.AccessToken));

            var profiler = MiniProfiler.Current;

            IRestResponse response;
            response = await client.ExecuteTaskAsync(request);

            return response;
        }

        

        private static void RemoveExistingAuthHeaders(RestRequest request)
        {
            if (request.Parameters != null && request.Parameters.Any())
            {
                var authHeaders = request.Parameters.Where(x => x.Name == "Authorization");
                if (authHeaders.Any())
                {
                    authHeaders.ToList().ForEach(x => request.Parameters.Remove(x));
                }
            }
        }
    }
}
