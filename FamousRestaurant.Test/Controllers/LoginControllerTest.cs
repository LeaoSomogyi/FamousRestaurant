using FamousRestaurant.API.Controllers;
using FamousRestaurant.Domain.Configurations;
using FamousRestaurant.Domain.Contracts;
using FamousRestaurant.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Xunit;
using DTO = FamousRestaurant.Domain.DTO;
using Model = FamousRestaurant.Domain.Models;

namespace FamousRestaurant.Test.Controllers
{
    public class LoginControllerTest : BaseTest
    {
        public LoginControllerTest() : base() { }

        #region "  Ok  "

        [Fact]
        public async void Login_Ok()
        {
            try
            {
                SqliteConnection.Open();

                //insert user on database
                IActionResult saveResult = SaveUser();

                //Arrange
                ILoginService loginService = GetLoginService();

                LoginController controller = new LoginController(loginService);

                DTO.User user = new DTO.User()
                {
                    Email = "testing@gmail.com",
                    Password = "12345678"
                };

                //Act
                IActionResult result = await controller.Login(user);

                //Assert
                Assert.True(result is OkObjectResult);
                Assert.True(((ObjectResult)result).Value is Model.Token);

            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        #endregion

        #region "  NOk  "

        [Fact]
        public async void Login_NOk()
        {
            try
            {
                SqliteConnection.Open();

                //insert user on database
                IActionResult saveResult = SaveUser();

                //Arrange
                ILoginService loginService = GetLoginService();

                LoginController controller = new LoginController(loginService);

                DTO.User user = new DTO.User()
                {
                    Email = "testing@gmail.com",
                    Password = "123456"
                };

                //Act
                IActionResult result = await controller.Login(user);

                //Assert
                Assert.True(result is BadRequestObjectResult);
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        #endregion

        #region "  Private Methods  "

        /// <summary>
        /// Method to create a instance of ILoginService with required constructor
        /// </summary>
        /// <returns>New instance of ILoginService</returns>
        private ILoginService GetLoginService()
        {
            IRepository<Model.User> repository = GetRepository<Model.User>();

            TokenConfigurations tokenConfig = new TokenConfigurations() { Seconds = 6000 };

            SigningConfigurations signinConfig = new SigningConfigurations();

            return new LoginService(repository, tokenConfig, signinConfig);
        }

        /// <summary>
        /// Method to save User using the Controller
        /// </summary>
        /// <returns>Post IActionResult</returns>
        private IActionResult SaveUser()
        {
            using (IRepository<Model.User> repository = GetRepository<Model.User>())
            {
                UserController controller = new UserController(repository);

                DTO.User user = new DTO.User()
                {
                    Name = "Testing User",
                    Email = "testing@gmail.com",
                    Password = "12345678"
                };

                return controller.Save(user);
            }
        }

        #endregion
    }
}
