using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FamousRestaurant.API.Contracts
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Async method to list all data
        /// </summary>
        /// <returns>A collection of records from database</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Async method to search information
        /// </summary>
        /// <param name="expression">Expression to use on where clause</param>
        /// <returns>A collection of records that meets the constraint</returns>
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Save object
        /// </summary>
        /// <param name="entity">A object to be saved</param>
        /// <returns>Same object with Id fiiled</returns>
        T Save(T entity);

        /// <summary>
        /// Remove object
        /// </summary>
        /// <param name="entity">Object to be removed</param>
        void Remove(T entity);

        /// <summary>
        /// Update object
        /// </summary>
        /// <param name="entity">Object to be updated</param>
        void Update(T entity);
    }
}
