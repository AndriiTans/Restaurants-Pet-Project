using System;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService : IRestaurantsService
    {
        private IRestaurantsRepository _restaurantsRepository;
        private ILogger<RestaurantsService> _logger;

        public RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger)
        {
            _restaurantsRepository = restaurantsRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            var restaurants = await _restaurantsRepository.GetAllAsync();
            _logger.LogInformation("Getting all restaurants");

            var restaurantsDtos = restaurants.Select(RestaurantDto.FromEntity);

            return restaurantsDtos!;
        }

        public async Task<RestaurantDto?> GetById(int id)
        {
            _logger.LogInformation($"Getting restaurant by {id}");

            var restaurant = await _restaurantsRepository.GetByIdAsync(id);

            var restaurantDto = RestaurantDto.FromEntity(restaurant);

            return restaurantDto;
        }
    }
}

