using FamousRestaurant.API.EFConfigurations;
using FamousRestaurant.API.Model;
using Microsoft.EntityFrameworkCore;

namespace FamousRestaurant.API.DataContext
{
    public class RestaurantContext : DbContext
    {
        public virtual DbSet<Restaurant> Restaurants { get; set; }

        public RestaurantContext(DbContextOptions<RestaurantContext> contextOptions) : 
            base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RestaurantEntityTypeConfiguration());
        }
    }
}
