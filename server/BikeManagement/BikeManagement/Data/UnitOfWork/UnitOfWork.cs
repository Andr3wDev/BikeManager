using BikeManagement.Data.Context;
using BikeManagement.Models;
using BikeManagement.Interfaces.Repository;
using BikeManagement.Interfaces.UnitOfWork;
using BikeManagement.Data.Repository;

namespace BikeManagement.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BikeDbContext _context;

        private IGenericRepository<Bike>? _bikeRepository;

        public UnitOfWork(BikeDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Bike> Bikes =>
            _bikeRepository ??= new GenericRepository<Bike>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();

            // Commented out for In-Mem database not supporting transactions

            /*using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }*/
        }
    }
}
