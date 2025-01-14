
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Repositories.Implementation;

namespace ShoeStore.Repositories // Bu sınıf temel CRUD işlemleri yapıcak.
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context; //private olunca alt sınıflar erişemez.(extend)
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(); // Db'de güncelleme yapması için
        }

        public async Task DeleteAsync(int id)
        {
            var data = await GetByIdAsync(id);
            if (data != null) 
            {
                _dbSet.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
           _dbSet.Update(entity);
           await _context.SaveChangesAsync();
        }

        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            
            foreach(var item in includes) 
            {
                query = query.Include(item);
            }

            return await query.ToListAsync();
        }
    }
}
