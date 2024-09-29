using System;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Swagger.Examples.Restaurant;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.SeedRestaurants;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants.Queries.SeedRestaurants;
using Restaurants.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

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
        //public async Task<IActionResult> GetAll()
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            //var restaurants = await _restaurantsService.GetAllRestaurants();
            var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await _mediator.Send(new DeleteRestaurantCommand(id));

            //var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));

            //if (isDeleted)
            //{
            //    return NoContent();
            //}

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
        {
            //var restaurant = await _restaurantsService.GetById(id);
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));

            //Handling by middleware
            //if (restaurant is null)
            //{
            //    return NotFound();
            //}

            return Ok(restaurant);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(CreateRestaurantCommand), typeof(CreateRestaurantExample))]
        //public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            //int id = await _restaurantsService.Create(createRestaurantDto);
            int id = await _mediator.Send(command);

            //return Ok(id);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);

            return NoContent();

            //var isUpdated = await _mediator.Send(command);

            //if (isUpdated)
            //{
            //    return NoContent();
            //}
        }

        [HttpPost("seed")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SeedRestaurants([FromForm] SeedRestaurantsCommand command)
        {
            // Delegate file handling and seeding logic to the service/command handler
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("Database seeded successfully.");
            }

            return BadRequest("Failed to seed the database.");
        }

        //[HttpPost("seed")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> SeedRestaurants([FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("No file uploaded or the file is empty.");
        //    }

        //    // Delegate file handling and seeding logic to the service/command handler
        //    var result = await _mediator.Send(new SeedRestaurantsCommand { File = file });

        //    if (result)
        //    {
        //        return Ok("Database seeded successfully.");
        //    }

        //    return BadRequest("Failed to seed the database.");
        //}

    }
}

