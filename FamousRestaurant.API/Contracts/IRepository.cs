using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FamousRestaurant.API.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> expression);

        T Save(T entity);

        void Remove(T entity);
    }
}
