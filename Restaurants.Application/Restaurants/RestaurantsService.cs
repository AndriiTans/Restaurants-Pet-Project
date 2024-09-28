// Using MediatR instead
//using System;
//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using Restaurants.Application.Restaurants.Dtos;
//using Restaurants.Domain.Entities;
//using Restaurants.Domain.Repositories;

//namespace Restaurants.Application.Restaurants
//{
//    internal class RestaurantsService : IRestaurantsService
//    {
//        private IRestaurantsRepository _restaurantsRepository;
//        private ILogger<RestaurantsService> _logger;
//        private IMapper _mapper;

//        public RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger, IMapper mapper)
//        {
//            _restaurantsRepository = restaurantsRepository;
//            _logger = logger;
//            _mapper = mapper;
//        }

//        // Using CreateRestaurantProfile instead
//        //public async Task<int> Create(CreateRestaurantDto dto)
//        //{
//        //    _logger.LogInformation("Create restaurant");

//        //    var restaurant = _mapper.Map<Restaurant>(dto);

//        //    int id = await _restaurantsRepository.Create(restaurant);

//        //    return id;
//        //}

//        //public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
//        //{
//        //    var restaurants = await _restaurantsRepository.GetAllAsync();
//        //    _logger.LogInformation("Getting all restaurants");

//        //    // we are using IMapper instead
//        //    //var restaurantsDtos = restaurants.Select(RestaurantDto.FromEntity);
//        //    var restaurantsDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

//        //    return restaurantsDtos!;
//        //}

//        //public async Task<RestaurantDto?> GetById(int id)
//        //{
//        //    _logger.LogInformation($"Getting restaurant by {id}");

//        //    var restaurant = await _restaurantsRepository.GetByIdAsync(id);

//        //    // we are using IMapper instead
//        //    //var restaurantDto = RestaurantDto.FromEntity(restaurant);
//        //    var restaurantDto = _mapper.Map<RestaurantDto?>(restaurant);

//        //    return restaurantDto;
//        //}
//    }
//}

