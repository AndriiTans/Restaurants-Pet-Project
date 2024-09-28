using System;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpGet("seed")]
        //public async Task<IActionResult> SeedRestaurants()
        //{
        //    //var restaurants = await _restaurantsService.GetAllRestaurants();
        //    var restaurants = await _mediator.Send(new SeedRestaurantsQuery());
        //    return Ok(restaurants);
        //}

        //[HttpPost("seed")]
        //public async Task<IActionResult> SeedRestaurants([FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("No file uploaded or file is empty.");
        //    }

        //    // Read the file's content
        //    string jsonData;
        //    using (var streamReader = new StreamReader(file.OpenReadStream()))
        //    {
        //        jsonData = await streamReader.ReadToEndAsync();
        //    }

        //    // Deserialize the JSON content into Restaurant entities
        //    var restaurants = JsonSerializer.Deserialize<IEnumerable<RestaurantDto>>(jsonData, new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    });

        //    if (restaurants == null)
        //    {
        //        return BadRequest("Invalid JSON data.");
        //    }

        //    // Send the list of restaurants to the mediator for seeding
        //    var result = await _mediator.Send(new SeedRestaurantsCommand { RestaurantsDtos = restaurants });

        //    if (result)
        //    {
        //        return Ok("Database seeded successfully.");
        //    }

        //    return BadRequest("Failed to seed database.");
        //}

        [HttpPost("seed")]
        public async Task<IActionResult> SeedRestaurants([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or the file is empty.");
            }

            // Delegate file handling and seeding logic to the service/command handler
            var result = await _mediator.Send(new SeedRestaurantsCommand { File = file });

            if (result)
            {
                return Ok("Database seeded successfully.");
            }

            return BadRequest("Failed to seed the database.");
        }

    }
}

