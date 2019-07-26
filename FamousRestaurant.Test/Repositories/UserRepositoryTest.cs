using FamousRestaurant.Domain.Contracts;
using FamousRestaurant.Domain.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Models = FamousRestaurant.Domain.Models;

namespace FamousRestaurant.Test
{
    public class UserRepositoryTest : BaseTest
    {
        public UserRepositoryTest() : base() { }

        #region "  Ok  "

        [Fact]
        public async void Insert_User_Ok()
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
                        Password = "12345678@..".Cript(),
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
                            Password = "12345678@..".Cript(),
                            Name = "Test User",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing@hotmail.com",
                            Password = "123456@..".Cript(),
                            Name = "Another User",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing2@gmail.com",
                            Password = "T3st@..".Cript(),
                            Name = "Test2 User",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing3@gmail.com",
                            Password = "87654321@..".Cript(),
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
                    IEnumerable<bool> checks = users.GroupBy(u => u.Id).Select(group => group.Count() == 1);

                    Assert.True(checks.All(c => c == true));
                }
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public async void Delete_User_Ok()
        {
            try
            {
                SqliteConnection.Open();

                //After repository was disposed, the changes are commited
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    //Arrange
                    Models.User user = new Models.User()
                    {
                        Email = "testing@gmail.com",
                        Password = "12345678@..".Cript(),
                        Name = "Test User",
                        Id = Guid.NewGuid()
                    };

                    //Insert user on database
                    repository.Save(user);
                }

                //Using another instance of repository to make sure previous data was saved
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    IEnumerable<Models.User> users = await repository.SearchAsync(u => u.Email.Equals("testing@gmail.com"));

                    Models.User user = users.FirstOrDefault();

                    repository.Remove(user);
                }

                //Using another instance of repository to make sure previous data was removed
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    IEnumerable<Models.User> users = await repository.GetAllAsync();

                    //Assert
                    Assert.True(users.Count() == 0);
                }
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public async void Search_User_Ok()
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
                            Password = "12345678@..".Cript(),
                            Name = "Test",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing@hotmail.com",
                            Password = "123456@..".Cript(),
                            Name = "Test",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing2@gmail.com",
                            Password = "T3st@..".Cript(),
                            Name = "Test2 User",
                            Id = Guid.NewGuid()
                        },
                        new Models.User()
                        {
                            Email = "testing3@gmail.com",
                            Password = "87654321@..".Cript(),
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
                    //Retrieve users which name == Test
                    IEnumerable<Models.User> users = await repository.SearchAsync(u => u.Name.Equals("Test"));

                    //Assert
                    Assert.True(users.Count() == 2);
                }
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public async void Update_User_Ok()
        {
            try
            {
                SqliteConnection.Open();

                //After repository was disposed, the changes are commited
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    //Arrange
                    Models.User user = new Models.User()
                    {
                        Email = "testing@gmail.com",
                        Password = "12345678@..".Cript(),
                        Name = "Test User",
                        Id = Guid.NewGuid()
                    };

                    //Insert user on database
                    repository.Save(user);
                }

                //Using another instance of repository to make sure previous data was saved
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    IEnumerable<Models.User> users = await repository.SearchAsync(u => u.Name.Equals("Test User"));

                    Models.User user = users.FirstOrDefault();

                    user.Email = "changed@gmail.com";

                    repository.Update(user);
                }

                //Using another instance of repository to check if it's trully updated
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    IEnumerable<Models.User> users = await repository.SearchAsync(u => u.Email.Equals("changed@gmail.com"));

                    //Assert
                    Assert.True(users.Count() == 1);
                }
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public async void User_Login_Ok()
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
                        Password = "12345678@..".Cript(),
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
                    string password = "12345678@..".Cript();

                    IEnumerable<Models.User> users = await repository.SearchAsync(u => u.Email.Equals(email) && u.Password.Equals(password));

                    //Assert
                    Assert.True(users.Count() == 1);
                }
            }
            finally
            {
                //release memory database
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
                        Password = "12345678@..".Cript(),
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
                    string password = "123456".Cript();

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

        [Fact]
        public void Insert_Users_Failed()
        {
            try
            {
                SqliteConnection.Open();

                Exception ex = null;

                //Retrieve exception throwed without leave the try block
                ex = Record.Exception(() =>
                {
                    using (IRepository<Models.User> repository = GetRepository<Models.User>())
                    {
                        //try use same PK on both users (forcing exception)
                        Guid code = Guid.NewGuid();

                        //Arrange
                        List<Models.User> users = new List<Models.User>()
                        {
                            new Models.User()
                            {
                                Id = code,
                                Email = "testing@gmail.com",
                                Password = "12345678".Cript(),
                                Name = "First Test"
                            },
                            new Models.User()
                            {
                                Id = code,
                                Email = "testing@hotmail.com",
                                Password = "87654321".Cript(),
                                Name = "Second Test"
                            },
                        };

                        users.ForEach(user =>
                        {
                            repository.Save(user);
                        });
                    }
                });

                Assert.True(ex != null);
                Assert.True(ex is DbUpdateException);
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public void Update_User_Failed()
        {
            try
            {
                SqliteConnection.Open();

                Exception ex = null;

                ex = Record.Exception(() =>
                {
                    using (IRepository<Models.User> repository = GetRepository<Models.User>())
                    {
                        //Arrange
                        Models.User user = new Models.User()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Test Fail",
                            Password = "12345678".Cript(),
                            Email = "testing_fail@gmail.com"
                        };

                        //Try update some unexisting user
                        repository.Update(user);
                    }
                });

                //Assert
                Assert.True(ex != null);
                Assert.True(ex is DbUpdateConcurrencyException);
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public void Delete_User_Failed()
        {
            try
            {
                SqliteConnection.Open();

                Exception ex = null;

                ex = Record.Exception(() =>
                {
                    using (IRepository<Models.User> repository = GetRepository<Models.User>())
                    {
                        //Arrange
                        Models.User user = new Models.User()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Test Fail",
                            Password = "12345678".Cript(),
                            Email = "testing_fail@gmail.com"
                        };

                        //Try delete some unexisting user
                        repository.Remove(user);
                    }
                });

                //Assert
                Assert.True(ex != null);
                Assert.True(ex is DbUpdateConcurrencyException);
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        #endregion
    }
}
