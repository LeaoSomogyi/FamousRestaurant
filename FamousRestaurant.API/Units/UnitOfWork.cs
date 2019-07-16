using Microsoft.EntityFrameworkCore;
using System;

namespace FamousRestaurant.API.Units
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region "  Properties  "

        public DbContext Context { get; }

        #endregion

        #region "  Constructors  "

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        #endregion

        #region "  IUnitOfWork  "        

        public void Commit()
        {
            Context.SaveChanges();
        }

        #endregion

        #region "  IDisposable  "

        private bool _disposed = false;

        public void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Commit();
                Context.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
