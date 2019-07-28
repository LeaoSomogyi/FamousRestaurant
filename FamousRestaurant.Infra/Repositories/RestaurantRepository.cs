using FamousRestaurant.Infra.Contracts;
using FamousRestaurant.Domain.Models;

namespace FamousRestaurant.Infra.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>
    {
        public RestaurantRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
