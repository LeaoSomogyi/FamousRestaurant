using System;

namespace FamousRestaurant.Domain.Models
{
    public class Token
    {
        public bool Authenticated { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expiration { get; set; }

        public string AccessToken { get; set; }

        public Token() { }
    }
}
