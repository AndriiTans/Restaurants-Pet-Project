using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Restaurants.Application.Restaurants.Commands.SeedRestaurants;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

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
        if (request.File == null || request.File.Length == 0)
        {
            return false;
        }

        // Initialize a buffer to read the file in chunks
        var buffer = new byte[8192]; // 8 KB chunks
        var sb = new StringBuilder(); // For collecting chunks of data
        int bytesRead;

        using (var stream = request.File.OpenReadStream())
        {
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
            {
                // Convert the bytes to string and append to StringBuilder
                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
        }

        // The full JSON content is now in the StringBuilder
        string jsonData = sb.ToString();

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




//using AutoMapper;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using Restaurants.Application.Restaurants.Dtos;
//using Restaurants.Domain.Entities;
//using Restaurants.Domain.Repositories;
//using System.Collections.Generic;
//using System.Text.Json;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Restaurants.Application.Restaurants.Commands.SeedRestaurants;

//public class SeedRestaurantsCommandHandler : IRequestHandler<SeedRestaurantsCommand, bool>
//{
//    private readonly IRestaurantsRepository _restaurantsRepository;
//    private readonly IMapper _mapper;

//    public SeedRestaurantsCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
//    {
//        _restaurantsRepository = restaurantsRepository;
//        _mapper = mapper;
//    }

//    public async Task<bool> Handle(SeedRestaurantsCommand request, CancellationToken cancellationToken)
//    {
//        // Read the file's content
//        string jsonData;
//        using (var streamReader = new StreamReader(request.File.OpenReadStream()))
//        {
//            jsonData = await streamReader.ReadToEndAsync();
//        }

//        // Deserialize JSON into DTOs
//        var restaurantDtos = JsonSerializer.Deserialize<IEnumerable<RestaurantDto>>(jsonData, new JsonSerializerOptions
//        {
//            PropertyNameCaseInsensitive = true
//        });



//        if (restaurantDtos == null)
//        {
//            return false; // Invalid data, return failure
//        }

//        // Map DTOs to domain entities
//        var restaurants = _mapper.Map<IEnumerable<Restaurant>>(restaurantDtos);

//        // Save to the database (through repository or another service)
//        await _restaurantsRepository.AddRangeAsync(restaurants);

//        return true;
//    }
//}
