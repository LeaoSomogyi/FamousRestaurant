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

        #endregion

        #region "  NOk  "

        //to be created

        #endregion
    }
}
