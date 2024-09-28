using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Commands.SeedRestaurants;

public class SeedRestaurantsCommand : IRequest<bool>
{
    public required IFormFile File { get; set; }
}
//public class SeedRestaurantsCommand : IRequest<bool>
//{
//    public IEnumerable<RestaurantDto>? RestaurantsDtos { get; set; }
//}


