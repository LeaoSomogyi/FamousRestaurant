using FamousRestaurant.Infra.Contracts;
using FamousRestaurant.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FamousRestaurant.Infra.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region "  IDisposable  "

        public void Dispose()
        {
            _unitOfWork.Commit();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region "  IRepository<T>  "

        public async Task<IEnumerable<T>> GetAllAsync()
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

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression)
        {
            return await _unitOfWork.Context.Set<T>().Where(expression).ToListAsync();
        }

        public void Update(T entity)
        {
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        #endregion
    }
}
