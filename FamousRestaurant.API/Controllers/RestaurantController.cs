using FamousRestaurant.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FamousRestaurant.API.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRepository<Model.Restaurant> _repository;

        public RestaurantController(IRepository<Model.Restaurant> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Save([FromBody]DTO.Restaurant restaurant)
        {
            try
            {
                Model.Restaurant result = _repository.Save(new Model.Restaurant(restaurant));

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                System.Collections.Generic.IEnumerable<Model.Restaurant> result = await _repository.GetAll();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("")]
        public IActionResult Remove([FromBody]DTO.Restaurant restaurant)
        {
            try
            {
                _repository.Remove(new Model.Restaurant(restaurant));

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }
    }
}