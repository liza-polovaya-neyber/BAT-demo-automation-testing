using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace BATDemoFramework.Helpers
{
    public class RestClient
    {
        private static readonly HttpClient client = new HttpClient();

        public RestClient()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        public void UpdateAuthenticationHeader(string accessToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        public async Task<T> ExecuteAsync<T>(string resource, string method, object data = null)
        {
            var json = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod(method), resource)
            {
                Content = content
            };
            var response = await client.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Failed to perfom Http request. {await response.Content.ReadAsStringAsync()}");
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task ExecuteAsyncNoResponseExpected(string resource, string method, object data = null)
        {
            var json = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod(method), resource)
            {
                Content = content
            };
            var response = await client.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Failed to perfom Http request. {await response.Content.ReadAsStringAsync()}");
            }
            else return;
        }
    }
}