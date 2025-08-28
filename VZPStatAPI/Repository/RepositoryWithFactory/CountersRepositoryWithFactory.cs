using Database;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryWithFactory
{
    public class CountersRepositoryWithFactory : RepositoryWithFactory<Counter>, ICountersRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public CountersRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public bool JoinClerkAndCounter(int CounterId, int ClerkId)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    Clerk clerk = new Clerk { ClerkId = ClerkId };
                    context.Clerks.Add(clerk);
                    context.Clerks.Attach(clerk);

                    Counter counter = new Counter { CounterId = CounterId };
                    context.Counters.Add(counter);
                    context.Counters.Attach(counter);

                    clerk.Counters?.Add(counter);

                    int i = context.SaveChanges();
                    return i > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("CountersRepositoryWithFactory JoinClerkAndCounter function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public bool UpdateCounterStatus(int CounterId, int CounterStatusId)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<Counter>();
                    var entity = dbSet.Where(x => x.CounterId == CounterId).FirstOrDefault();
                    if (entity is null)
                    {
                        throw new Exception($"Entity cannot be found: {CounterId}");
                    }

                    if (entity.CounterStatusId == CounterStatusId) { return true; }
                    entity.CounterStatusId = CounterStatusId;

                    var res = context.SaveChanges();
                    return res > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("CountersRepositoryWithFactory UpdateCounterStatus function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public bool Update(Counter obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.Counters.Update(obj);
                    int i = context.SaveChanges();
                    return i > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("CountersRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<IEnumerable<CounterStatus>> GetCounterStatusReasonAsync(Expression<Func<CounterStatus, bool>>? filter = null, string includeProperties = "")
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    IQueryable<CounterStatus> query = context.Set<CounterStatus>();

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }

                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("CountersRepositoryWithFactory GetCounterStatusReasonAsync function failed: " + msg, Logger.Logger.Level.Warning);
                return Enumerable.Empty<CounterStatus>();
            }
        }
    }
}
