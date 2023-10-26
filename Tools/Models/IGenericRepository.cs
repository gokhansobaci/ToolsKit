using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Models
{
    public interface IGenericRepository<T> where T : class
    {
        //Task: void yapısının async yapılarda kullanımıdır.
        //Tast<type>: return type yapısının async yapılarda kullanımıdır.

        Task InsertOneAsync(T entity);
        Task InsertManyAsync(IEnumerable<T> entities);

        Task<T?> ReadOneAsync(object entityKey);
        Task<IEnumerable<T>?> ReadManyAsync(Expression<Func<T, bool>>? expression = null, string[]? includes = null);
        
        Task UpdateOneAsync(T entity);
        Task UpdateManyAsync(IEnumerable<T> entities);

        Task DeleteOneAsync(T entity);
        Task DeleteManyAsync(IEnumerable<T> entities);
        Task DeleteAllAsync();

        Task<int> CountAsync(Expression<Func<T, bool>>? expression = null);
        Task<bool> ExistsAsync(Expression<Func<T, bool>>? expression = null);
    }
}
