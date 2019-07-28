using System;

namespace FamousRestaurant.Domain.DTO
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

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
