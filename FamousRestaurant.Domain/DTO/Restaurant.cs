using Newtonsoft.Json;
using System;

namespace FamousRestaurant.Domain.DTO
{
    [JsonObject("restaurant")]
    public class Restaurant
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("stocking_level")]
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
