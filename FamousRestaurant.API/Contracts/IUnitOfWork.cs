using Microsoft.EntityFrameworkCore;

namespace FamousRestaurant.API.Contracts
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }

        void Commit();
    }
}
