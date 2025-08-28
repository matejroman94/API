using Database;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VZPStatDbContext _db;
        public Repository(VZPStatDbContext db)
        {
            _db = db;
        }

        public bool Add(T entity)
        {
            try
            {
                _db.Add(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository Add function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _db.AddAsync(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository AddAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            try
            {
                _db.AddRange(entities);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository AddRange function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _db.AddRangeAsync(entities);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository AddRangeAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public IEnumerable<T> GetAll(ref Filter pagFilter, Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _db.Set<T>();
                pagFilter.TotalCount = query.Count();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                
                if (pagFilter.PageSize > 0)
                {
                    query = query.Skip(pagFilter.PageSize * (pagFilter.PageNumber - 1)).Take(pagFilter.PageSize);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository GetAll function Failed:" + msg, Logger.Logger.Level.Warning);
                pagFilter.TotalCount = 0;
                return Enumerable.Empty<T>();
            }
        }

        public async Task<(IEnumerable<T>, int TotalCount)> GetAllAsync(Filter pagFilter, Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            (IEnumerable<T> item, int TotalCount) result = (Enumerable.Empty<T>(), 0);
            try
            {
                IQueryable<T> query = _db.Set<T>();
                pagFilter.TotalCount = query.Count();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                result.TotalCount = query.Count();

                if (pagFilter.PageSize > 0)
                {
                    query = query.Skip(pagFilter.PageSize * (pagFilter.PageNumber - 1)).Take(pagFilter.PageSize);
                }

                result.item = await query.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository GetAllAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return result;
            }
        }

        public Task<(IEnumerable<T>, int TotalCount)> GetAllFilterAsync(Filter pagFilter, IEnumerable<Expression<Func<T, bool>>>? filter = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties = "", bool tracked = true)
        {
            try
            {
                IQueryable<T> query = _db.Set<T>();
                if (!tracked)
                {
                    query = query.AsNoTracking();
                }
                query = query.Where(filter);

                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository GetFirstOrDefault function Failed:" + msg, Logger.Logger.Level.Warning);
                return null;
            }
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeProperties = "", bool tracked = true)
        {
            try
            {
                IQueryable<T> query = _db.Set<T>();
                if (!tracked)
                {
                    query = query.AsNoTracking();
                }
                query = query.Where(filter);

                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository GetFirstOrDefaultAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return null;
            }
        }

        public IEnumerable<T> GetLast1000()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetLast1000Async()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T entity)
        {
            try
            {
                _db.Remove(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository Remove function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository RemoveAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                _db.RemoveRange(entities);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository RemoveRange function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _db.RemoveRange(entities);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("Repository RemoveRangeAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
