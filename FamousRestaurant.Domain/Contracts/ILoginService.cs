using FamousRestaurant.Domain.Models;
using System.Threading.Tasks;

namespace FamousRestaurant.Domain.Contracts
{
    public interface ILoginService
    {
        Task<Token> DoLogin(User user);
    }
}
