using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BATDemoSalesForce.Services;

namespace BATDemoSalesForce.Repos
{
    /// IRepository definition.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// Returns the T by its given id.
        /// </summary>
        /// <param name="id">The string representing the ObjectId of the entity to retrieve.</param>
        /// <returns>The Entity T.</returns>
        Task<T> GetByIdAsync(string id);

        /// <summary>
        /// Returns a single T by the given criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>A single T matching the criteria.</returns>
        T GetSingle(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Returns All the records of T.
        /// </summary>
        /// <param name="take">Optional parameter. How many records to take from resulting collection.</param>
        /// <returns>IQueryable of T.</returns>
        IQueryable<T> All(int take = 0);

        /// <summary>
        /// Returns the list of T where it matches the criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <param name="take">Optional parameter. How many records to take from resulting collection.</param>
        /// <returns>IQueryable of T.</returns>
        IQueryable<T> All(Expression<Func<T, bool>> criteria, int take = 0);

        /// <summary>
        /// Adds the new entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity including its new ObjectId.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Adds the new entities in the repository.
        /// </summary>
        /// <param name="entities">The entities of type T.</param>
        void Add(IEnumerable<T> entities);

        /// <summary>
        /// Upserts an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The updated entity.</returns>
        Task<T> UpdateAsync(string id, object data);

        /// <summary>
        /// Upserts the entities.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Deletes an entity from the repository by its id.
        /// </summary>
        /// <param name="id">The string representation of the entity's id.</param>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the entities matching the criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        void Delete(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Deletes all entities in the repository.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Counts the total entities in the repository.
        /// </summary>
        /// <returns>Count of entities in the repository.</returns>
        long Count();

        /// <summary>
        /// Checks if the entity exists for given criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>true when an entity matching the criteria exists, false otherwise.</returns>
        bool Exists(Expression<Func<T, bool>> criteria);
    }
}