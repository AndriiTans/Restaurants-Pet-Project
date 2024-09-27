using System;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _restaurantsService.GetAllRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var restaurant = await _restaurantsService.GetById(id);

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            int id = await _restaurantsService.Create(createRestaurantDto);

            //return Ok(id);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}

