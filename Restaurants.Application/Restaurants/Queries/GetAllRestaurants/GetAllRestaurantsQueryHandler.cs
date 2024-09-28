using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        private IRestaurantsRepository _restaurantsRepository;
        private ILogger<GetAllRestaurantsQueryHandler> _logger;
        private IMapper _mapper;

        public GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _restaurantsRepository.GetAllAsync();
            _logger.LogInformation("Getting all restaurants");

            // we are using IMapper instead
            //var restaurantsDtos = restaurants.Select(RestaurantDto.FromEntity);
            var restaurantsDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantsDtos!;
        }
    }
}

