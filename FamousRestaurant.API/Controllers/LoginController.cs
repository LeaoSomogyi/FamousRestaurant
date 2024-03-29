﻿using FamousRestaurant.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
                Model.Token token = await _loginService.DoLogin(new Model.User(user.Email, user.Password));

                if (token == null)
                {
                    return BadRequest(new
                    {
                        type = "ERROR",
                        message = "Usuário ou senha incorretos."
                    });
                }
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