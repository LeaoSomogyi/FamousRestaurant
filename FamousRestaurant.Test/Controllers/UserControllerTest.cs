using FamousRestaurant.API.Controllers;
using FamousRestaurant.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using DTO = FamousRestaurant.Domain.DTO;
using Models = FamousRestaurant.Domain.Models;

namespace FamousRestaurant.Test.Controllers
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
                IActionResult result = SaveUser();

                //Assert
                Assert.True(result is OkObjectResult);

            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public async void Get_All_Ok()
        {
            try
            {
                SqliteConnection.Open();

                //Insert user
                IActionResult saveResult = SaveUser();

                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    UserController controller = new UserController(repository);

                    //Act
                    IActionResult getResult = await controller.GetAll();

                    //Assert
                    Assert.True(saveResult is OkObjectResult);
                    Assert.True(getResult is OkObjectResult);
                    Assert.True(((OkObjectResult)getResult).Value is IEnumerable<DTO.User>);
                }

            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public void Put_User_Ok()
        {
            try
            {
                SqliteConnection.Open();

                IActionResult saveResult = SaveUser();

                //Update user
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    UserController controller = new UserController(repository);

                    //Act
                    DTO.User user = ((OkObjectResult)saveResult).Value as DTO.User;

                    //Set password because save API not returns it
                    user.Password = "12345678";
                    user.Email = "changed@gmail.com";

                    IActionResult result = controller.Update(user);

                    //Assert
                    Assert.True(saveResult is OkObjectResult);
                    Assert.True(result is OkResult);
                }
            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public void Delete_User_Ok()
        {
            try
            {
                SqliteConnection.Open();

                IActionResult saveResult = SaveUser();

                //Remove user
                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    UserController controller = new UserController(repository);

                    //Act
                    DTO.User user = ((OkObjectResult)saveResult).Value as DTO.User;

                    //Set password because save API not returns it
                    user.Password = "12345678";

                    IActionResult result = controller.Remove(user);

                    //Assert
                    Assert.True(saveResult is OkObjectResult);
                    Assert.True(result is OkResult);
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
        public void Save_User_Email_NOk()
        {
            try
            {
                SqliteConnection.Open();

                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    UserController controller = new UserController(repository);

                    DTO.User user = GetDTOUser();

                    user.Email = null;

                    IActionResult result = controller.Save(user);

                    Assert.True(result is BadRequestObjectResult);
                }

            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public void Save_User_Password_NOk()
        {
            try
            {
                SqliteConnection.Open();

                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    UserController controller = new UserController(repository);

                    DTO.User user = GetDTOUser();

                    user.Password = null;

                    IActionResult result = controller.Save(user);

                    Assert.True(result is BadRequestObjectResult);
                }

            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        [Fact]
        public void Save_User_Name_NOk()
        {
            try
            {
                SqliteConnection.Open();

                using (IRepository<Models.User> repository = GetRepository<Models.User>())
                {
                    UserController controller = new UserController(repository);

                    DTO.User user = GetDTOUser();

                    user.Name = null;

                    IActionResult result = controller.Save(user);

                    Assert.True(result is BadRequestObjectResult);
                }

            }
            finally
            {
                SqliteConnection.Close();
            }
        }

        #endregion

        #region "  Private Methods  "

        /// <summary>
        /// Create a User DTO
        /// </summary>
        /// <returns>User DTO</returns>
        private DTO.User GetDTOUser()
        {
            return new DTO.User()
            {
                Email = "testing@gmail.com",
                Name = "Test",
                Password = "123456"
            };
        }

        /// <summary>
        /// Method to save User using the Controller
        /// </summary>
        /// <returns>Post IActionResult</returns>
        private IActionResult SaveUser()
        {
            using (IRepository<Models.User> repository = GetRepository<Models.User>())
            {
                UserController controller = new UserController(repository);

                DTO.User user = GetDTOUser();

                return controller.Save(user);
            }
        }

        #endregion
    }
}
