using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Neyber.Atlas.Services.Services
{
    public class UserRestService
    {
        private readonly IRestClient client;

        public UserRestService()
        {
            this.client = new RestClient();
        }

        public void AddBaseUrl(string baseUrl)
        {
            this.client.BaseUrl = new Uri(baseUrl);
        }

        public void AddCertificate(X509Certificate2 cert)
        {
            client.ClientCertificates = new X509CertificateCollection() { cert };
        }

        public void AddDefaultHeader(string name, string value)
        {
            this.client.RemoveDefaultParameter(name);
            this.client.AddDefaultHeader(name, value);
        }

        public async Task<T> ExecuteAsync<T>(string resource, Method method, object data = null)
        {
            var response = await ExecuteAsync(resource, method, data);

            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IRestResponse> ExecuteAsync(string resource, Method method, object data = null)
        {
            var request = new RestRequest(method);

            if (!string.IsNullOrEmpty(resource))
            {
                request.Resource = resource;
            }

            if (data != null)
            {
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("application/json;charset=utf-8", JsonConvert.SerializeObject(data), ParameterType.RequestBody);
            }

            return await this.client.ExecuteTaskAsync(request);
        }

        public async Task<T> TryExecuteAsync<T>(string resource, Method method, object data = null)
        {
            try
            {
                var response = await ExecuteAsync(resource, method, data);
               return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IRestResponse> ExecuteWithParametersAsync(string resource, Method method, Dictionary<string, string> parameters)
        {
            var request = new RestRequest(method);

            if (!string.IsNullOrEmpty(resource))
            {
                request.Resource = resource;
            }

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value, ParameterType.QueryString);
                }
            }

            return await this.client.ExecuteTaskAsync(request);
        }
    }
}