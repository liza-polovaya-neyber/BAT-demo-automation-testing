using RestSharp;
using Salesforce.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoSalesForce.Services
{
    public interface ISalesForceService
    {
        Task<SuccessResponse> CreateAsync(string objectName, object record);
        Task<SuccessResponse> UpdateAsync(string objectName, string recordId, object record);
        Task<SuccessResponse> UpsertExternalAsync(string objectName, string externalFieldName, string externalId, object record);
        Task<bool> DeleteAsync(string objectName, string recordId);

        Task<T> QueryByIdAsync<T>(string objectName, string recordId);
        Task<T> RecentAsync<T>(int limit = 200);
        Task<List<T>> SearchAsync<T>(string query);
        Task<QueryResult<T>> QueryAsync<T>(string query);
        Task<QueryResult<T>> QueryContinuationAsync<T>(string nextRecordsUrl);
        Task<T> GetUpdated<T>(string objectName, DateTime startDateTime, DateTime endDateTime);
        Task<T> GetDeleted<T>(string objectName, DateTime startDateTime, DateTime endDateTime);
        Task<DescribeGlobalResult<T>> GetObjectsAsync<T>();

        Task<T> UserInfo<T>(string url);
        Task<T> BasicInformationAsync<T>(string objectName);
        Task<T> DescribeAsync<T>(string objectName);
        Task<T> DescribeLayoutAsync<T>(string objectName);
        Task<T> DescribeLayoutAsync<T>(string objectName, string recordTypeId);

        //Apex services 
        Task<T> GetApexRecordAsync<T>(string serviceRoute, string recordId);
        Task<T> ExecuteApexAsync<T>(string serviceRoute, Method method, object inputObject);

        void Dispose();
    }
}
