using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamousRestaurant.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DTO = FamousRestaurant.Domain.DTO;
using Model = FamousRestaurant.Domain.Models;

namespace FamousRestaurant.API.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> Login(DTO.User user)
        {
            try
            {
                var token = await _loginService.DoLogin(new Model.User(user.Email, user.Password));

                if (token == null)
                    return Unauthorized();
                else
                    return Ok(token);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }
        }
    }
}