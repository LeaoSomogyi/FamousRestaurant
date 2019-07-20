using FamousRestaurant.API.DataContext;
using FamousRestaurant.API.Repositories;
using FamousRestaurant.API.Units;
using FamousRestaurant.Domain.Contracts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FamousRestaurant.Test
{
    public class BaseRepositoryTest
    {
        /// <summary>
        /// SQLite Connection to use in all test classes
        /// </summary>
        public readonly SqliteConnection SqliteConnection;

        public BaseRepositoryTest()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
        }

        /// <summary>
        /// Method to create typed repository with SQLite options
        /// </summary>
        /// <typeparam name="T">Model class</typeparam>
        /// <returns>The typed repository of the model</returns>
        public IRepository<T> GetRepository<T>() where T : class
        {
            //Set DbContext options to use SQLite
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(SqliteConnection)
                .Options;

            //Create DbContext
            ApplicationContext context = new ApplicationContext(dbContextOptions);

            //Create schema in database
            context.Database.EnsureCreated();

            //Create UnitOfWork with memory database context
            IUnitOfWork unitOfWork = new UnitOfWork(context);

            //return typed repository
            return new BaseRepository<T>(unitOfWork);
        }
    }
}
