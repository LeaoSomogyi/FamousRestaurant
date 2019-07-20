using FamousRestaurant.Domain.Contracts;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Models = FamousRestaurant.Domain.Models;

namespace FamousRestaurant.Test
{
    public class UserRepositoryTest : BaseRepositoryTest
    {
        public UserRepositoryTest() : base() { }

        #region "  Ok  "

        [Fact]
        public async void Insert_User_Database_Ok()
        {
            try
            {
                //Add memory database
                SqliteConnection.Open();

                //After repository was disposed, the changes are commited
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    //Arrange
                    Models.User user = new Models.User()
                    {
                        Email = "testing@gmail.com",
                        Password = "12345678@..",
                        Name = "Test User",
                        Id = Guid.NewGuid()
                    };

                    //Insert user on database
                    repository.Save(user);
                }

                //Using another instance of repository to make sure previous data was saved
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    IEnumerable<Models.User> users = await repository.GetAllAsync();

                    //Assert
                    Assert.True(users.Count() == 1);
                    Assert.Equal("testing@gmail.com", users.FirstOrDefault().Email);
                }
            }
            finally
            {
                //release memory database
                SqliteConnection.Close();
            }
        }

        [Fact]
        public async void Get_All_Users_Ok()
        {
            try
            {
                SqliteConnection.Open();

                //After repository was disposed, the changes are commited
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    //Arrange
                    List<Models.User> users = new List<Models.User>()
                    {
                        new Models.User()
                        {
                            Email = "testing@gmail.com",
                            Password = "12345678@..",
                            Name = "Test User",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing@hotmail.com",
                            Password = "123456@..",
                            Name = "Another User",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing2@gmail.com",
                            Password = "T3st@..",
                            Name = "Test2 User",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing3@gmail.com",
                            Password = "87654321@..",
                            Name = "Test3 User",
                            Id = Guid.NewGuid()
                        },
                    };

                    users.ForEach(user =>
                    {
                        //Insert user on database
                        repository.Save(user);
                    });
                }

                //Using another instance of repository to make sure previous data was saved
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    IEnumerable<Models.User> users = await repository.GetAllAsync();

                    //Assert
                    Assert.True(users.Count() == 4);

                    //Check if Primary keys not repeat (expected one user for each key)
                    var checks = users.GroupBy(u => u.Id).Select(group => group.Count() == 1);

                    Assert.True(checks.All(c => c == true)); 
                }
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        #endregion

        #region "  NOk  "

        [Fact]
        public async void User_Login_Failed()
        {
            try
            {
                //Add memory database
                SqliteConnection.Open();

                //After repository was disposed, the changes are commited
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    Models.User user = new Models.User()
                    {
                        Email = "testing@gmail.com",
                        Password = "12345678@..",
                        Name = "Test User",
                        Id = Guid.NewGuid()
                    };

                    //Insert user on database
                    repository.Save(user);
                }

                //Using another instance of repository to make sure previous data was saved
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    //Arrange
                    string email = "testing@gmail.com";
                    string password = "123456";

                    IEnumerable<Models.User> users = await repository.SearchAsync(u => u.Email.Equals(email) && u.Password.Equals(password));

                    //Assert
                    Assert.True(users.Count() == 0);
                }
            }
            finally
            {
                //release memory database
                SqliteConnection.Close();
            }
        }

        #endregion
    }
}
