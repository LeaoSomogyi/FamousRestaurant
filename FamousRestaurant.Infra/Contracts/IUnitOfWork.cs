using Microsoft.EntityFrameworkCore;

namespace FamousRestaurant.Infra.Contracts
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }

        void Commit();
    }
}
