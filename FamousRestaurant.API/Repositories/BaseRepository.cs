using FamousRestaurant.API.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FamousRestaurant.API.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region "  IRepository<T>  "

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public void Remove(T entity)
        {
            try
            {
                _unitOfWork.Context.Set<T>().Remove(entity);
            }
            catch
            {
                throw;
            }
        }

        public T Save(T entity)
        {
            try
            {
                return _unitOfWork.Context.Set<T>().Add(entity).Entity;
            }
            catch
            {
                throw;
            }            
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> expression)
        {
            return await _unitOfWork.Context.Set<T>().Where(expression).ToListAsync();
        }

        #endregion
    }
}
