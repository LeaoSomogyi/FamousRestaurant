using FamousRestaurant.Infra.Contracts;
using FamousRestaurant.Domain.Models;

namespace FamousRestaurant.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
