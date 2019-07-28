using FamousRestaurant.Infra.EFConfigurations;
using FamousRestaurant.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FamousRestaurant.Infra.DataContext
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions) : 
            base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RestaurantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
