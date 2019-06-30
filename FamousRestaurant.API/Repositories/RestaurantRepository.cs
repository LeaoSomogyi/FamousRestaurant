using FamousRestaurant.API.Contracts;
using FamousRestaurant.API.Model;

namespace FamousRestaurant.API.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>
    {
        public RestaurantRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
