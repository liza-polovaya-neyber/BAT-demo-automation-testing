using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoSalesForce.Services.RestClient
{
    public interface ISalesForceRestService
    {
        Task<T> ExecuteAsync<T>(string resource, Method method);

        Task<T> ExecuteAsync<T>(string resource, Method method, object data);

        Task<T> ExecuteApexAsync<T>(string serviceRoute, Method method, object data);

        Task<T> ExecuteAsync<T>(string serviceRoute, string resource, Method method, object data);

        Task<IRestResponse> ExecuteAsync(string serviceRoute, string resource, Method method, object data);
    }
}
