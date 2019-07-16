using FamousRestaurant.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO = FamousRestaurant.Domain.DTO;
using Models = FamousRestaurant.Domain.Models;


namespace FamousRestaurant.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<Models.User> _repository;

        public UserController(IRepository<Models.User> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Save([FromBody]DTO.User user)
        {
            try
            {
                Models.User result = _repository.Save(new Models.User(user));

                return Ok(new DTO.User(result));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]DTO.User user)
        {
            try
            {
                IEnumerable<Models.User> result = await _repository.SearchAsync(Models.User.RetrieveLoginExpression(user.Email, user.Password));

                if (result.Count() == decimal.Zero)
                {
                    return new UnauthorizedObjectResult("Usuário ou senha incorretos.");
                }
                else
                {
                    DTO.User dto = new DTO.User(result.FirstOrDefault());

                    return Ok(dto);
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<Models.User> result = await _repository.GetAllAsync();

                List<DTO.User> dtos = result.Select(u => new DTO.User(u)).ToList();

                return Ok(dtos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody]DTO.User user)
        {
            try
            {
                _repository.Update(new Models.User(user));

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("")]
        public IActionResult Remove([FromBody]DTO.User user)
        {
            try
            {
                _repository.Remove(new Models.User(user, true));

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }
        }
    }
}