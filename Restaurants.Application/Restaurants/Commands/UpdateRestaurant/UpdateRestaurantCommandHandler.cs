using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
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

        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update restaurant with id ${request.Id}");

            var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);

            if (restaurant is null)
            {
                //return false;
                throw new NotFoundException($"Restaurant with {request.Id} doesnt exist");
            }

            _mapper.Map(request, restaurant);

            // Using _mapper.Map instead
            //restaurant.Name = request.Name;
            //restaurant.Description = request.Description;
            //restaurant.HasDelivery = request.HasDelivery;

            await _restaurantsRepository.SaveChanges();

            //return true;
        }
    }
}
