﻿// <auto-generated />
using System;
using FamousRestaurant.API.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamousRestaurant.API.Migrations
{
    [DbContext(typeof(RestaurantContext))]
    partial class RestaurantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FamousRestaurant.API.Model.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("City_Restaurant")
                        .HasColumnType("VARCHAR(128)");

                    b.Property<string>("Complement")
                        .HasColumnName("Complement_Restaurant")
                        .HasColumnType("VARCHAR(256)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnName("District_Restaurant")
                        .HasColumnType("VARCHAR(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name_Restaurant")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int?>("Number")
                        .HasColumnName("Number_Restaurant")
                        .HasColumnType("INT");

                    b.Property<string>("Phone")
                        .HasColumnName("Phone_Restaurant")
                        .HasColumnType("VARCHAR(14)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnName("State_Restaurant")
                        .HasColumnType("VARCHAR(128)");

                    b.Property<int?>("StockingLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StockingLevel_Restaurant")
                        .HasColumnType("INT")
                        .HasDefaultValue(0);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnName("Street_Restaurant")
                        .HasColumnType("VARCHAR(256)");

                    b.Property<string>("Zipcode")
                        .IsRequired()
                        .HasColumnName("Zipcode_Restaurant")
                        .HasColumnType("VARCHAR(9)");

                    b.HasKey("Id");

                    b.HasIndex("Zipcode", "Name", "Street", "District", "City", "State");

                    b.ToTable("TBRestaurant");
                });
#pragma warning restore 612, 618
        }
    }
}
