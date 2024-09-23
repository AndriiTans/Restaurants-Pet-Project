using System;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext : DbContext
    {
        //
        public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : base(options)
        {

        }
        //

        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionString = "Server=localhost;Database=RestaurantsDb;User=root;Password=root;";
        //    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 39)));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);

            modelBuilder.Entity<Restaurant>().HasMany(r => r.Dishes).WithOne().HasForeignKey(d => d.RestaurantId);
        }
    }
}
