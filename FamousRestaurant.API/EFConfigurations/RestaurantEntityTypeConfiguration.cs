using FamousRestaurant.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamousRestaurant.API.EFConfigurations
{
    public class RestaurantEntityTypeConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> restaurantConfiguration)
        {
            restaurantConfiguration.ToTable("TBRestaurant");

            restaurantConfiguration.HasKey(r => r.Id);

            restaurantConfiguration.Property<string>("Name")
                .HasColumnName("Name_Restaurant")
                .HasColumnType("VARCHAR(50)")
                .IsRequired(true);

            restaurantConfiguration.Property<string>("Phone")
                .HasColumnName("Phone_Restaurant")
                .HasColumnType("VARCHAR(14)");

            restaurantConfiguration.Property<string>("Zipcode")
                .HasColumnName("Zipcode_Restaurant")
                .HasColumnType("VARCHAR(9)")
                .IsRequired(true);

            restaurantConfiguration.Property<string>("Street")
                .HasColumnType("VARCHAR(256)")
                .HasColumnName("Street_Restaurant")
                .IsRequired(true);

            restaurantConfiguration.Property<int?>("Number")
                .HasColumnType("INT")
                .HasColumnName("Number_Restaurant")
                .IsRequired(false);

            restaurantConfiguration.Property<string>("Complement")
                .HasColumnType("VARCHAR(256)")
                .HasColumnName("Complement_Restaurant");

            restaurantConfiguration.Property<string>("District")
                .HasColumnType("VARCHAR(128)")
                .HasColumnName("District_Restaurant")
                .IsRequired(true);

            restaurantConfiguration.Property<string>("City")
                .HasColumnType("VARCHAR(128)")
                .HasColumnName("City_Restaurant")
                .IsRequired(true);

            restaurantConfiguration.Property<string>("State")
                .HasColumnType("VARCHAR(128)")
                .HasColumnName("State_Restaurant")
                .IsRequired(true);

            restaurantConfiguration.Property<int?>("StockingLevel")
                .HasColumnType("INT")
                .HasColumnName("StockingLevel_Restaurant")
                .HasDefaultValue(0)
                .IsRequired(false);

            restaurantConfiguration.HasIndex("Zipcode", "Name", "Street", "District", "City", "State");
        }
    }
}
