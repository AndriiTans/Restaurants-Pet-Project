using System;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder : IRestaurantSeeder
    {
        private readonly RestaurantsDbContext _dbContext;

        public RestaurantSeeder(RestaurantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    await _dbContext.SaveChangesAsync();
                }

            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Nashville Hot Chicken (10 pcs.)",
                            Price = 10.30M,
                        },
                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (5 pcs.)",
                            Price = 5.30M,
                        }
                    },
                    Address = new Address()
                    {
                        City = "London",
                        Street = "Cork St 5",
                        PostalCode = "WC2N 5DU"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald's",
                    Category = "Fast Food",
                    Description = "McDonald's is one of the largest fast food chains, known for its burgers and fries.",
                    ContactEmail = "contact@mcdonalds.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Big Mac",
                            Description = "A double decker burger with beef patties, lettuce, and special sauce.",
                            Price = 7.50M,
                        },
                        new Dish()
                        {
                            Name = "French Fries",
                            Description = "Classic McDonald's fries (Medium).",
                            Price = 3.00M,
                        }
                    },
                    Address = new Address()
                    {
                        City = "London",
                        Street = "Oxford St 12",
                        PostalCode = "W1D 1LT"
                    }
                },
                new Restaurant()
                {
                    Name = "Burger King",
                    Category = "Fast Food",
                    Description = "Burger King is a global chain known for its flame-grilled burgers.",
                    ContactEmail = "contact@burgerking.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Whopper",
                            Description = "A large flame-grilled beef burger with lettuce, tomato, and mayo.",
                            Price = 8.00M,
                        },
                        new Dish()
                        {
                            Name = "Onion Rings",
                            Description = "Crispy onion rings (Large).",
                            Price = 3.50M,
                        }
                    },
                    Address = new Address()
                    {
                        City = "London",
                        Street = "King St 15",
                        PostalCode = "SW1Y 6QY"
                    }
                }
            };

            return restaurants;
        }


    }
}

