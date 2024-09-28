using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private IRestaurantsRepository _restaurantsRepository;
        private ILogger<CreateRestaurantCommandHandler> _logger;
        private IMapper _mapper;

        public CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create restaurant");

            var restaurant = _mapper.Map<Restaurant>(request);

            int id = await _restaurantsRepository.Create(restaurant);

            return id;
        }
    }
}

