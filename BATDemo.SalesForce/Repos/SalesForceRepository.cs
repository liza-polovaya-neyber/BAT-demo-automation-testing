using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Models;
using BATDemoSalesForce.Services;

namespace BATDemoSalesForce.Repos
{
    public class SalesForceRepository<T> : IRepository<T> where T : SalesObject
    {
        protected ISalesForceService context;
        protected string collectionName;

        public SalesForceRepository(ISalesForceService context, string collectionName)
        {
            this.context = context;
            this.collectionName = collectionName;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await this.context.QueryByIdAsync<T>(this.collectionName, id);
        }

        public T GetSingle(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> All(int take = 0)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> All(Expression<Func<T, bool>> criteria, int take = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<T> AddAsync(T entity)
        {
            var task = await this.context.CreateAsync(this.collectionName, entity);

            return await GetByIdAsync(task.Id);
        }

        public void Add(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(string id, object data)
        {
            var task = await this.context.UpdateAsync(this.collectionName, id, data);

            return await GetByIdAsync(id);
        }

        public void Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await this.context.DeleteAsync(this.collectionName, id);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException();
        }
    }
}
