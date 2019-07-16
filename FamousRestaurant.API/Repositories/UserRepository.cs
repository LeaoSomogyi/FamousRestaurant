using FamousRestaurant.API.Units;
using FamousRestaurant.Domain.Models;

namespace FamousRestaurant.API.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
