using Domain.Models;
using Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(ref Filter pagFilter, Expression<Func<T, bool>>? filter = null, string includeProperties = "");
        T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties = "", bool tracked = true);
        bool Add(T entity);
        bool AddRange(IEnumerable<T> entities);
        bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);

        Task<(IEnumerable<T>, int TotalCount)> GetAllAsync(Filter pagFilter, Expression<Func<T, bool>>? filter = null, string includeProperties = "");
        Task<(IEnumerable<T>, int TotalCount)> GetAllFilterAsync(Filter pagFilter, IEnumerable<Expression<Func<T, bool>>>? filter = null, string includeProperties = "");
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeProperties = "", bool tracked = true);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<bool> RemoveAsync(T entity);
        Task<bool> RemoveRangeAsync(IEnumerable<T> entities);
    }
}
