using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository : IRestaurantsRepository
    {
        private RestaurantsDbContext _dbContext;

        public RestaurantsRepository(RestaurantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Restaurant entity)
        {
            _dbContext.Restaurants.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(Restaurant entity)
        {
            _dbContext.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await _dbContext.Restaurants.Include(r => r.Dishes).ToListAsync();

            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await _dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefaultAsync(x => x.Id == id);

            return restaurant;
        }
    }
}

