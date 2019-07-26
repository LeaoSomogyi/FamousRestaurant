using FamousRestaurant.API.Controllers;
using FamousRestaurant.Domain.Extensions;
using FamousRestaurant.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using DTO = FamousRestaurant.Domain.DTO;
using Models = FamousRestaurant.Domain.Models;

namespace FamousRestaurant.Test
{
    public class UserControllerTest : BaseTest
    {
        public UserControllerTest() : base() { }

        #region "  Ok  "

        [Fact]
        public void Post_User_Ok()
        {
            try
            {
                SqliteConnection.Open();

                //Arrange
                IRepository<Models.User> repository = GetRepository<Models.User>();

                UserController controller = new UserController(repository);

                DTO.User user = new DTO.User()
                {
                    Email = "testing@gmail.com",
                    Name = "Test",
                    Password = "123456"
                };

                IActionResult result = controller.Save(user);

                //Assert
                Assert.True(result is OkObjectResult);

            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public async void Post_Login_Ok()
        {
            try
            {
                SqliteConnection.Open();

                //Save an user to login
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    Models.User user = new Models.User()
                    {
                        Email = "testing@gmail.com",
                        Id = Guid.Empty,
                        Name = "Test",
                        Password = "12345678".Cript()
                    };

                    repository.Save(user);
                }

                //Need to get another one because the first one is disposed
                IRepository<Models.User> repo = GetRepository<Models.User>();

                UserController controller = new UserController(repo);

                DTO.User userDto = new DTO.User()
                {
                    Email = "testing@gmail.com",
                    Password = "12345678"
                };

                IActionResult result = await controller.Login(userDto);

                //Assert
                Assert.True(result is OkObjectResult);
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        #endregion

        #region "  NOk  "

        //to be created

        #endregion
    }
}
