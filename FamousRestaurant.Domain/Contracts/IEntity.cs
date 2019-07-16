using System;

namespace FamousRestaurant.Domain.Contracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
