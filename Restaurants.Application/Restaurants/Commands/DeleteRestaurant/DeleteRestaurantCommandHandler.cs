using System;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        private ILogger<DeleteRestaurantCommandHandler> _logger;
        private IRestaurantsRepository _restaurantsRepository;

        public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository)
        {
            _logger = logger;
            _restaurantsRepository = restaurantsRepository;
        }

        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting restaurant with id: {request.Id}");

            var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);

            if (restaurant is null)
            {
                return false;
            }

            await _restaurantsRepository.Delete(restaurant);

            return true;

        }
    }
}

