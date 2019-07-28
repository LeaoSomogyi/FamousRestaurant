using FamousRestaurant.Domain.Models;
using System.Threading.Tasks;

namespace FamousRestaurant.Domain.Contracts
{
    public interface ILoginService
    {
        /// <summary>
        /// Method responsible to validate if user exists and generate an access token for him
        /// </summary>
        /// <param name="user">User information to login</param>
        /// <returns>Access Token</returns>
        Task<Token> DoLogin(User user);
    }
}
