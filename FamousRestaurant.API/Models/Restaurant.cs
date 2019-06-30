using FamousRestaurant.API.Contracts;
using System;

namespace FamousRestaurant.API.Models
{
    public class Restaurant : IEntity, IIsValid
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

        public Restaurant(DTO.Restaurant restaurant)
        {
            if (restaurant.Id.Equals(Guid.Empty))
            {
                Id = Guid.NewGuid();
            }
            else
            {
                Id = restaurant.Id;
            }

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

            IsValid();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException("Por favor, envie o nome do restaurante.");
            }

            if (string.IsNullOrEmpty(Zipcode))
            {
                throw new ArgumentException("Por favor, envie o CEP do restaurante.");
            }

            if (string.IsNullOrEmpty(Street))
            {
                throw new ArgumentException("Por favor, envie o logradouro do restaurante.");
            }

            if (string.IsNullOrEmpty(City))
            {
                throw new ArgumentException("Por favor, envie a cidade do restaurante.");
            }

            if (string.IsNullOrEmpty(District))
            {
                throw new ArgumentException("Por favor, envie o bairo do restaurante.");
            }

            if (string.IsNullOrEmpty(State))
            {
                throw new ArgumentException("Por favor, envie o estado do restaurante.");
            }

            return true;
        }
    }
}
