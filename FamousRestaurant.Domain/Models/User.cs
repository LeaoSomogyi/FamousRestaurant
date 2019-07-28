using FamousRestaurant.Domain.Contracts;
using FamousRestaurant.Domain.Extensions;
using System;
using System.Linq.Expressions;

namespace FamousRestaurant.Domain.Models
{
    public class User : IEntity, IIsValid
    {
        /// <summary>
        /// User unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User e-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password. This will be encrypted when save on database
        /// </summary>
        public string Password { get; set; }

        public User() { }

        /// <summary>
        /// Constructor with DTO properties
        /// </summary>
        /// <param name="user">DTO User received on API</param>
        /// <param name="isRemoving">If this is true, not validate information because in this moment there's no password</param>
        public User(DTO.User user, bool isRemoving = false)
        {
            if (user.Id.Equals(Guid.Empty))
            {
                Id = Guid.NewGuid();
            }
            else
            {
                Id = user.Id;
            }

            Name = user.Name;
            Email = user.Email;

            if (!isRemoving)
            {
                Password = user.Password.Cript();
                IsValid();
            }            
        }

        /// <summary>
        /// Constructor to use on login
        /// </summary>
        /// <param name="email">User e-mail</param>
        /// <param name="password">User password</param>
        public User(string email, string password)
        {
            Email = email;
            Password = password.Cript();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException("Por favor, envie o nome do usuário.");
            }

            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentException("Por favor, envie o e-mail do usuário.");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("Por favor, envie a senha do usuário.");
            }

            return true;
        }

        /// <summary>
        /// Method used to retrieve where expression to pass as conditional on Search
        /// </summary>
        /// <param name="email">User e-mail</param>
        /// <param name="password">User password</param>
        /// <returns>A expression to use on Where clause</returns>
        public static Expression<Func<User, bool>> RetrieveLoginExpression(string email, string password)
        {
            string cript = password.Cript();

            return u => u.Email.Equals(email) && u.Password.Equals(cript);
        }

        /// <summary>
        /// Method used to retrieve where expression to pass as conditional on Search
        /// </summary>
        /// <param name="user">User intent to login</param>
        /// <returns>A expression to use on Where clause</returns>
        public static Expression<Func<User, bool>> RetrieveLoginExpression(User user)
        {
            return u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password);
        }
    }
}
