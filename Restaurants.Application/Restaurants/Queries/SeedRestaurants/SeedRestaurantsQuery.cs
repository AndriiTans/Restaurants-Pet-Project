using System;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.SeedRestaurants
{
    public class SeedRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
    {

    }
}

