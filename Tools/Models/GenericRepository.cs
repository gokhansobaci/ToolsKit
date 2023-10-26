using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tools.Models
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _context;
        protected DbSet<T> _table;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? expression = null)
        {
            return expression != null ? await _table.CountAsync(expression) : await _table.CountAsync();
        }

        public async Task DeleteAllAsync()
        {
            var entities = await ReadManyAsync();
            if (entities != null)
            {
                await Task.Run(() => _table.RemoveRange(entities));
            }
        }

        public async Task DeleteManyAsync(IEnumerable<T> entities)
        {
            //Async olmayan fonksiyonlar, Task.run fonksiyonuyla çağırılır.
            await Task.Run(() => _table.RemoveRange(entities));
        }

        public async Task DeleteOneAsync(T entity)
        {
            await Task.Run(() => _table.Remove(entity));
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>>? expression = null)
        {
            return expression != null ? await _table.AnyAsync(expression) : await _table.AnyAsync();
        }

        public async Task InsertManyAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public async Task InsertOneAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task<IEnumerable<T>?> ReadManyAsync(Expression<Func<T, bool>>? expression = null, string[]? includes = null)
        {
            IQueryable<T> data = expression != null ? _table.Where(expression) : _table;
            if (data != null)
            {
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        data = data.Include(item);
                    }
                }
            }
            return await Task.Run(() => data);
        }

        public async Task<T?> ReadOneAsync(object entityKey) => await _table.FindAsync(entityKey);

        public async Task UpdateManyAsync(IEnumerable<T> entities) => await Task.Run(() => _table.UpdateRange(entities));

        public async Task UpdateOneAsync(T entity) => await Task.Run(() => _table.Update(entity));
    }
}
