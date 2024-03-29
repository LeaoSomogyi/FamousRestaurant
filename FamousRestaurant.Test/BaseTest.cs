﻿using FamousRestaurant.Domain.Contracts;
using FamousRestaurant.Infra.Contracts;
using FamousRestaurant.Infra.DataContext;
using FamousRestaurant.Infra.Repositories;
using FamousRestaurant.Infra.Units;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FamousRestaurant.Test
{
    public abstract class BaseTest
    {
        /// <summary>
        /// SQLite Connection to use in all test classes
        /// </summary>
        protected readonly SqliteConnection SqliteConnection;

        /// <summary>
        /// Unit Of Work using SQLite DbContext
        /// </summary>
        protected IUnitOfWork UnitOfWork
        {
            get
            {
                return RetrieveUnitOfWork();
            }
        }

        /// <summary>
        /// Default constructor setting SQLite Connection
        /// </summary>
        public BaseTest()
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
            //Create UnitOfWork with memory database context
            IUnitOfWork unitOfWork = RetrieveUnitOfWork();

            //return typed repository
            return new BaseRepository<T>(unitOfWork);
        }

        /// <summary>
        /// Method to create a instance of UnitOfWork with SQLite context
        /// </summary>
        /// <returns>Instance of IUnitOfWork</returns>
        private IUnitOfWork RetrieveUnitOfWork()
        {
            //Set DbContext options to use SQLite
            DbContextOptions<ApplicationContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(SqliteConnection)
                .Options;

            //Create DbContext
            ApplicationContext context = new ApplicationContext(dbContextOptions);

            //Create schema in database
            context.Database.EnsureCreated();

            return new UnitOfWork(context);
        }
    }
}
