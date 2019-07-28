using System;

namespace FamousRestaurant.Domain.Models
{
    public class Token
    {
        /// <summary>
        /// Indicate if user is authenticated
        /// </summary>
        public bool Authenticated { get; set; }

        /// <summary>
        /// Token creation date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Token expiration date
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Access token
        /// </summary>
        public string AccessToken { get; set; }

        public Token() { }
    }
}
