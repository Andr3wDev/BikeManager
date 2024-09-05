using BikeManagement.Interfaces.Repository;
using BikeManagement.Models;

namespace BikeManagement.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Bike> Bikes { get; }
        Task<int> SaveChangesAsync();
    }
}
