using FamousRestaurant.API.Units;
using FamousRestaurant.Domain.Models;

namespace FamousRestaurant.API.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>
    {
        public RestaurantRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
