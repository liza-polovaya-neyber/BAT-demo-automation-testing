using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Helpers;
using BATDemoSalesForce.Models;
using BATDemoSalesForce.Services;

namespace BATDemoSalesForce.Repos
{
    public class ProfileRepository : SalesForceRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(ISalesForceService context)
            : base(context, "Contact")
        {
        }

        public async Task<string> CreateAsync(Profile entity)
        {
            var task = await this.context.CreateAsync(this.collectionName, entity);

            return task.Id;
        }


        public Task<Profile> GetByEmailAsync(string email)
        {
            return GetBy("Email =", email);
        }

        public Task<Profile> GetBySecondaryEmailAsync(string email)
        {
            return GetBy("Secondary_Email__c =", email.ToLower());
        }

        public async Task<bool> IsEmailExisting(string email, string secondaryEmail = null)
        {
            var conditions = new NameValueCollection() { { "Email =", email }, { "OR Secondary_Email__c =", email.ToLower() } };
            if (!string.IsNullOrWhiteSpace(secondaryEmail))
            {
                conditions.Add("OR Email =", secondaryEmail);
                conditions.Add("OR Secondary_Email__c =", secondaryEmail.ToLower());
            }

            var task = await this.context.QueryAsync<BorrowerClient>(
                SOQLQueryBuilder.SELECT(
                    new List<string>() { "Id" },
                    collectionName,
                   conditions));

            return task.Records.FirstOrDefault() != null;
        }

        public async Task<Profile> GetBy(string fieldAndOperator, string value)
        {
            var task = await this.context.QueryAsync<Profile>(
                SOQLQueryBuilder.SELECT(
                    ModelHelper.GetApiPropertyNames(typeof(Profile)),
                    collectionName,
                    new NameValueCollection() { { fieldAndOperator, value } }));

            return task.Records.FirstOrDefault();
        }
    }
}
