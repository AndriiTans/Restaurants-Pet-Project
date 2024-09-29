using System;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurants.API.Swagger.Examples.Restaurant
{
    public class CreateRestaurantExample : IExamplesProvider<CreateRestaurantCommand>
    {
        public CreateRestaurantCommand GetExamples()
        {
            return new CreateRestaurantCommand
            {
                Name = "Sample Restaurant",
                Description = "A sample restaurant for demonstration",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "sample@restaurant.com",
                ContactNumber = "123-456-7890",
                City = "Sample City",
                Street = "Sample Street",
                PostalCode = "12345"
            };
        }
    }

}

