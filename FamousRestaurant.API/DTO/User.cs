using Newtonsoft.Json;
using System;

namespace FamousRestaurant.API.DTO
{
    [JsonObject("user")]
    public class User
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        public User() { }

        public User(Models.User user)
        {
            if (user == null)
            {
                return;
            }

            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
        }
    }
}
