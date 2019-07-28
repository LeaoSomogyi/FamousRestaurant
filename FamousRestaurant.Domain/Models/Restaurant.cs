using FamousRestaurant.Domain.Contracts;
using System;

namespace FamousRestaurant.Domain.Models
{
    public class Restaurant : IEntity, IIsValid
    {
        /// <summary>
        /// Restaurant unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Restaurant name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Restaurant phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Restaurant zipcode
        /// </summary>
        public string Zipcode { get; set; }

        /// <summary>
        /// Restaurant address street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Restaurant address number
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Restaurant address complement
        /// </summary>
        public string Complement { get; set; }

        /// <summary>
        /// Restaurant address district
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Restaurant address city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Restaurant address state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Restaurant stocking level (0 - empty/ 10 - full)
        /// </summary>
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
                throw new ArgumentException("Por favor, envie o bairro do restaurante.");
            }

            if (string.IsNullOrEmpty(State))
            {
                throw new ArgumentException("Por favor, envie o estado do restaurante.");
            }

            return true;
        }
    }
}
