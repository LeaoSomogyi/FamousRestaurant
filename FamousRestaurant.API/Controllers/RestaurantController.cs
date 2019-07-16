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
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRepository<Models.Restaurant> _repository;

        public RestaurantController(IRepository<Models.Restaurant> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Save([FromBody]DTO.Restaurant restaurant)
        {
            try
            {
                Models.Restaurant result = _repository.Save(new Models.Restaurant(restaurant));

                return Ok(new DTO.Restaurant(result));
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
                IEnumerable<Models.Restaurant> result = await _repository.GetAllAsync();

                List<DTO.Restaurant> dtos = result.Select(r => new DTO.Restaurant(r)).ToList();

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

        [HttpDelete]
        [Route("")]
        public IActionResult Remove([FromBody]DTO.Restaurant restaurant)
        {
            try
            {
                _repository.Remove(new Models.Restaurant(restaurant));

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

        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody]DTO.Restaurant restaurant)
        {
            try
            {
                _repository.Update(new Models.Restaurant(restaurant));

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