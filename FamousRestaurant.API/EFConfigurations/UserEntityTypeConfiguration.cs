using FamousRestaurant.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamousRestaurant.API.EFConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> userConfiguration)
        {
            userConfiguration.ToTable("TBUser");

            userConfiguration.HasKey(u => u.Id);

            userConfiguration.Property<string>("Name")
                .HasColumnName("Name_User")
                .HasColumnType("VARCHAR(128)")
                .IsRequired(true);

            userConfiguration.Property<string>("Email")
                .HasColumnName("Email_User")
                .HasColumnType("VARCHAR(256)")
                .IsRequired(true);

            userConfiguration.Property<string>("Password")
                .HasColumnName("Password_User")
                .HasColumnType("VARCHAR(512)")
                .IsRequired(true);

            userConfiguration.HasIndex("Email", "Password", "Name", "Id");
        }
    }
}
