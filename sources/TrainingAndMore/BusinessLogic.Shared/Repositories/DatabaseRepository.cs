using BusinessLogic.Shared.Interfaces;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusinessLogic.Shared.Repositories
{
    public class DatabaseRepository<T>:  IDbRepositoryBase<T> where T : AEntityBase
    {
        private AppDataContext _context;
        private bool disposedValue;

        public DatabaseRepository(AppDataContext context) 
        { 
            _context = context;
        }

        public async Task<List<T>> GetAllAsync(bool asNoTracking = false)
        {
            var table = _context.Set<T>();

            if (asNoTracking)
            {
                return await table.AsNoTracking().ToListAsync();
            }

            return await table.ToListAsync();
        }

        public async Task<T?> GetById(int id, bool asNoTracking = false)
        {
            var table = _context.Set<T>();

            if (asNoTracking)
            {
                return await table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }

            return await table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetBy(IDbQueryOptions<T> options)
        {
            var table = _context.Set<T>();

            List<T> data;

            if (options.AsNoTracking)
            {
               data = await table.AsNoTracking().ToListAsync();
            }
            else
            {
                data = await table.ToListAsync();
            }

            if (options.WhereExpression != null) 
            { 
                return data.Where(options.WhereExpression).ToList();
            }

            return data;
        }

        public async Task<T> AddAsync(T entity)
        {
            var table = _context.Set<T>();

            var result = await table.AddAsync(entity);

            return result.Entity;
        }

        public async Task<T> AddOrUpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            var table = _context.Set<T>();

            var exists = predicate != null ? table.Any(predicate) : table.Any();

            if (!exists)
            {
                var result = await table.AddAsync(entity);

                return result.Entity;
            }

            var updateResult = table.Update(entity);

            return await Task.FromResult(updateResult.Entity);
        }

        public async Task DeleteAsync(int id)
        {

            var table = _context.Set<T>();

            var entity = await table.FirstOrDefaultAsync(x  => x.Id == id);

            if (entity != null) 
            {
                table.Remove(entity);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
