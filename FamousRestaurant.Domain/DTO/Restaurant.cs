using System;

namespace FamousRestaurant.Domain.DTO
{
    public class Restaurant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Zipcode { get; set; }

        public string Street { get; set; }

        public int? Number { get; set; }

        public string Complement { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int? StockingLevel { get; set; }

        public Restaurant() { }

        public Restaurant(Models.Restaurant restaurant)
        {
            if (restaurant == null) return;

            Id = restaurant.Id;
            Name = restaurant.Name;
            Phone = restaurant.Phone;
            Zipcode = restaurant.Zipcode;
            Street = restaurant.Street;
            Number = restaurant.Number;
            Complement = restaurant.Complement;
            District = restaurant.District;
            City = restaurant.City;
            State = restaurant.State;
            StockingLevel = restaurant.StockingLevel;
        }
    }
}
