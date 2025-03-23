using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Data.Repositories
{
    /// <summary>
    /// Generic repository interface defining common CRUD operations
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets entity by its identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>Entity or null if not found</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>Collection of entities</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Identifier of the added entity</returns>
        Task<int> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity by its identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Checks if entity with specified identifier exists
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>True if entity exists, false otherwise</returns>
        Task<bool> ExistsAsync(int id);
    }
}
