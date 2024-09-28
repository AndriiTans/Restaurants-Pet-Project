using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.SeedRestaurants;

public class SeedRestaurantsCommandHandler : IRequestHandler<SeedRestaurantsCommand, bool>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IMapper _mapper;

    public SeedRestaurantsCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(SeedRestaurantsCommand request, CancellationToken cancellationToken)
    {
        // Read the file's content
        string jsonData;
        using (var streamReader = new StreamReader(request.File.OpenReadStream()))
        {
            jsonData = await streamReader.ReadToEndAsync();
        }

        // Deserialize JSON into DTOs
        var restaurantDtos = JsonSerializer.Deserialize<IEnumerable<RestaurantDto>>(jsonData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });



        if (restaurantDtos == null)
        {
            return false; // Invalid data, return failure
        }

        // Map DTOs to domain entities
        var restaurants = _mapper.Map<IEnumerable<Restaurant>>(restaurantDtos);

        // Save to the database (through repository or another service)
        await _restaurantsRepository.AddRangeAsync(restaurants);

        return true;
    }
}



//public class SeedRestaurantsCommandHandler : IRequestHandler<SeedRestaurantsCommand, bool>
//{
//    private readonly IRestaurantsRepository _restaurantsRepository;
//    private readonly ILogger<SeedRestaurantsCommandHandler> _logger;
//    private IMapper _mapper;


//    public SeedRestaurantsCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger<SeedRestaurantsCommandHandler> logger, IMapper mapper)
//    {
//        _restaurantsRepository = restaurantsRepository;
//        _logger = logger;
//        _mapper = mapper;
//    }

//    public async Task<bool> Handle(SeedRestaurantsCommand request, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("Seeding restaurants data...");


//        //if (await _restaurantsRepository.GetAllAsync() != null)
//        //{
//        //    _logger.LogInformation("Database already contains restaurants, skipping seeding.");
//        //    return false;
//        //}


//        var restaurantsToAdd = _mapper.Map<IEnumerable<Restaurant>>(request.RestaurantsDtos);

//        // Add the mapped restaurants to the repository
//        await _restaurantsRepository.AddRangeAsync(restaurantsToAdd);

//        _logger.LogInformation("Restaurants data seeded.");
//        return true;
//    }
//}
