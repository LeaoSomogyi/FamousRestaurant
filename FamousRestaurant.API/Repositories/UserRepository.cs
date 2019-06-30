using FamousRestaurant.API.Contracts;
using FamousRestaurant.API.Models;

namespace FamousRestaurant.API.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
