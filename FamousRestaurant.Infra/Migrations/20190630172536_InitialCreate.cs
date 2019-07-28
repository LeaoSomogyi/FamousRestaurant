using System;
using FamousRestaurant.Domain.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamousRestaurant.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBRestaurant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name_Restaurant = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Phone_Restaurant = table.Column<string>(type: "VARCHAR(14)", nullable: true),
                    Zipcode_Restaurant = table.Column<string>(type: "VARCHAR(9)", nullable: false),
                    Street_Restaurant = table.Column<string>(type: "VARCHAR(256)", nullable: false),
                    Number_Restaurant = table.Column<int>(type: "INT", nullable: true),
                    Complement_Restaurant = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    District_Restaurant = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    City_Restaurant = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    State_Restaurant = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    StockingLevel_Restaurant = table.Column<int>(type: "INT", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBRestaurant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name_User = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    Email_User = table.Column<string>(type: "VARCHAR(256)", nullable: false),
                    Password_User = table.Column<string>(type: "VARCHAR(512)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBRestaurant_Zipcode_Restaurant_Name_Restaurant_Street_Restaurant_District_Restaurant_City_Restaurant_State_Restaurant",
                table: "TBRestaurant",
                columns: new[] { "Zipcode_Restaurant", "Name_Restaurant", "Street_Restaurant", "District_Restaurant", "City_Restaurant", "State_Restaurant" });

            migrationBuilder.CreateIndex(
                name: "IX_TBUser_Email_User_Password_User_Name_User_Id",
                table: "TBUser",
                columns: new[] { "Email_User", "Password_User", "Name_User", "Id" });

            //initial admin user seed
            migrationBuilder.InsertData(
                table: "TBUser",
                columns: new[] { "Id", "Name_User", "Email_User", "Password_User" },
                values: new object[] { Guid.NewGuid(), "Felipe Somogyi", "leaosomogyi@hotmail.com", "12345678".Cript() });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBRestaurant");

            migrationBuilder.DropTable(
                name: "TBUser");
        }
    }
}
