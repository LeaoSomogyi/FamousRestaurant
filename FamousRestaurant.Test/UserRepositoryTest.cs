using FamousRestaurant.API.DataContext;
using FamousRestaurant.API.Repositories;
using FamousRestaurant.API.Units;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Models = FamousRestaurant.Domain.Models;

namespace FamousRestaurant.Test
{
    public class UserRepositoryTest
    {
        private readonly SqliteConnection sqliteConnection = new SqliteConnection("DataSource=:memory:");
        private readonly DbContextOptions<ApplicationContext> dbContextOptions;

        public UserRepositoryTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(sqliteConnection)
                .Options;
        }

        #region "  Ok  "

        [Fact]
        public async void Insert_User_Database_Ok()
        {
            try
            {
                //Add memory database
                sqliteConnection.Open();

                using (ApplicationContext context = new ApplicationContext(dbContextOptions))
                {
                    //Create schema in database
                    context.Database.EnsureCreated();

                    //Create UnitOfWork with memory database context
                    UnitOfWork unitOfWork = new UnitOfWork(context);

                    //Create User repository
                    UserRepository repository = new UserRepository(unitOfWork);

                    Models.User user = new Models.User()
                    {
                        Email = "testing@gmail.com",
                        Password = "12345678@..",
                        Name = "Test User",
                        Id = Guid.NewGuid()
                    };

                    //Insert user on database
                    repository.Save(user);

                    //Dispose UnitOfWork to commit changes
                    unitOfWork.Dispose();
                }

                //Using another instance of context to make sure previous data was saved
                using (ApplicationContext context = new ApplicationContext(dbContextOptions))
                {
                    //Create UnitOfWork with memory database context
                    UnitOfWork unitOfWork = new UnitOfWork(context);

                    //Create User repository
                    UserRepository repository = new UserRepository(unitOfWork);

                    IEnumerable<Models.User> users = await repository.GetAllAsync();

                    //Assert
                    Assert.True(users.Count() == 1);
                    Assert.Equal("testing@gmail.com", users.FirstOrDefault().Email);
                }
            }
            finally
            {
                sqliteConnection.Close();
            }
        }

        #endregion

        #region "  NOk  "

        [Fact]
        public async void User_Login_Failed()
        {
            try
            {
                sqliteConnection.Open();

                using (ApplicationContext context = new ApplicationContext(dbContextOptions))
                {
                    //Create schema in database
                    context.Database.EnsureCreated();

                    //Create UnitOfWork with memory database context
                    UnitOfWork unitOfWork = new UnitOfWork(context);

                    //Create User repository
                    UserRepository repository = new UserRepository(unitOfWork);

                    Models.User user = new Models.User()
                    {
                        Email = "testing@gmail.com",
                        Password = "12345678@..",
                        Name = "Test User",
                        Id = Guid.NewGuid()
                    };

                    //Insert user on database
                    repository.Save(user);

                    //Dispose UnitOfWork to commit changes
                    unitOfWork.Dispose();
                }

                //Using another instance of context to make sure previous data was saved
                using (ApplicationContext context = new ApplicationContext(dbContextOptions))
                {
                    //Create UnitOfWork with memory database context
                    UnitOfWork unitOfWork = new UnitOfWork(context);

                    //Create User repository
                    UserRepository repository = new UserRepository(unitOfWork);

                    //Arrange
                    string email = "testing@gmail.com";
                    string password = "123456";

                    var users = await repository.SearchAsync(u => u.Email.Equals(email) && u.Password.Equals(password));

                    //Assert
                    Assert.True(users.Count() == 0);                    
                }
            }
            finally
            {
                sqliteConnection.Close();
            }
        }

        #endregion
    }
}
