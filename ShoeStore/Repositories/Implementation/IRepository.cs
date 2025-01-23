using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace ShoeStore.Repositories.Implementation
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id); // T entity olabilir de
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
