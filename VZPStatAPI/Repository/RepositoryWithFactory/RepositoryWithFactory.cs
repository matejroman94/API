using Database;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.FilterHelper;
using Repository.Interfaces;
using Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repository.RepositoryWithFactory
{
    public class RepositoryWithFactory<T> : IRepository<T> where T : class
    {
        private readonly Func<VZPStatDbContext> _factory;

        public RepositoryWithFactory(Func<VZPStatDbContext> factory)
        {
            _factory = factory;

            using (var context = _factory.Invoke())
            {
                var databaseName = context.Database.GetDbConnection().Database;
                if (string.IsNullOrWhiteSpace(databaseName))
                {
                    throw new InvalidOperationException("Unable to determine the database name.");
                }
                context.Database.ExecuteSqlRaw($"ALTER DATABASE {databaseName} SET ALLOW_SNAPSHOT_ISOLATION ON");
            }
        }

        public bool Add(T entity)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    dbSet.Add(entity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory Add function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    dbSet.Add(entity);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory AddAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    dbSet.AddRange(entities);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory AddRange function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    dbSet.AddRange(entities);
                    var res = await context.SaveChangesAsync();
                    return res > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory AddRangeAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public virtual IEnumerable<T> GetAll(ref Filter pagFilter, Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    IQueryable<T> query = dbSet;

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    pagFilter.TotalCount = query.Count();

                    if (pagFilter.PageSize > 0)
                    {
                        query = query.Skip(pagFilter.PageSize * (pagFilter.PageNumber - 1)).Take(pagFilter.PageSize);
                    }

                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory GetAll function Failed:" + msg, Logger.Logger.Level.Warning);
                pagFilter.TotalCount = 0;
                return Enumerable.Empty<T>();
            }
        }

        public virtual async Task<(IEnumerable<T>, int TotalCount)> GetAllAsync(Filter pagFilter, Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            (IEnumerable<T> item, int TotalCount) result = (Enumerable.Empty<T>(), 0);
            try
            {                
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    IQueryable<T> query = dbSet;

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }

                    result.item = await query.ToListAsync();
                    result.TotalCount = result.item.Count();

                    if (pagFilter.PageSize > 0)
                    {
                        result.item = result.item.Skip(pagFilter.PageSize * (pagFilter.PageNumber - 1)).Take(pagFilter.PageSize).ToList();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory GetAllAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return result;
            }
        }

        public async Task<(IEnumerable<T>, int TotalCount)> GetAllFilterAsync(Filter pagFilter, IEnumerable<Expression<Func<T, bool>>>? filter = null, string includeProperties = "")
        {
            (IEnumerable<T> item, int TotalCount) result = (Enumerable.Empty<T>(), 0);
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    IQueryable<T> query = dbSet;

                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }

                    if (filter != null)
                    {
                        foreach (var f in filter)
                        {
                            query = query.Where(f);
                        }
                    }

                    result.item = await query.ToListAsync();
                    result.TotalCount = result.item.Count();

                    if (pagFilter.PageSize > 0)
                    {
                        result.item = result.item.Skip(pagFilter.PageSize * (pagFilter.PageNumber - 1)).Take(pagFilter.PageSize).ToList();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory GetAllAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return result;
            }
        }

        public virtual T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties = "", bool tracked = true)
        {
            try
            {
                using (var context = _factory.Invoke()) 
                { 


                    using (var scope = new TransactionScope(TransactionScopeOption.Required, new
                             TransactionOptions
                        { IsolationLevel = IsolationLevel.Snapshot }))
                        {
                            //context.Database.ExecuteSqlRaw("SET TRANSACTION ISOLATION LEVEL SNAPSHOT");
                            var dbSet = context.Set<T>();
                            IQueryable<T> query = dbSet;
                            if (!tracked) {
                                query = query.AsNoTracking();
                            }
                            query = query.Where(filter);

                            foreach (var includeProperty in includeProperties.Split
                            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                                query = query.Include(includeProperty);
                            }

                            return query.FirstOrDefault();
                        }
                }
            }
            catch (Exception ex)
            {
                var str = new StringBuilder();
                str.AppendLine($"RepositoryWithFactory GetFirstOrDefault function failed {typeof(T).FullName}: ");
                str.Append(Logger.Exceptions.HelperEx.FlattenException(ex));
                Logger.Logger.NewOperationLog(str.ToString(), Logger.Logger.Level.Warning);
                return null;
            }
        }

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeProperties = "", bool tracked = true)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    IQueryable<T> query = dbSet;
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
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory GetFirstOrDefaultAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return null;
            }
        }

        public bool Remove(T entity)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    dbSet.Remove(entity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory Remove function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    dbSet.Remove(entity);
                    var res = await context.SaveChangesAsync();
                    return res > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory RemoveAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    dbSet.RemoveRange(entities);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory RemoveRange function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<T>();
                    dbSet.RemoveRange(entities);
                    var res = await context.SaveChangesAsync();
                    return res > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RepositoryWithFactory RemoveRangeAsync function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
