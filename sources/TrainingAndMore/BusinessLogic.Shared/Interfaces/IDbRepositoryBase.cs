using Data.Entities;
using System.Linq.Expressions;

namespace BusinessLogic.Shared.Interfaces
{
    public interface IDbRepositoryBase<T>: IDisposable where T : AEntityBase
    {
        Task<List<T>> GetAllAsync(bool asNoTracking = false);
        Task<T?> GetById(int id, bool asNoTracking = false);
        Task<List<T>> GetBy(IDbQueryOptions<T> options);
        Task<T> AddAsync(T entity);
        Task<T> AddOrUpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task DeleteAsync(int id);

    }
}
