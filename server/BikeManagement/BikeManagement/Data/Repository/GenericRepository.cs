using BikeManagement.Data.Context;
using BikeManagement.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace BikeManagement.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BikeDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(BikeDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dbSet.Remove(entity);
            return true;
        }
    }
}
