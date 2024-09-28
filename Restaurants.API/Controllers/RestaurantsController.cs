using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        //private readonly IRestaurantsService _restaurantsService;
        private readonly IMediator _mediator;

        //public RestaurantsController(IRestaurantsService restaurantsService)
        public RestaurantsController(IMediator mediator)
        {
            //_restaurantsService = restaurantsService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var restaurants = await _restaurantsService.GetAllRestaurants();
            var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            //var restaurant = await _restaurantsService.GetById(id);
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        //public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            //int id = await _restaurantsService.Create(createRestaurantDto);
            int id = await _mediator.Send(command);

            //return Ok(id);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;

            //return Ok(command);

            var isUpdated = await _mediator.Send(command);

            if (isUpdated)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}

