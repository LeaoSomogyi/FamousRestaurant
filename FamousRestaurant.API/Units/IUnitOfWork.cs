using Microsoft.EntityFrameworkCore;

namespace FamousRestaurant.API.Units
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }

        void Commit();
    }
}
