using RestSharp;
using Salesforce.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Helpers;
using BATDemoSalesForce.Services.RestClient;

namespace BATDemoSalesForce.Services
{
    public class SalesForceService : ISalesForceService
    {
        private readonly ISalesForceRestService client;

        public SalesForceService(ISalesForceRestService client)
        {
            this.client = client;
        }

        public async Task<T> BasicInformationAsync<T>(string objectName)
        {
            return await client.ExecuteAsync<T>(string.Format("sobjects/{0}", objectName), Method.GET);
        }

        public async Task<SuccessResponse> CreateAsync(string objectName, object record)
        {
            return await client.ExecuteAsync<SuccessResponse>(string.Format("sobjects/{0}", objectName), Method.POST, record).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(string objectName, string recordId)
        {
            var response = await client.ExecuteAsync(string.Empty, string.Format("sobjects/{0}/{1}", objectName, recordId), Method.DELETE, null);

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<T> DescribeAsync<T>(string objectName)
        {
            return await client.ExecuteAsync<T>(string.Format("sobjects/{0}/describe/", objectName), Method.GET);
        }

        public async Task<T> DescribeLayoutAsync<T>(string objectName, string recordTypeId)
        {
            return await client.ExecuteAsync<T>(string.Format("sobjects/{0}/describe/layouts/{1}", objectName, recordTypeId), Method.GET);
        }

        public async Task<T> DescribeLayoutAsync<T>(string objectName)
        {
            return await client.ExecuteAsync<T>(string.Format("sobjects/{0}/describe/layouts/", objectName), Method.GET);
        }

        public async Task<T> GetDeleted<T>(string objectName, DateTime startDateTime, DateTime endDateTime)
        {
            return await client.ExecuteAsync<T>(string.Format("sobjects/{0}/deleted/?start={1}&end={2}", objectName, startDateTime, endDateTime), Method.GET);
        }

        public async Task<DescribeGlobalResult<T>> GetObjectsAsync<T>()
        {
            return await client.ExecuteAsync<DescribeGlobalResult<T>>("sobjects", Method.GET);
        }

        public async Task<T> GetUpdated<T>(string objectName, DateTime startDateTime, DateTime endDateTime)
        {
            return await client.ExecuteAsync<T>(string.Format("sobjects/{0}/updated/?start={1}&end={2}", objectName, startDateTime, endDateTime), Method.GET);
        }

        public async Task<QueryResult<T>> QueryAsync<T>(string query)
        {
            var url = string.Format("query?q={0}", Uri.EscapeDataString(query));

            return await client.ExecuteAsync<QueryResult<T>>(url, Method.GET);
        }

        public async Task<T> QueryByIdAsync<T>(string objectName, string recordId)
        {
            var query = string.Format("SELECT {0} FROM {1} WHERE Id = '{2}'",
                string.Join(",", ModelHelper.GetApiPropertyNames(typeof(T))), objectName, recordId);

            var results = await QueryAsync<T>(query);

            return results.Records.FirstOrDefault();
        }

        public async Task<QueryResult<T>> QueryContinuationAsync<T>(string nextRecordsUrl)
        {
            return await client.ExecuteAsync<QueryResult<T>>(nextRecordsUrl, Method.GET);
        }

        public async Task<T> RecentAsync<T>(int limit = 200)
        {
            return await client.ExecuteAsync<T>(string.Format("recent/?limit={0}", limit), Method.GET);
        }

        public async Task<List<T>> SearchAsync<T>(string query)
        {
            return await client.ExecuteAsync<List<T>>(string.Format("search?q={0}", Uri.EscapeDataString(query)), Method.GET);
        }

        public async Task<SuccessResponse> UpdateAsync(string objectName, string recordId, object record)
        {
            return await client.ExecuteAsync<SuccessResponse>(string.Format("sobjects/{0}/{1}", objectName, recordId), Method.PATCH, record);
        }

        public async Task<SuccessResponse> UpsertExternalAsync(string objectName, string externalFieldName, string externalId, object record)
        {
            return await client.ExecuteAsync<SuccessResponse>(string.Format("sobjects/{0}/{1}/{2}", objectName, externalFieldName, externalId), Method.PATCH, record);
        }

        public async Task<T> UserInfo<T>(string url)
        {
            return await client.ExecuteAsync<T>(url, Method.GET);
        }

        public async Task<T> GetApexRecordAsync<T>(string serviceRoute, string recordId)
        {
            return await client.ExecuteApexAsync<T>(string.Format("{0}/{1}", serviceRoute, recordId), Method.GET, null);
        }

        public async Task<T> ExecuteApexAsync<T>(string serviceRoute, Method method, object inputObject)
        {
            return await client.ExecuteApexAsync<T>(serviceRoute, method, inputObject);
        }
        
        public void Dispose()
        {

        }
    }
}
